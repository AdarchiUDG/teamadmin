using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Prometheus.Migrations
{
    /// <inheritdoc />
    public partial class AddedSomethingIdk : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("b3d38bd5-4185-48a7-bdac-20139287941a"));

            migrationBuilder.AddColumn<bool>(
                name: "HasVoucher",
                table: "Payments",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Payments",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2023, 4, 19, 1, 41, 50, 121, DateTimeKind.Utc).AddTicks(3580), new DateTime(2023, 4, 19, 1, 41, 50, 121, DateTimeKind.Utc).AddTicks(3580) });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2023, 4, 19, 1, 41, 50, 121, DateTimeKind.Utc).AddTicks(3580), new DateTime(2023, 4, 19, 1, 41, 50, 121, DateTimeKind.Utc).AddTicks(3580) });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2023, 4, 19, 1, 41, 50, 121, DateTimeKind.Utc).AddTicks(3580), new DateTime(2023, 4, 19, 1, 41, 50, 121, DateTimeKind.Utc).AddTicks(3580) });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "CreatedAt", "Deleted", "Email", "LastName", "Name", "Password", "Phone", "RoleId", "UpdatedAt" },
                values: new object[] { new Guid("b2bd9999-aa15-4b3c-bf9e-91eda6a0a610"), new DateTime(2023, 4, 19, 1, 41, 50, 121, DateTimeKind.Utc).AddTicks(3660), false, "admin@saei.com", "Administrator", "Administrator", "$2a$13$8Wg30VPnN.zqO/p6PNeob.6Do.FeiZNGbH4gK3L5.wluC3KYs.5i6", "1234567890", 1, new DateTime(2023, 4, 19, 1, 41, 50, 121, DateTimeKind.Utc).AddTicks(3660) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("b2bd9999-aa15-4b3c-bf9e-91eda6a0a610"));

            migrationBuilder.DropColumn(
                name: "HasVoucher",
                table: "Payments");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "Payments");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2023, 4, 18, 18, 23, 3, 429, DateTimeKind.Utc).AddTicks(5590), new DateTime(2023, 4, 18, 18, 23, 3, 429, DateTimeKind.Utc).AddTicks(5590) });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2023, 4, 18, 18, 23, 3, 429, DateTimeKind.Utc).AddTicks(5590), new DateTime(2023, 4, 18, 18, 23, 3, 429, DateTimeKind.Utc).AddTicks(5590) });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2023, 4, 18, 18, 23, 3, 429, DateTimeKind.Utc).AddTicks(5590), new DateTime(2023, 4, 18, 18, 23, 3, 429, DateTimeKind.Utc).AddTicks(5590) });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "CreatedAt", "Deleted", "Email", "LastName", "Name", "Password", "Phone", "RoleId", "UpdatedAt" },
                values: new object[] { new Guid("b3d38bd5-4185-48a7-bdac-20139287941a"), new DateTime(2023, 4, 18, 18, 23, 3, 429, DateTimeKind.Utc).AddTicks(5730), false, "admin@saei.com", "Administrator", "Administrator", "$2a$13$JrSsMzhZ.2Q6YbBP/TmO/Oct2tHMbbjv5ERZsm2HHdGWkZmW6DX4i", "1234567890", 1, new DateTime(2023, 4, 18, 18, 23, 3, 429, DateTimeKind.Utc).AddTicks(5730) });
        }
    }
}
