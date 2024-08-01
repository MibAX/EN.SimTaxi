using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EN.SimTaxi.Mvc.Data.Migrations
{
    /// <inheritdoc />
    public partial class Bookings_paid : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsPaid",
                table: "Bookings",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsPaid",
                table: "Bookings");
        }
    }
}
