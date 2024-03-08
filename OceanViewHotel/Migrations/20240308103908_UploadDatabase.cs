using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OceanViewHotel.Migrations
{
    /// <inheritdoc />
    public partial class UploadDatabase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Clienti",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Cognome = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CodiceFiscale = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Citta = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Cellulare = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clienti", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Dipendenti",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Username = table.Column<string>(type: "nvarchar(20)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Dipendenti", x => x.Id);
                    table.UniqueConstraint("Unique_Username", x => x.Username);
                });

            migrationBuilder.CreateTable(
                name: "Pensione",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Descrizione = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Costo = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pensione", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ServPerPrenList",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Descrizione = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Costo = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ServPerPrenList", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TipologiaCamera",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Costo = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TipologiaCamera", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Camere",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdTipologiaCamera = table.Column<int>(type: "int", nullable: false),
                    Numero = table.Column<int>(type: "int", nullable: false),
                    TipologiaCameraId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Camere", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Camere_TipologiaCamera_TipologiaCameraId",
                        column: x => x.TipologiaCameraId,
                        principalTable: "TipologiaCamera",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Prenotazioni",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DataPrenotazione = table.Column<DateOnly>(type: "date", nullable: false),
                    DataCheckIn = table.Column<DateOnly>(type: "date", nullable: false),
                    DataCheckOut = table.Column<DateOnly>(type: "date", nullable: false),
                    Caparra = table.Column<double>(type: "float", nullable: false),
                    Tariffa = table.Column<double>(type: "float", nullable: false),
                    IdCamera = table.Column<int>(type: "int", nullable: false),
                    IdCliente = table.Column<int>(type: "int", nullable: false),
                    IdPensione = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Prenotazioni", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Prenotazioni_Camere_IdCamera",
                        column: x => x.IdCamera,
                        principalTable: "Camere",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Prenotazioni_Clienti_IdCliente",
                        column: x => x.IdCliente,
                        principalTable: "Clienti",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Prenotazioni_Pensione_IdPensione",
                        column: x => x.IdPensione,
                        principalTable: "Pensione",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Servizi",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DataServizio = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IdPrenotazione = table.Column<int>(type: "int", nullable: false),
                    IdServizio = table.Column<int>(type: "int", nullable: false),
                    ServPerPrenId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Servizi", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Servizi_Prenotazioni_IdPrenotazione",
                        column: x => x.IdPrenotazione,
                        principalTable: "Prenotazioni",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Servizi_ServPerPrenList_ServPerPrenId",
                        column: x => x.ServPerPrenId,
                        principalTable: "ServPerPrenList",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Camere_TipologiaCameraId",
                table: "Camere",
                column: "TipologiaCameraId");

            migrationBuilder.CreateIndex(
                name: "IX_Prenotazioni_IdCamera",
                table: "Prenotazioni",
                column: "IdCamera");

            migrationBuilder.CreateIndex(
                name: "IX_Prenotazioni_IdCliente",
                table: "Prenotazioni",
                column: "IdCliente");

            migrationBuilder.CreateIndex(
                name: "IX_Prenotazioni_IdPensione",
                table: "Prenotazioni",
                column: "IdPensione");

            migrationBuilder.CreateIndex(
                name: "IX_Servizi_IdPrenotazione",
                table: "Servizi",
                column: "IdPrenotazione");

            migrationBuilder.CreateIndex(
                name: "IX_Servizi_ServPerPrenId",
                table: "Servizi",
                column: "ServPerPrenId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Dipendenti");

            migrationBuilder.DropTable(
                name: "Servizi");

            migrationBuilder.DropTable(
                name: "Prenotazioni");

            migrationBuilder.DropTable(
                name: "ServPerPrenList");

            migrationBuilder.DropTable(
                name: "Camere");

            migrationBuilder.DropTable(
                name: "Clienti");

            migrationBuilder.DropTable(
                name: "Pensione");

            migrationBuilder.DropTable(
                name: "TipologiaCamera");
        }
    }
}
