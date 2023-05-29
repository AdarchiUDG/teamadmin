using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Prometheus.Migrations
{
    /// <inheritdoc />
    public partial class AddedMetadataToNotifications : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Key",
                table: "Notifications");

            migrationBuilder.AddColumn<object>(
                name: "MetaData",
                table: "Notifications",
                type: "jsonb",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2023, 5, 8, 15, 53, 2, 795, DateTimeKind.Utc).AddTicks(8730), new DateTime(2023, 5, 8, 15, 53, 2, 795, DateTimeKind.Utc).AddTicks(8730) });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2023, 5, 8, 15, 53, 2, 795, DateTimeKind.Utc).AddTicks(8730), new DateTime(2023, 5, 8, 15, 53, 2, 795, DateTimeKind.Utc).AddTicks(8730) });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2023, 5, 8, 15, 53, 2, 795, DateTimeKind.Utc).AddTicks(8730), new DateTime(2023, 5, 8, 15, 53, 2, 795, DateTimeKind.Utc).AddTicks(8730) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("b2bd9999-aa15-4b3c-bf9e-91eda6a0a610"),
                columns: new[] { "CreatedAt", "Password", "UpdatedAt" },
                values: new object[] { new DateTime(2023, 5, 8, 15, 53, 2, 795, DateTimeKind.Utc).AddTicks(8840), "$2a$13$J6GP1vI/8qduKZgr618ovOMky1sdIlx/rqufn6c/82tPprmEwphOy", new DateTime(2023, 5, 8, 15, 53, 2, 795, DateTimeKind.Utc).AddTicks(8840) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MetaData",
                table: "Notifications");

            migrationBuilder.AddColumn<string>(
                name: "Key",
                table: "Notifications",
                type: "text",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2023, 5, 8, 14, 47, 58, 6, DateTimeKind.Utc).AddTicks(3580), new DateTime(2023, 5, 8, 14, 47, 58, 6, DateTimeKind.Utc).AddTicks(3580) });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2023, 5, 8, 14, 47, 58, 6, DateTimeKind.Utc).AddTicks(3580), new DateTime(2023, 5, 8, 14, 47, 58, 6, DateTimeKind.Utc).AddTicks(3580) });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2023, 5, 8, 14, 47, 58, 6, DateTimeKind.Utc).AddTicks(3580), new DateTime(2023, 5, 8, 14, 47, 58, 6, DateTimeKind.Utc).AddTicks(3580) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("b2bd9999-aa15-4b3c-bf9e-91eda6a0a610"),
                columns: new[] { "CreatedAt", "Password", "UpdatedAt" },
                values: new object[] { new DateTime(2023, 5, 8, 14, 47, 58, 6, DateTimeKind.Utc).AddTicks(3680), "$2a$13$ANAp8L2lNmCAvehRUmTWMeKHwg7pqc/HSMa7zrc7Laa2aX0idS6bS", new DateTime(2023, 5, 8, 14, 47, 58, 6, DateTimeKind.Utc).AddTicks(3680) });
        }
    }
}
