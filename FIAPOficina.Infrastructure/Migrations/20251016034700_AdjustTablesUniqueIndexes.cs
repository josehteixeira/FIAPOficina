using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FIAPOficina.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AdjustTablesUniqueIndexes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Vehicles_Plate",
                table: "Vehicles");

            migrationBuilder.DropIndex(
                name: "IX_Clients_Address",
                table: "Clients");

            migrationBuilder.CreateIndex(
                name: "IX_Vehicles_Plate",
                table: "Vehicles",
                column: "Plate",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Clients_Identifier",
                table: "Clients",
                column: "Identifier",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Vehicles_Plate",
                table: "Vehicles");

            migrationBuilder.DropIndex(
                name: "IX_Clients_Identifier",
                table: "Clients");

            migrationBuilder.CreateIndex(
                name: "IX_Vehicles_Plate",
                table: "Vehicles",
                column: "Plate");

            migrationBuilder.CreateIndex(
                name: "IX_Clients_Address",
                table: "Clients",
                column: "Address",
                unique: true);
        }
    }
}
