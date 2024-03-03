using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MainMikitan.Database.Migrations
{
    /// <inheritdoc />
    public partial class GuestArrivedColumnAddedInReservation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateOnly>(
                name: "Date",
                table: "Reservations",
                type: "date",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AddColumn<bool>(
                name: "GuestArrived",
                table: "Reservations",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<TimeOnly>(
                name: "Time",
                table: "Reservations",
                type: "time",
                nullable: false,
                defaultValue: new TimeOnly(0, 0, 0));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "GuestArrived",
                table: "Reservations");

            migrationBuilder.DropColumn(
                name: "IsCompleted",
                table: "Reservations");

            migrationBuilder.DropColumn(
                name: "Time",
                table: "Reservations");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Date",
                table: "Reservations",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateOnly),
                oldType: "date");
        }
    }
}
