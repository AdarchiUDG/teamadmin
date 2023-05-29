using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Prometheus.Migrations
{
    /// <inheritdoc />
    public partial class RenamedRolesTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PostTag");

            migrationBuilder.CreateTable(
                name: "UserRoles",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    RoleId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_UserRoles_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserRoles_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

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

            migrationBuilder.InsertData(
                table: "UserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { 1, new Guid("b2bd9999-aa15-4b3c-bf9e-91eda6a0a610") });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("b2bd9999-aa15-4b3c-bf9e-91eda6a0a610"),
                columns: new[] { "CreatedAt", "Password", "UpdatedAt" },
                values: new object[] { new DateTime(2023, 4, 30, 2, 2, 24, 317, DateTimeKind.Utc).AddTicks(3930), "$2a$13$nkXeGR.dW1Fc2feS6zRJX.iH3k4n6sKA1upejFaGRLqJndegGLa2K", new DateTime(2023, 4, 30, 2, 2, 24, 317, DateTimeKind.Utc).AddTicks(3930) });

            migrationBuilder.CreateIndex(
                name: "IX_UserRoles_RoleId",
                table: "UserRoles",
                column: "RoleId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserRoles");

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
    }
}
