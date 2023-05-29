using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Prometheus.Migrations
{
    /// <inheritdoc />
    public partial class CreateMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("2fb89ca7-9ed0-4900-bca8-b6e718728218"));

            migrationBuilder.AddColumn<Guid>(
                name: "TrainerId",
                table: "Teams",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2023, 4, 18, 17, 56, 9, 839, DateTimeKind.Utc).AddTicks(7950), new DateTime(2023, 4, 18, 17, 56, 9, 839, DateTimeKind.Utc).AddTicks(7950) });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2023, 4, 18, 17, 56, 9, 839, DateTimeKind.Utc).AddTicks(7960), new DateTime(2023, 4, 18, 17, 56, 9, 839, DateTimeKind.Utc).AddTicks(7960) });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2023, 4, 18, 17, 56, 9, 839, DateTimeKind.Utc).AddTicks(7960), new DateTime(2023, 4, 18, 17, 56, 9, 839, DateTimeKind.Utc).AddTicks(7960) });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "CreatedAt", "Deleted", "Email", "LastName", "Name", "Password", "Phone", "RoleId", "UpdatedAt" },
                values: new object[] { new Guid("1a11db7e-9387-42a6-a8cb-0ffefc9494a2"), new DateTime(2023, 4, 18, 17, 56, 9, 839, DateTimeKind.Utc).AddTicks(8080), false, "admin@saei.com", "Administrator", "Administrator", "$2a$13$z64t2g9RfHuR1vTJQDtpY.C4o9/awKkkMASu5Y3YyUF.nomQTDPga", "1234567890", 1, new DateTime(2023, 4, 18, 17, 56, 9, 839, DateTimeKind.Utc).AddTicks(8080) });

            migrationBuilder.CreateIndex(
                name: "IX_Teams_TrainerId",
                table: "Teams",
                column: "TrainerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Teams_Users_TrainerId",
                table: "Teams",
                column: "TrainerId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Teams_Users_TrainerId",
                table: "Teams");

            migrationBuilder.DropIndex(
                name: "IX_Teams_TrainerId",
                table: "Teams");

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("1a11db7e-9387-42a6-a8cb-0ffefc9494a2"));

            migrationBuilder.DropColumn(
                name: "TrainerId",
                table: "Teams");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2023, 4, 17, 6, 4, 56, 97, DateTimeKind.Utc).AddTicks(3050), new DateTime(2023, 4, 17, 6, 4, 56, 97, DateTimeKind.Utc).AddTicks(3050) });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2023, 4, 17, 6, 4, 56, 97, DateTimeKind.Utc).AddTicks(3050), new DateTime(2023, 4, 17, 6, 4, 56, 97, DateTimeKind.Utc).AddTicks(3050) });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2023, 4, 17, 6, 4, 56, 97, DateTimeKind.Utc).AddTicks(3050), new DateTime(2023, 4, 17, 6, 4, 56, 97, DateTimeKind.Utc).AddTicks(3050) });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "CreatedAt", "Deleted", "Email", "LastName", "Name", "Password", "Phone", "RoleId", "UpdatedAt" },
                values: new object[] { new Guid("2fb89ca7-9ed0-4900-bca8-b6e718728218"), new DateTime(2023, 4, 17, 6, 4, 56, 97, DateTimeKind.Utc).AddTicks(3150), false, "admin@saei.com", "Administrator", "Administrator", "$2a$13$f0UptkfS7SoyGNrYRFSVUO1lYSdH45/nhRowu4C07OMmGX7n7FzPe", "1234567890", 1, new DateTime(2023, 4, 17, 6, 4, 56, 97, DateTimeKind.Utc).AddTicks(3150) });
        }
    }
}
