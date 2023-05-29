using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Prometheus.Migrations
{
    /// <inheritdoc />
    public partial class MaybeFixedDates : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
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
    }
}
