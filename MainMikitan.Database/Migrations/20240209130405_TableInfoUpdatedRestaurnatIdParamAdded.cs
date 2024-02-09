using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MainMikitan.Database.Migrations
{
    /// <inheritdoc />
    public partial class TableInfoUpdatedRestaurnatIdParamAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "RestaurantId",
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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RestaurantId",
                table: "TableInfo");

            migrationBuilder.UpdateData(
                table: "Customers",
                keyColumn: "Id",
                keyValue: -1,
                columns: new[] { "CreatedAt", "HashPassWord" },
                values: new object[] { new DateTime(2024, 2, 9, 16, 20, 7, 868, DateTimeKind.Local).AddTicks(4145), "AQAAAAIAAYagAAAAEP6qjOvaMc+7Rc8Z8K1BTf5F9oPShx2c4v+HqzrO1qpoxxlevR6E3ImXWSU1P2ounw==" });

            migrationBuilder.UpdateData(
                table: "Restaurant",
                keyColumn: "Id",
                keyValue: -1,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2024, 2, 9, 16, 20, 7, 926, DateTimeKind.Local).AddTicks(146), "AQAAAAIAAYagAAAAECq0bYgSYB6Ns8Tp3ix+Fu+sfAwt6v5he8wc1HCp7/irlv9jCuheuhvgQJHr4D17eA==" });
        }
    }
}
