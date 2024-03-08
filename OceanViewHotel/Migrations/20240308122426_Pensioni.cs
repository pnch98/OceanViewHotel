using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OceanViewHotel.Migrations
{
    /// <inheritdoc />
    public partial class Pensioni : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Prenotazioni_Pensione_IdPensione",
                table: "Prenotazioni");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Pensione",
                table: "Pensione");

            migrationBuilder.RenameTable(
                name: "Pensione",
                newName: "Pensioni");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Pensioni",
                table: "Pensioni",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Prenotazioni_Pensioni_IdPensione",
                table: "Prenotazioni",
                column: "IdPensione",
                principalTable: "Pensioni",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Prenotazioni_Pensioni_IdPensione",
                table: "Prenotazioni");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Pensioni",
                table: "Pensioni");

            migrationBuilder.RenameTable(
                name: "Pensioni",
                newName: "Pensione");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Pensione",
                table: "Pensione",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Prenotazioni_Pensione_IdPensione",
                table: "Prenotazioni",
                column: "IdPensione",
                principalTable: "Pensione",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
