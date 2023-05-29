using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Prometheus.Migrations
{
    /// <inheritdoc />
    public partial class DeletedTournaments : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Matches_Teams_WinningTeamId",
                table: "Matches");

            migrationBuilder.DropForeignKey(
                name: "FK_Matches_Tournaments_TournamentId",
                table: "Matches");

            migrationBuilder.DropTable(
                name: "Tournaments");

            migrationBuilder.DropIndex(
                name: "IX_Matches_TournamentId",
                table: "Matches");

            migrationBuilder.DropIndex(
                name: "IX_Matches_WinningTeamId",
                table: "Matches");

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("1a11db7e-9387-42a6-a8cb-0ffefc9494a2"));

            migrationBuilder.DropColumn(
                name: "Round",
                table: "Matches");

            migrationBuilder.RenameColumn(
                name: "WinningTeamId",
                table: "Matches",
                newName: "SecondTeamScore");

            migrationBuilder.RenameColumn(
                name: "TournamentId",
                table: "Matches",
                newName: "FirstTeamScore");

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("b3d38bd5-4185-48a7-bdac-20139287941a"));

            migrationBuilder.RenameColumn(
                name: "SecondTeamScore",
                table: "Matches",
                newName: "WinningTeamId");

            migrationBuilder.RenameColumn(
                name: "FirstTeamScore",
                table: "Matches",
                newName: "TournamentId");

            migrationBuilder.AddColumn<int>(
                name: "Round",
                table: "Matches",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Tournaments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Deleted = table.Column<bool>(type: "boolean", nullable: false),
                    EndDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    Name = table.Column<string>(type: "text", nullable: false),
                    StartDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tournaments", x => x.Id);
                });

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
                name: "IX_Matches_TournamentId",
                table: "Matches",
                column: "TournamentId");

            migrationBuilder.CreateIndex(
                name: "IX_Matches_WinningTeamId",
                table: "Matches",
                column: "WinningTeamId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Matches_Teams_WinningTeamId",
                table: "Matches",
                column: "WinningTeamId",
                principalTable: "Teams",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Matches_Tournaments_TournamentId",
                table: "Matches",
                column: "TournamentId",
                principalTable: "Tournaments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
