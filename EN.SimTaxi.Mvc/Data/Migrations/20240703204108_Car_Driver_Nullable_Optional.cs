﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EN.SimTaxi.Mvc.Data.Migrations
{
    /// <inheritdoc />
    public partial class Car_Driver_Nullable_Optional : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cars_Driver_DriverId",
                table: "Cars");

            migrationBuilder.AlterColumn<int>(
                name: "DriverId",
                table: "Cars",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Cars_Driver_DriverId",
                table: "Cars",
                column: "DriverId",
                principalTable: "Driver",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cars_Driver_DriverId",
                table: "Cars");

            migrationBuilder.AlterColumn<int>(
                name: "DriverId",
                table: "Cars",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Cars_Driver_DriverId",
                table: "Cars",
                column: "DriverId",
                principalTable: "Driver",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
