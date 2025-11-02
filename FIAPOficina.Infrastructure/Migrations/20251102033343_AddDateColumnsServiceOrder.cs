using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FIAPOficina.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddDateColumnsServiceOrder : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "ApprovedOn",
                table: "ServiceOrders",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedOn",
                table: "ServiceOrders",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "FinishedOn",
                table: "ServiceOrders",
                type: "timestamp with time zone",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ApprovedOn",
                table: "ServiceOrders");

            migrationBuilder.DropColumn(
                name: "CreatedOn",
                table: "ServiceOrders");

            migrationBuilder.DropColumn(
                name: "FinishedOn",
                table: "ServiceOrders");
        }
    }
}
