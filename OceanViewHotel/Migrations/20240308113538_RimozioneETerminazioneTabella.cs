using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OceanViewHotel.Migrations
{
    /// <inheritdoc />
    public partial class RimozioneETerminazioneTabella : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Camere_TipologiaCamera_TipologiaCameraId",
                table: "Camere");

            migrationBuilder.DropIndex(
                name: "IX_Camere_TipologiaCameraId",
                table: "Camere");

            migrationBuilder.DropColumn(
                name: "TipologiaCameraId",
                table: "Camere");

            migrationBuilder.CreateIndex(
                name: "IX_Camere_IdTipologiaCamera",
                table: "Camere",
                column: "IdTipologiaCamera");

            migrationBuilder.AddForeignKey(
                name: "FK_Camere_TipologiaCamera_IdTipologiaCamera",
                table: "Camere",
                column: "IdTipologiaCamera",
                principalTable: "TipologiaCamera",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Camere_TipologiaCamera_IdTipologiaCamera",
                table: "Camere");

            migrationBuilder.DropIndex(
                name: "IX_Camere_IdTipologiaCamera",
                table: "Camere");

            migrationBuilder.AddColumn<int>(
                name: "TipologiaCameraId",
                table: "Camere",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Camere_TipologiaCameraId",
                table: "Camere",
                column: "TipologiaCameraId");

            migrationBuilder.AddForeignKey(
                name: "FK_Camere_TipologiaCamera_TipologiaCameraId",
                table: "Camere",
                column: "TipologiaCameraId",
                principalTable: "TipologiaCamera",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
