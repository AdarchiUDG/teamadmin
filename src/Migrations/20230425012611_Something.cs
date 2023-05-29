using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Prometheus.Migrations
{
    /// <inheritdoc />
    public partial class Something : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("476a0824-2824-49ef-ba9c-18cdf30eb56e"));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2023, 4, 25, 1, 26, 10, 750, DateTimeKind.Utc).AddTicks(7300), new DateTime(2023, 4, 25, 1, 26, 10, 750, DateTimeKind.Utc).AddTicks(7300) });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2023, 4, 25, 1, 26, 10, 750, DateTimeKind.Utc).AddTicks(7310), new DateTime(2023, 4, 25, 1, 26, 10, 750, DateTimeKind.Utc).AddTicks(7310) });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2023, 4, 25, 1, 26, 10, 750, DateTimeKind.Utc).AddTicks(7310), new DateTime(2023, 4, 25, 1, 26, 10, 750, DateTimeKind.Utc).AddTicks(7310) });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "CreatedAt", "Deleted", "Email", "LastName", "Name", "Password", "Phone", "RoleId", "UpdatedAt" },
                values: new object[] { new Guid("873a9236-0e62-412c-b738-c2da6413cd2d"), new DateTime(2023, 4, 25, 1, 26, 10, 750, DateTimeKind.Utc).AddTicks(7400), false, "admin@saei.com", "Administrator", "Administrator", "$2a$13$E39SWmc3yZf7xJVfCJZ38.frnEoRg87xx0lNAsMDgl2JPa5i5cLYW", "1234567890", 1, new DateTime(2023, 4, 25, 1, 26, 10, 750, DateTimeKind.Utc).AddTicks(7410) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("873a9236-0e62-412c-b738-c2da6413cd2d"));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2023, 4, 21, 0, 40, 40, 313, DateTimeKind.Utc).AddTicks(8130), new DateTime(2023, 4, 21, 0, 40, 40, 313, DateTimeKind.Utc).AddTicks(8130) });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2023, 4, 21, 0, 40, 40, 313, DateTimeKind.Utc).AddTicks(8130), new DateTime(2023, 4, 21, 0, 40, 40, 313, DateTimeKind.Utc).AddTicks(8130) });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2023, 4, 21, 0, 40, 40, 313, DateTimeKind.Utc).AddTicks(8140), new DateTime(2023, 4, 21, 0, 40, 40, 313, DateTimeKind.Utc).AddTicks(8140) });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "CreatedAt", "Deleted", "Email", "LastName", "Name", "Password", "Phone", "RoleId", "UpdatedAt" },
                values: new object[] { new Guid("476a0824-2824-49ef-ba9c-18cdf30eb56e"), new DateTime(2023, 4, 21, 0, 40, 40, 313, DateTimeKind.Utc).AddTicks(8220), false, "admin@saei.com", "Administrator", "Administrator", "$2a$13$VpWsZOTpKX2/FlM2TuVtbe4IysojXr32/N91EqChUUiXE0LgFQm6y", "1234567890", 1, new DateTime(2023, 4, 21, 0, 40, 40, 313, DateTimeKind.Utc).AddTicks(8220) });
        }
    }
}
