using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Prometheus.Migrations
{
    /// <inheritdoc />
    public partial class RemovedSomestuff : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RoleUser");

            migrationBuilder.DropColumn(
                name: "AllowsChildren",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "RoleId",
                table: "Users");

            migrationBuilder.CreateTable(
                name: "PostTag",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    RoleId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PostTag", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_PostTag_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PostTag_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "PostTag",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { 1, new Guid("b2bd9999-aa15-4b3c-bf9e-91eda6a0a610") });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2023, 4, 30, 1, 28, 10, 322, DateTimeKind.Utc).AddTicks(7180), new DateTime(2023, 4, 30, 1, 28, 10, 322, DateTimeKind.Utc).AddTicks(7180) });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2023, 4, 30, 1, 28, 10, 322, DateTimeKind.Utc).AddTicks(7180), new DateTime(2023, 4, 30, 1, 28, 10, 322, DateTimeKind.Utc).AddTicks(7180) });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2023, 4, 30, 1, 28, 10, 322, DateTimeKind.Utc).AddTicks(7180), new DateTime(2023, 4, 30, 1, 28, 10, 322, DateTimeKind.Utc).AddTicks(7180) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("b2bd9999-aa15-4b3c-bf9e-91eda6a0a610"),
                columns: new[] { "CreatedAt", "Password", "UpdatedAt" },
                values: new object[] { new DateTime(2023, 4, 30, 1, 28, 10, 322, DateTimeKind.Utc).AddTicks(7270), "$2a$13$MqCRiLu1CF63NmXBU8mwg.3n3feygXZouAKWh0.KWcQ8R9zdhlGSC", new DateTime(2023, 4, 30, 1, 28, 10, 322, DateTimeKind.Utc).AddTicks(7270) });

            migrationBuilder.CreateIndex(
                name: "IX_PostTag_RoleId",
                table: "PostTag",
                column: "RoleId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PostTag");

            migrationBuilder.AddColumn<bool>(
                name: "AllowsChildren",
                table: "Users",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "RoleId",
                table: "Users",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "RoleUser",
                columns: table => new
                {
                    RolesId = table.Column<int>(type: "integer", nullable: false),
                    UsersId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoleUser", x => new { x.RolesId, x.UsersId });
                    table.ForeignKey(
                        name: "FK_RoleUser_Roles_RolesId",
                        column: x => x.RolesId,
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RoleUser_Users_UsersId",
                        column: x => x.UsersId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2023, 4, 30, 1, 11, 57, 8, DateTimeKind.Utc).AddTicks(3200), new DateTime(2023, 4, 30, 1, 11, 57, 8, DateTimeKind.Utc).AddTicks(3200) });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2023, 4, 30, 1, 11, 57, 8, DateTimeKind.Utc).AddTicks(3200), new DateTime(2023, 4, 30, 1, 11, 57, 8, DateTimeKind.Utc).AddTicks(3200) });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2023, 4, 30, 1, 11, 57, 8, DateTimeKind.Utc).AddTicks(3210), new DateTime(2023, 4, 30, 1, 11, 57, 8, DateTimeKind.Utc).AddTicks(3210) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("b2bd9999-aa15-4b3c-bf9e-91eda6a0a610"),
                columns: new[] { "AllowsChildren", "CreatedAt", "Password", "RoleId", "UpdatedAt" },
                values: new object[] { false, new DateTime(2023, 4, 30, 1, 11, 57, 8, DateTimeKind.Utc).AddTicks(3340), "$2a$13$.OKGLcdXeqIG6CUHHr9dFu.5w11X0AOK8ENEvJlp4r9454MyJ6Ibm", 1, new DateTime(2023, 4, 30, 1, 11, 57, 8, DateTimeKind.Utc).AddTicks(3340) });

            migrationBuilder.CreateIndex(
                name: "IX_RoleUser_UsersId",
                table: "RoleUser",
                column: "UsersId");
        }
    }
}
