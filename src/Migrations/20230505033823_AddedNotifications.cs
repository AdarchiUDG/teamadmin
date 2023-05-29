using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Prometheus.Migrations
{
    /// <inheritdoc />
    public partial class AddedNotifications : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "ScheduledAt",
                table: "Notifications",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "ScheduledAt",
                table: "Announcements",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2023, 5, 5, 3, 38, 22, 523, DateTimeKind.Utc).AddTicks(5180), new DateTime(2023, 5, 5, 3, 38, 22, 523, DateTimeKind.Utc).AddTicks(5180) });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2023, 5, 5, 3, 38, 22, 523, DateTimeKind.Utc).AddTicks(5190), new DateTime(2023, 5, 5, 3, 38, 22, 523, DateTimeKind.Utc).AddTicks(5190) });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2023, 5, 5, 3, 38, 22, 523, DateTimeKind.Utc).AddTicks(5190), new DateTime(2023, 5, 5, 3, 38, 22, 523, DateTimeKind.Utc).AddTicks(5190) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("b2bd9999-aa15-4b3c-bf9e-91eda6a0a610"),
                columns: new[] { "CreatedAt", "Password", "UpdatedAt" },
                values: new object[] { new DateTime(2023, 5, 5, 3, 38, 22, 523, DateTimeKind.Utc).AddTicks(5300), "$2a$13$RiGoJAP2G6F2MdGkDnXR2OisHxAvQD/Rxxhuh6Wf.AOqSaqVKu2fS", new DateTime(2023, 5, 5, 3, 38, 22, 523, DateTimeKind.Utc).AddTicks(5300) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ScheduledAt",
                table: "Notifications");

            migrationBuilder.DropColumn(
                name: "ScheduledAt",
                table: "Announcements");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2023, 4, 30, 2, 2, 24, 317, DateTimeKind.Utc).AddTicks(3840), new DateTime(2023, 4, 30, 2, 2, 24, 317, DateTimeKind.Utc).AddTicks(3840) });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2023, 4, 30, 2, 2, 24, 317, DateTimeKind.Utc).AddTicks(3840), new DateTime(2023, 4, 30, 2, 2, 24, 317, DateTimeKind.Utc).AddTicks(3840) });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2023, 4, 30, 2, 2, 24, 317, DateTimeKind.Utc).AddTicks(3850), new DateTime(2023, 4, 30, 2, 2, 24, 317, DateTimeKind.Utc).AddTicks(3850) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("b2bd9999-aa15-4b3c-bf9e-91eda6a0a610"),
                columns: new[] { "CreatedAt", "Password", "UpdatedAt" },
                values: new object[] { new DateTime(2023, 4, 30, 2, 2, 24, 317, DateTimeKind.Utc).AddTicks(3930), "$2a$13$nkXeGR.dW1Fc2feS6zRJX.iH3k4n6sKA1upejFaGRLqJndegGLa2K", new DateTime(2023, 4, 30, 2, 2, 24, 317, DateTimeKind.Utc).AddTicks(3930) });
        }
    }
}
