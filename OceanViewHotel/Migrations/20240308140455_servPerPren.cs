using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OceanViewHotel.Migrations
{
    /// <inheritdoc />
    public partial class servPerPren : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ServPerPrenId",
                table: "Servizi");

            migrationBuilder.CreateIndex(
                name: "IX_Servizi_IdServizio",
                table: "Servizi",
                column: "IdServizio");

            migrationBuilder.AddForeignKey(
                name: "FK_Servizi_ServPerPrenList_IdServizio",
                table: "Servizi",
                column: "IdServizio",
                principalTable: "ServPerPrenList",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
            migrationBuilder.RenameTable(
                name: "Pensione",
                newName: "Pensioni");
            migrationBuilder.RenameTable(
                    name: "TipologiaCamera",
                    newName: "TipologieCamera");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Servizi_ServPerPrenList_IdServizio",
                table: "Servizi");

            migrationBuilder.DropIndex(
                name: "IX_Servizi_IdServizio",
                table: "Servizi");

            migrationBuilder.AddColumn<int>(
                name: "ServPerPrenId",
                table: "Servizi",
                type: "int",
                nullable: false,
                defaultValue: 0);
            migrationBuilder.RenameTable(
                name: "Pensioni",
                newName: "Pensione");
            migrationBuilder.RenameTable(
                name: "TipologieCamera",
                newName: "TipologiaCamera");
        }
    }
}
