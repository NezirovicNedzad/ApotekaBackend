using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ApotekaBackend.Migrations
{
    /// <inheritdoc />
    public partial class AddedNewEnt : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TransakcijaDetalji_Transakcije_IdProdajeStandardni",
                table: "TransakcijaDetalji");

            migrationBuilder.DropIndex(
                name: "IX_TransakcijaDetalji_IdProdajeStandardni",
                table: "TransakcijaDetalji");

            migrationBuilder.DropColumn(
                name: "IdProdajeRecept",
                table: "TransakcijaDetalji");

            migrationBuilder.DropColumn(
                name: "IdProdajeStandardni",
                table: "TransakcijaDetalji");

            migrationBuilder.RenameColumn(
                name: "IdProdaje",
                table: "TransakcijaDetalji",
                newName: "IdTransakcije");

            migrationBuilder.CreateIndex(
                name: "IX_TransakcijaDetalji_IdTransakcije",
                table: "TransakcijaDetalji",
                column: "IdTransakcije");

            migrationBuilder.AddForeignKey(
                name: "FK_TransakcijaDetalji_Transakcije_IdTransakcije",
                table: "TransakcijaDetalji",
                column: "IdTransakcije",
                principalTable: "Transakcije",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TransakcijaDetalji_Transakcije_IdTransakcije",
                table: "TransakcijaDetalji");

            migrationBuilder.DropIndex(
                name: "IX_TransakcijaDetalji_IdTransakcije",
                table: "TransakcijaDetalji");

            migrationBuilder.RenameColumn(
                name: "IdTransakcije",
                table: "TransakcijaDetalji",
                newName: "IdProdaje");

            migrationBuilder.AddColumn<int>(
                name: "IdProdajeRecept",
                table: "TransakcijaDetalji",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "IdProdajeStandardni",
                table: "TransakcijaDetalji",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_TransakcijaDetalji_IdProdajeStandardni",
                table: "TransakcijaDetalji",
                column: "IdProdajeStandardni");

            migrationBuilder.AddForeignKey(
                name: "FK_TransakcijaDetalji_Transakcije_IdProdajeStandardni",
                table: "TransakcijaDetalji",
                column: "IdProdajeStandardni",
                principalTable: "Transakcije",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
