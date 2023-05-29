using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Prometheus.Migrations
{
    /// <inheritdoc />
    public partial class AddedRequestedVoucher : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "RequestedVoucher",
                table: "Payments",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2023, 5, 13, 16, 12, 11, 843, DateTimeKind.Utc).AddTicks(8440), new DateTime(2023, 5, 13, 16, 12, 11, 843, DateTimeKind.Utc).AddTicks(8440) });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2023, 5, 13, 16, 12, 11, 843, DateTimeKind.Utc).AddTicks(8440), new DateTime(2023, 5, 13, 16, 12, 11, 843, DateTimeKind.Utc).AddTicks(8440) });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2023, 5, 13, 16, 12, 11, 843, DateTimeKind.Utc).AddTicks(8470), new DateTime(2023, 5, 13, 16, 12, 11, 843, DateTimeKind.Utc).AddTicks(8470) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("b2bd9999-aa15-4b3c-bf9e-91eda6a0a610"),
                columns: new[] { "CreatedAt", "Password", "UpdatedAt" },
                values: new object[] { new DateTime(2023, 5, 13, 16, 12, 11, 843, DateTimeKind.Utc).AddTicks(8540), "$2a$13$FNIoh/f0epT/ct7mn9KNt.HGvLSW1867yjttG5KeCjGB5vw23z/sK", new DateTime(2023, 5, 13, 16, 12, 11, 843, DateTimeKind.Utc).AddTicks(8540) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RequestedVoucher",
                table: "Payments");

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
    }
}
