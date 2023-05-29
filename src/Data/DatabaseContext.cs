using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Query;
using Microsoft.EntityFrameworkCore.ValueGeneration;
using Prometheus.Data.Entities;
using Prometheus.Security;

namespace Prometheus.Data;

public sealed class DatabaseContext : DbContext {
  public DbSet<Announcement> Announcements => Set<Announcement>();

  public DbSet<Child> Children => Set<Child>();
  public DbSet<Match> Matches => Set<Match>();
  public DbSet<Notification> Notifications => Set<Notification>();
  public DbSet<PasswordRecovery> PasswordRecoveries => Set<PasswordRecovery>();
  public DbSet<Payment> Payments => Set<Payment>();
  public DbSet<PushToken> PushTokens => Set<PushToken>();
  public DbSet<RefreshToken> RefreshTokens => Set<RefreshToken>();
  public DbSet<Role> Roles => Set<Role>();
  public DbSet<Team> Teams => Set<Team>();
  public DbSet<User> Users => Set<User>();

  public DatabaseContext(DbContextOptions<DatabaseContext> dbContextOptions) : base(dbContextOptions) {
    ChangeTracker.StateChanged += OnStateChanged;
  }

  protected override void OnModelCreating(ModelBuilder modelBuilder) {
    base.OnModelCreating(modelBuilder);

    modelBuilder.Entity<Announcement>()
      .ToTable("Announcements")
      .Property(e => e.Content)
      .HasColumnType("text");
    
    modelBuilder.Entity<Child>()
      .ToTable("Children");

    modelBuilder.Entity<Notification>()
      .ToTable("Notifications")
      .Property(p => p.MetaData)
      .HasColumnType("jsonb");

    modelBuilder.Entity<Match>(match => {
      match.ToTable("Matches");
      match.HasOne(m => m.FirstTeam)
        .WithMany(t => t.FirstTeamMatches)
        .HasForeignKey(m => m.FirstTeamId);
      match.HasOne(m => m.SecondTeam)
        .WithMany(t => t.SecondTeamMatches)
        .HasForeignKey(m => m.SecondTeamId);
    });


    modelBuilder.Entity<Payment>()
      .ToTable("Payments")
      .Property(e => e.Id)
      .HasValueGenerator<GuidValueGenerator>();

    modelBuilder.Entity<RefreshToken>().ToTable("RefreshTokens")
      .HasIndex(r => r.Token);
    modelBuilder.Entity<PushToken>().ToTable("PushTokens")
      .HasKey(r => r.Token);
    modelBuilder.Entity<Role>().ToTable("Roles");
    modelBuilder.Entity<Team>().ToTable("Teams");

    modelBuilder.Entity<User>()
      .ToTable("Users")
      .HasIndex(e => e.Email)
      .IsUnique();
    

    modelBuilder.Entity<Role>().HasData(
      new Role { Id = 1, Slug = "administrator" },
      new Role { Id = 2, Slug = "teacher" },
      new Role { Id = 3, Slug = "parent" });

    modelBuilder.Entity<User>().HasData(new User {
      Id = Guid.Parse("b2bd9999-aa15-4b3c-bf9e-91eda6a0a610"),
      Email = "admin@saei.com",
      Name = "Administrator",
      LastName = "Administrator",
      Password = Crypto.HashPassword("Passw0rd"),
      Phone = "1234567890"
    });
    modelBuilder.Entity<User>()
      .HasMany(u => u.Roles)
      .WithMany(r => r.Users)
      .UsingEntity<Dictionary<string, object>>(
        "UserRoles",
        r => r.HasOne<Role>().WithMany().HasForeignKey("RoleId"),
        l => l.HasOne<User>().WithMany().HasForeignKey("UserId"),
        je =>
        {
          je.HasKey("UserId", "RoleId");
          je.HasData(
            new { UserId = Guid.Parse("b2bd9999-aa15-4b3c-bf9e-91eda6a0a610"), RoleId = 1 }
          );
        });
  }

  private static void OnStateChanged(object? sender, EntityStateChangedEventArgs args) {
    switch (args) {
      case { NewState: EntityState.Modified, Entry.Entity: ITimedEntity timedEntity }:
        timedEntity.UpdatedAt = DateTime.UtcNow;
        break;
    }
  }

  private static bool DeletedQueryFilter(IDeletableEntity entity) => !entity.Deleted;
}
