using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Prometheus.Migrations
{
    /// <inheritdoc />
    public partial class DidSomething : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Sent",
                table: "Notifications",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2023, 5, 9, 4, 35, 56, 799, DateTimeKind.Utc).AddTicks(1040), new DateTime(2023, 5, 9, 4, 35, 56, 799, DateTimeKind.Utc).AddTicks(1040) });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2023, 5, 9, 4, 35, 56, 799, DateTimeKind.Utc).AddTicks(1040), new DateTime(2023, 5, 9, 4, 35, 56, 799, DateTimeKind.Utc).AddTicks(1040) });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2023, 5, 9, 4, 35, 56, 799, DateTimeKind.Utc).AddTicks(1040), new DateTime(2023, 5, 9, 4, 35, 56, 799, DateTimeKind.Utc).AddTicks(1040) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("b2bd9999-aa15-4b3c-bf9e-91eda6a0a610"),
                columns: new[] { "CreatedAt", "Password", "UpdatedAt" },
                values: new object[] { new DateTime(2023, 5, 9, 4, 35, 56, 799, DateTimeKind.Utc).AddTicks(1230), "$2a$13$ObOJZtS9IEp/HesyeNGi.eBq9JflADuis2hJ1DSSw88yh9FXeSi.a", new DateTime(2023, 5, 9, 4, 35, 56, 799, DateTimeKind.Utc).AddTicks(1230) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Sent",
                table: "Notifications");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2023, 5, 8, 15, 59, 30, 382, DateTimeKind.Utc).AddTicks(6900), new DateTime(2023, 5, 8, 15, 59, 30, 382, DateTimeKind.Utc).AddTicks(6900) });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2023, 5, 8, 15, 59, 30, 382, DateTimeKind.Utc).AddTicks(6910), new DateTime(2023, 5, 8, 15, 59, 30, 382, DateTimeKind.Utc).AddTicks(6910) });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2023, 5, 8, 15, 59, 30, 382, DateTimeKind.Utc).AddTicks(6910), new DateTime(2023, 5, 8, 15, 59, 30, 382, DateTimeKind.Utc).AddTicks(6910) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("b2bd9999-aa15-4b3c-bf9e-91eda6a0a610"),
                columns: new[] { "CreatedAt", "Password", "UpdatedAt" },
                values: new object[] { new DateTime(2023, 5, 8, 15, 59, 30, 382, DateTimeKind.Utc).AddTicks(7000), "$2a$13$8k92L3TSx2eMsz9eAtiOfuzKZlp/HOpWTjUN3Vqo0Zqk6Tp9Pm/Ga", new DateTime(2023, 5, 8, 15, 59, 30, 382, DateTimeKind.Utc).AddTicks(7000) });
        }
    }
}
