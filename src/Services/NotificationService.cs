using System.Threading.Channels;
using System.Timers;
using Expo.Server.Client;
using Expo.Server.Models;
using Microsoft.EntityFrameworkCore;
using Prometheus.Data;
using Prometheus.Data.Entities;

namespace Prometheus.Services; 

public class NotificationService : BackgroundService {
  private readonly IServiceProvider _provider;
  private readonly Channel<Notification> _channel;
  public NotificationService(IServiceProvider provider) {
    _channel = Channel.CreateUnbounded<Notification>();
    _provider = provider;
  }

  public async Task Initialize() {
    using var scope = _provider.CreateScope();
    var context = scope.ServiceProvider.GetRequiredService<DatabaseContext>();
    var notifications = await context.Notifications
      .Where(n => !n.Sent)
      .Include(n => n.Targets)
      .AsSplitQuery()
      .AsNoTracking()
      .ToListAsync();

    foreach (var notification in notifications) {
      Add(notification);
    }
  }

  public async Task Create(Notification notification, DatabaseContext context) {
    if (notification.ScheduledAt <= DateTime.UtcNow) {
      notification.Sent = true;
    }
    
    await context.AddAsync(notification);
    await context.SaveChangesAsync();

    Add(notification);
  }

  private void Add(Notification notification) {
    Console.WriteLine(new {
      not = notification.ScheduledAt,
      now = DateTime.UtcNow,
      lessthan = notification.ScheduledAt <= DateTime.UtcNow
    });
    
    _channel.Writer.TryWrite(notification);
  }

  private static void Schedule(Func<Task> handler, DateTime scheduledAt) {
    var interval = scheduledAt > DateTime.UtcNow ? scheduledAt.Subtract(DateTime.UtcNow) : TimeSpan.FromSeconds(0.5f);
    var timer = new System.Timers.Timer() {
      AutoReset = false,
      Enabled = false,
      Interval = interval.TotalMilliseconds,
    };
    timer.Elapsed += async (_, _) => {
      await handler();
      timer.Dispose();
    };
    timer.Enabled = true;
  }

  protected override async Task ExecuteAsync(CancellationToken ct) {
    while (await _channel.Reader.WaitToReadAsync(ct)) {
      while (_channel.Reader.TryRead(out var notification)) {
        Schedule(() => Send(notification), notification.ScheduledAt);
      }
    }
  }

  private async Task Send(Notification notification) {
    var targets = notification.Targets.Select(u => u.Id);
    using var scope = _provider.CreateScope();
    var context = scope.ServiceProvider.GetRequiredService<DatabaseContext>();
    var client = new PushApiClient();
    var tokens = context.PushTokens
      .Where(pt => targets.Contains(pt.UserId))
      .Select(pt => pt.Token);

    if (!tokens.Any()) {
      return;
    }
    var result = await client.PushSendAsync(new PushTicketRequest() {
      PushTo = await tokens.ToListAsync(),
      PushTitle = notification.Title,
      PushSubTitle = notification.MetaData?.Subtitle ?? "",
      PushBody = notification.Content
    });
    
    Console.WriteLine(new { isNullNotification = result is null });

    if (result is null) {
      return;
    }

    if (result.PushTicketErrors is not null) {
      foreach (var error in result.PushTicketErrors) {
        Console.WriteLine($"Error: {error.ErrorCode} - {error.ErrorMessage}");
      }
    }
    

    await context.Notifications.Where(n => n.Id == notification.Id)
      .ExecuteUpdateAsync(p => p.SetProperty(n => n.Sent, true));

    Schedule(() => CheckReceipts(result.PushTicketStatuses), DateTime.UtcNow.Add(TimeSpan.FromMinutes(15)));
  }

  private async Task CheckReceipts(IEnumerable<PushTicketStatus> tickets) {
    using var scope = _provider.CreateScope();
    var context = scope.ServiceProvider.GetRequiredService<DatabaseContext>();
    var client = new PushApiClient();
    
    var receipts = await client.PushGetReceiptsAsync(new PushReceiptRequest {
      PushTicketIds = tickets.Select(ps => ps.TicketId).ToList()
    });

    var pushTokensToDelete = new List<string>();
    foreach (var (key, value) in receipts.PushTicketReceipts) {
      if (value.DeliveryStatus != "ok") {
        pushTokensToDelete.Add(key);
      }
      Console.WriteLine($"TicketId & Delivery Status: {key} {value.DeliveryStatus} {value.DeliveryMessage}");
    }
    
    await context.PushTokens.Where(pt => pushTokensToDelete.Contains(pt.Token))
      .ExecuteDeleteAsync();
  }
}
