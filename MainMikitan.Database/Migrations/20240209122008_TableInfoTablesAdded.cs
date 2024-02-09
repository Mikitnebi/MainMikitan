using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MainMikitan.Database.Migrations
{
    /// <inheritdoc />
    public partial class TableInfoTablesAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TableEnvironment",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TableId = table.Column<int>(type: "int", nullable: false),
                    EnvironmentId = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TableEnvironment", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TableInfo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TableNumber = table.Column<int>(type: "int", nullable: false),
                    MaxPlace = table.Column<int>(type: "int", nullable: false),
                    MinPlace = table.Column<int>(type: "int", nullable: false),
                    TableEnvironmentListId = table.Column<int>(type: "int", nullable: false),
                    TableType = table.Column<int>(type: "int", nullable: false),
                    XCoordinate = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    YCoordinate = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TableInfo", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Reservations");

            migrationBuilder.DropTable(
                name: "RestaurantBranchingCodeLogs");

            migrationBuilder.DropTable(
                name: "Sector");

            migrationBuilder.DropTable(
                name: "TableEnvironment");

            migrationBuilder.DropTable(
                name: "TableInfo");

            migrationBuilder.DropColumn(
                name: "OperationId",
                table: "OtpLogIntro");

            migrationBuilder.DropColumn(
                name: "UpdateUserId",
                table: "DishInfo");

            migrationBuilder.RenameColumn(
                name: "NameGeo",
                table: "Dictionary",
                newName: "GeoName");

            migrationBuilder.RenameColumn(
                name: "NameEng",
                table: "Dictionary",
                newName: "EngName");

            migrationBuilder.AddColumn<string>(
                name: "FullName",
                table: "CustomerInfo",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "Customers",
                keyColumn: "Id",
                keyValue: -1,
                columns: new[] { "CreatedAt", "HashPassWord" },
                values: new object[] { new DateTime(2023, 12, 30, 18, 28, 21, 608, DateTimeKind.Local).AddTicks(4250), "AQAAAAIAAYagAAAAEK96rANMOlqhcxa9DPwbr/ohsHLBUR0srhf/QhVteJkv89yb7Po5bSz6zxIMi61xxA==" });

            migrationBuilder.UpdateData(
                table: "Restaurant",
                keyColumn: "Id",
                keyValue: -1,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2023, 12, 30, 18, 28, 21, 645, DateTimeKind.Local).AddTicks(7700), "AQAAAAIAAYagAAAAEEpDMIvjmSLmdFQTgljyG6MkOHPDtbN5I/DH9ZLbadzPHJ53OWwwgPN/2oTGrSFURQ==" });
        }
    }
}
