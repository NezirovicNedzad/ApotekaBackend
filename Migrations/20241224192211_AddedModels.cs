using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ApotekaBackend.Migrations
{
    /// <inheritdoc />
    public partial class AddedModels : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Klijenti",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Ime = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Prezime = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Adresa = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Telefon = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DatumRodjenja = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IdApotekara = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Klijenti", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Klijenti_AspNetUsers_IdApotekara",
                        column: x => x.IdApotekara,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Lekovi",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Naziv = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Opis = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Proizvodjac = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DatumIsteka = table.Column<DateTime>(type: "datetime2", nullable: false),
                    NaRecept = table.Column<bool>(type: "bit", nullable: false),
                    Cena = table.Column<int>(type: "int", nullable: false),
                    Kolicina = table.Column<int>(type: "int", nullable: false),
                    PhotoUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IdFarmaceuta = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Lekovi", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Lekovi_AspNetUsers_IdFarmaceuta",
                        column: x => x.IdFarmaceuta,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Recepti",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Zaglavlje = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Invocatio = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Ordinatio = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Subskricpija = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Uputstvo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IdLeka = table.Column<int>(type: "int", nullable: false),
                    IdFarmaceuta = table.Column<int>(type: "int", nullable: false),
                    IdKlijenta = table.Column<int>(type: "int", nullable: false),
                    IsDoctorPresribed = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Recepti", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Recepti_AspNetUsers_IdFarmaceuta",
                        column: x => x.IdFarmaceuta,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Recepti_Klijenti_IdKlijenta",
                        column: x => x.IdKlijenta,
                        principalTable: "Klijenti",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Recepti_Lekovi_IdLeka",
                        column: x => x.IdLeka,
                        principalTable: "Lekovi",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Klijenti_IdApotekara",
                table: "Klijenti",
                column: "IdApotekara");

            migrationBuilder.CreateIndex(
                name: "IX_Lekovi_IdFarmaceuta",
                table: "Lekovi",
                column: "IdFarmaceuta");

            migrationBuilder.CreateIndex(
                name: "IX_Recepti_IdFarmaceuta",
                table: "Recepti",
                column: "IdFarmaceuta");

            migrationBuilder.CreateIndex(
                name: "IX_Recepti_IdKlijenta",
                table: "Recepti",
                column: "IdKlijenta");

            migrationBuilder.CreateIndex(
                name: "IX_Recepti_IdLeka",
                table: "Recepti",
                column: "IdLeka");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Recepti");

            migrationBuilder.DropTable(
                name: "Klijenti");

            migrationBuilder.DropTable(
                name: "Lekovi");
        }
    }
}
