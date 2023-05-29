using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Prometheus.Migrations
{
    /// <inheritdoc />
    public partial class AddedUpdateChildren : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("873a9236-0e62-412c-b738-c2da6413cd2d"));

            migrationBuilder.AddColumn<bool>(
                name: "AllowsChildren",
                table: "Users",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2023, 4, 27, 15, 16, 19, 116, DateTimeKind.Utc).AddTicks(6070), new DateTime(2023, 4, 27, 15, 16, 19, 116, DateTimeKind.Utc).AddTicks(6070) });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2023, 4, 27, 15, 16, 19, 116, DateTimeKind.Utc).AddTicks(6070), new DateTime(2023, 4, 27, 15, 16, 19, 116, DateTimeKind.Utc).AddTicks(6070) });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2023, 4, 27, 15, 16, 19, 116, DateTimeKind.Utc).AddTicks(6070), new DateTime(2023, 4, 27, 15, 16, 19, 116, DateTimeKind.Utc).AddTicks(6070) });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "AllowsChildren", "CreatedAt", "Deleted", "Email", "LastName", "Name", "Password", "Phone", "RoleId", "UpdatedAt" },
                values: new object[] { new Guid("b2bd9999-aa15-4b3c-bf9e-91eda6a0a610"), false, new DateTime(2023, 4, 27, 15, 16, 19, 116, DateTimeKind.Utc).AddTicks(6150), false, "admin@saei.com", "Administrator", "Administrator", "$2a$13$w1OkHXW4ji2779403PN3FO6RCeMRnD.C0pV84f0UFg9SAha7u2522", "1234567890", 1, new DateTime(2023, 4, 27, 15, 16, 19, 116, DateTimeKind.Utc).AddTicks(6160) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("b2bd9999-aa15-4b3c-bf9e-91eda6a0a610"));

            migrationBuilder.DropColumn(
                name: "AllowsChildren",
                table: "Users");

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
    }
}
