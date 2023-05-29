using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Prometheus.Migrations
{
    /// <inheritdoc />
    public partial class AddSomethingOrseomidk : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("b2bd9999-aa15-4b3c-bf9e-91eda6a0a610"));

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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
    }
}
