using System.Text.Json;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Prometheus.Data;
using Prometheus.Endpoints;
using Prometheus.ServiceExtensions;
using Prometheus.Services;
using Prometheus.Validators;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services
    .AddDatabase()
    .AddSmtp()
    .AddNotifications()
    .AddAuthenticationEngine()
    .AddValidators()
    .ConfigureRouting()
    .AddSwaggerDocumentation();

var app = builder.Build();

using (var scope = app.Services.CreateScope()) {
    await using var db = scope.ServiceProvider.GetRequiredService<DatabaseContext>();
    db.Database.Migrate();

    var notifications = scope.ServiceProvider.GetRequiredService<NotificationService>();
    await notifications.Initialize();
}

app.UseSwagger();
app.UseSwaggerUI();

var contentTypes = new FileExtensionContentTypeProvider();
contentTypes.Mappings[".apk"] = "application/vnd.android.package-archive";
app.UseFileServer(options: new FileServerOptions {
    EnableDirectoryBrowsing = false,
    StaticFileOptions = {
        ContentTypeProvider = contentTypes
    }
});
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.UseExceptionHandler(handlerBuilder => {
    handlerBuilder.Run(async context => {
        var ex = context.Features.Get<IExceptionHandlerFeature>();

        if (ex?.Error is BadHttpRequestException error) {
            await Results.ValidationProblem(new Dictionary<string, string[]>()
                { { "_", new []{ error.Message } } }).ExecuteAsync(context);
        } else {
            await Results.Problem().ExecuteAsync(context);
        }
    });
});

var root = app.MapGroup("")
    .AddEndpointFilter(async (context, next) => {
        foreach (var argument in context.Arguments) {
            if (argument is not IValidatable validatable) continue;
            var result = validatable.Validate();

            if (!result.IsValid) {
                return Results.ValidationProblem(result.ToDictionary());
            }
        }
        
        return await next(context);
    })
    .MapEndpoints();

app.Urls.Clear();
app.Urls.Add($"http://0.0.0.0:{Environment.GetEnvironmentVariable("PORT")}");
app.Run();
