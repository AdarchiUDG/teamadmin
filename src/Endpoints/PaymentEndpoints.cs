using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using Prometheus.Data.Entities;
using Prometheus.Models.Payment;
using Prometheus.Models.Requests;
using SkiaSharp;

namespace Prometheus.Endpoints; 

public static class PaymentEndpoints {
  public static IEndpointRouteBuilder AddPaymentEndpoints(this IEndpointRouteBuilder builder) {
    var crudEndpoints = new CrudEndpointBuilder<Payment, Guid, PaymentResponse>(builder);
    crudEndpoints.MapPaged<ListRequest>(QueryGenerator)
      .WithSwagger("List Payments", "Gets the list of all payments");
    crudEndpoints.MapGetById<KeyRequest<Guid>>(QueryGenerator)
      .WithSwagger("Get Payment By Id", "Gets a payment by its ID");
    crudEndpoints.MapCreate<BodyRequest<PaymentPayload>>(Mapper)
      .WithSwagger("Create Payment", "Assigns a payment to a user");
    crudEndpoints.MapUpdate<UpdateRequest<Guid, PaymentPayload>, PaymentPayload>(QueryGenerator, ObjectUpdateDelegate)
      .WithSwagger("Update Payment", "Updates a payment");
    crudEndpoints.MapDelete<KeyRequest<Guid>>(QueryGenerator)
      .WithSwagger("Delete Payment", "Deletes a payment by its ID");

    builder.MapPatch("{id:guid}/paid", async Task<Results<NotFound, NoContent>>([AsParameters] KeyRequest<Guid> request) => {
      var payment = await QueryGenerator(request).Where(p => p.Id == request.Id)
        .FirstOrDefaultAsync();

      if (payment is null) {
        return TypedResults.NotFound();
      }

      payment.Paid = true;
      payment.PaidAt = DateTime.UtcNow;

      await request.Database.SaveChangesAsync();

      return TypedResults.NoContent();
    }).WithSwagger("Mark As Paid", "Marks a payment as paid and updates its paidat info");

    var voucher = builder.MapGroup("{id:guid}/voucher");
    voucher.MapPut("", async Task<Results<NotFound, Created>> (Guid id, IFormFile file, [AsParameters] Request request) => {
      var payment = await QueryGenerator(request).Where(p => p.Id == id)
        .FirstOrDefaultAsync();

      if (payment is null) {
        return TypedResults.NotFound();
      }

      payment.HasVoucher = true;

      await request.Database.SaveChangesAsync();
      await using var fileStream = file.OpenReadStream();
      using var image = SKBitmap.Decode(fileStream);
      var filePath = Path.Combine("wwwroot", "vouchers", $"{payment.Id}.jpg");
      Directory.CreateDirectory(Path.GetDirectoryName(filePath)!);

      if (File.Exists(filePath)) {
        File.Delete(filePath);
      }
      await using var stream = File.OpenWrite(filePath);
      image.Encode(stream, SKEncodedImageFormat.Jpeg, 80);
      
      await request.Database.SaveChangesAsync();
      
      request.Notifications.Create(new Notification() {
        Title = $"Voucher Disponible",
        Content = $"Tu voucher para el pago {payment.Name} ya se encuentra disponible.",
        ScheduledAt = DateTime.UtcNow,
        MetaData = new NotificationMetaData {
          Key = payment.Id.ToString(),
          Type = NotificationType.PaymentVoucher
        },
        Targets = new List<User>() { payment.User }
      }, request.Database).Wait();

      return TypedResults.Created($"/vouchers/{payment.Id}");
    }).WithSwagger("Attach Payment Voucher", "Attaches a voucher to the payment");
    
    voucher.MapPatch("request", async Task<Results<NotFound, NoContent>>(Guid id, [AsParameters] Request request) => {
      var payment = await QueryGenerator(request).Where(p => p.Id == id)
        .FirstOrDefaultAsync();

      if (payment is null || payment.Paid || payment.HasVoucher || payment.RequestedVoucher) {
        return TypedResults.NotFound();
      }

      payment.RequestedVoucher = true;
      await request.Database.SaveChangesAsync();

      try {
        request.Notifications.Create(new Notification() {
          Title = $"Solicitud de voucher",
          Content =
            $"El usuario {payment.User?.Name} ha solicitado el voucher para el pago {payment.Name} creado el {payment.CreatedAt}",
          ScheduledAt = DateTime.UtcNow,
          MetaData = new NotificationMetaData {
            Key = payment.Id.ToString(),
            Type = NotificationType.PaymentVoucher
          },
          Targets = request.Database.Roles.Where(r => r.Id == 2)
            .SelectMany(r => r.Users)
            .ToList()
        }, request.Database).Wait();
      } catch (Exception e) {
        Console.WriteLine(e);
      }

      return TypedResults.NoContent();
    }).WithSwagger("Request Payment Voucher", "Requests a voucher for this payment");
    voucher.MapGet("",
        async Task<Results<NotFound, PhysicalFileHttpResult>>(Guid id, [AsParameters] Request request) => {
          var payment = await QueryGenerator(request).Where(p => p.Id == id)
            .FirstOrDefaultAsync();

          if (payment is null || !payment.Paid || !payment.HasVoucher) {
            return TypedResults.NotFound();
          }

          var filePath = Path.Combine(Environment.CurrentDirectory, "wwwroot", "vouchers", $"{payment.Id}.jpg");
          return TypedResults.PhysicalFile(filePath, "image/jpeg");
        }).Produces(StatusCodes.Status200OK, contentType: "image/jpeg")
      .WithSwagger("Get Payment Voucher", "Gets the payment voucher");

    return builder;
  }

  private static void ObjectUpdateDelegate(UpdateRequest<Guid, PaymentPayload> request, Payment item, out Action<Payment>? onSave) {
    onSave = null;
    item.Name = request.Payload.Name;
    item.Amount = request.Payload.Amount;
    item.User = request.GetValue<User>("user")!;
  }

  private static Payment Mapper(BodyRequest<PaymentPayload> request, out Action<Payment>? onSave) {
    onSave = (payment => {
      request.Notifications.Create(new Notification() {
        Title = $"Nuevo pago",
        Content = $"{payment.Amount} - {payment.Name}",
        ScheduledAt = DateTime.UtcNow,
        MetaData = new NotificationMetaData {
          Key = payment.Id.ToString(),
          Type = NotificationType.Payment
        },
        Targets = new List<User>() { payment.User }
      }, request.Database).Wait();
    });

    return new Payment {
      Name = request.Payload.Name,
      Amount = request.Payload.Amount,
      User = request.GetValue<User>("user")!
    };
  }
  private static IQueryable<Payment> QueryGenerator(Request context) {
    var user = context.GetValue<User>("user")!;
    var query = context.Database.Users
      .Where(u => !u.Deleted && u.Id == user.Id)
      .SelectMany(u => u.Payments)
      .Where(p => !p.Deleted);

    return query;
  }
}
