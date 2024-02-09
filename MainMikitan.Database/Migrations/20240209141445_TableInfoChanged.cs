using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MainMikitan.Database.Migrations
{
    /// <inheritdoc />
    public partial class TableInfoChanged : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TableEnvironmentListId",
                table: "TableInfo");

            migrationBuilder.UpdateData(
                table: "Customers",
                keyColumn: "Id",
                keyValue: -1,
                columns: new[] { "CreatedAt", "HashPassWord" },
                values: new object[] { new DateTime(2024, 2, 9, 18, 14, 44, 851, DateTimeKind.Local).AddTicks(7483), "AQAAAAIAAYagAAAAEJxrZCWvKK8PHI7QL+8Sx1H/AiczejZvElTsSoBIaeawczKbqZzsD3p14hnHtFQ+dQ==" });

            migrationBuilder.UpdateData(
                table: "Restaurant",
                keyColumn: "Id",
                keyValue: -1,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2024, 2, 9, 18, 14, 44, 910, DateTimeKind.Local).AddTicks(9401), "AQAAAAIAAYagAAAAEEtrfO9Lb7HwKCaUsx9OyL4+PiimgWfPkMuogV54T56At8XKfWwbE0zx1fXohusTKQ==" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TableEnvironmentListId",
                table: "TableInfo",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "Customers",
                keyColumn: "Id",
                keyValue: -1,
                columns: new[] { "CreatedAt", "HashPassWord" },
                values: new object[] { new DateTime(2024, 2, 9, 17, 4, 5, 172, DateTimeKind.Local).AddTicks(6264), "AQAAAAIAAYagAAAAEFfmh3+es8bW4cJuEXL/HKGiuT1CQt6+BmyHeKMNNmsFiMDmOLCnqe3CvPStiqixfw==" });

            migrationBuilder.UpdateData(
                table: "Restaurant",
                keyColumn: "Id",
                keyValue: -1,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2024, 2, 9, 17, 4, 5, 231, DateTimeKind.Local).AddTicks(793), "AQAAAAIAAYagAAAAED/kC6jbnXKBVlvOnHy1xG1fc9zuFYP2wUvLxm63wjgrwBzagyasNpYlGTXhSFm9IQ==" });
        }
    }
}
