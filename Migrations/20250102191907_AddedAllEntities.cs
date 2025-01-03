using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ApotekaBackend.Migrations
{
    /// <inheritdoc />
    public partial class AddedAllEntities : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Transakcije",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    KlijentId = table.Column<int>(type: "int", nullable: false),
                    Cena = table.Column<int>(type: "int", nullable: false),
                    DatumTransakcije = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transakcije", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Transakcije_Klijenti_KlijentId",
                        column: x => x.KlijentId,
                        principalTable: "Klijenti",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TransakcijaDetalji",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdProdaje = table.Column<int>(type: "int", nullable: false),
                    IdLeka = table.Column<int>(type: "int", nullable: false),
                    Kolicina = table.Column<int>(type: "int", nullable: false),
                    ReceptId = table.Column<int>(type: "int", nullable: true),
                    IdProdajeStandardni = table.Column<int>(type: "int", nullable: true),
                    IdProdajeRecept = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TransakcijaDetalji", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TransakcijaDetalji_Lekovi_IdLeka",
                        column: x => x.IdLeka,
                        principalTable: "Lekovi",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TransakcijaDetalji_Recepti_ReceptId",
                        column: x => x.ReceptId,
                        principalTable: "Recepti",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TransakcijaDetalji_Transakcije_IdProdajeStandardni",
                        column: x => x.IdProdajeStandardni,
                        principalTable: "Transakcije",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TransakcijaDetalji_IdLeka",
                table: "TransakcijaDetalji",
                column: "IdLeka");

            migrationBuilder.CreateIndex(
                name: "IX_TransakcijaDetalji_IdProdajeStandardni",
                table: "TransakcijaDetalji",
                column: "IdProdajeStandardni");

            migrationBuilder.CreateIndex(
                name: "IX_TransakcijaDetalji_ReceptId",
                table: "TransakcijaDetalji",
                column: "ReceptId");

            migrationBuilder.CreateIndex(
                name: "IX_Transakcije_KlijentId",
                table: "Transakcije",
                column: "KlijentId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TransakcijaDetalji");

            migrationBuilder.DropTable(
                name: "Transakcije");
        }
    }
}
