using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApplication1.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddScreeningsAndBookings : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bookings_Movies_MovieId",
                table: "Bookings");

            migrationBuilder.RenameColumn(
                name: "MovieId",
                table: "Bookings",
                newName: "ScreeningId");

            migrationBuilder.RenameIndex(
                name: "IX_Bookings_MovieId",
                table: "Bookings",
                newName: "IX_Bookings_ScreeningId");

            migrationBuilder.CreateTable(
                name: "Screenings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MovieId = table.Column<int>(type: "int", nullable: false),
                    Showtime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    HallName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Screenings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Screenings_Movies_MovieId",
                        column: x => x.MovieId,
                        principalTable: "Movies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Screenings_MovieId",
                table: "Screenings",
                column: "MovieId");

            migrationBuilder.AddForeignKey(
                name: "FK_Bookings_Screenings_ScreeningId",
                table: "Bookings",
                column: "ScreeningId",
                principalTable: "Screenings",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bookings_Screenings_ScreeningId",
                table: "Bookings");

            migrationBuilder.DropTable(
                name: "Screenings");

            migrationBuilder.RenameColumn(
                name: "ScreeningId",
                table: "Bookings",
                newName: "MovieId");

            migrationBuilder.RenameIndex(
                name: "IX_Bookings_ScreeningId",
                table: "Bookings",
                newName: "IX_Bookings_MovieId");

            migrationBuilder.AddForeignKey(
                name: "FK_Bookings_Movies_MovieId",
                table: "Bookings",
                column: "MovieId",
                principalTable: "Movies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
