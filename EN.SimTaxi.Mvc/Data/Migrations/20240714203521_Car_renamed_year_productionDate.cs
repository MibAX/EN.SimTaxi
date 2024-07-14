using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EN.SimTaxi.Mvc.Data.Migrations
{
    /// <inheritdoc />
    public partial class Car_renamed_year_productionDate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Year",
                table: "Cars",
                newName: "ProductionDate");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ProductionDate",
                table: "Cars",
                newName: "Year");
        }
    }
}
