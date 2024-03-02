using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MainMikitan.Database.Migrations
{
    /// <inheritdoc />
    public partial class EventAndEventDetailsTablesAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Event",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RestaurantId = table.Column<int>(type: "int", nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Event", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EventDetails",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EventId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MaxAttendances = table.Column<int>(type: "int", nullable: false),
                    NeedsRegistration = table.Column<bool>(type: "bit", nullable: false),
                    TakeManagersRegistrationAddress = table.Column<bool>(type: "bit", nullable: false),
                    EventAddress = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    HasMusician = table.Column<bool>(type: "bit", nullable: false),
                    Musician = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    HasPresenter = table.Column<bool>(type: "bit", nullable: false),
                    Presenter = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    HasDancer = table.Column<bool>(type: "bit", nullable: false),
                    Dancer = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EventDetails", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Permission",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StaffId = table.Column<int>(type: "int", nullable: false),
                    PermissionId = table.Column<int>(type: "int", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdaterStaffId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Permission", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Event");

            migrationBuilder.DropTable(
                name: "EventDetails");

            migrationBuilder.DropTable(
                name: "Permission");

            migrationBuilder.DropColumn(
                name: "EmailConfirmation",
                table: "RestaurantStaff");

            migrationBuilder.DropColumn(
                name: "IsManager",
                table: "RestaurantStaff");

            migrationBuilder.DropColumn(
                name: "PhoneConfirmation",
                table: "RestaurantIntro");

            migrationBuilder.DropColumn(
                name: "AddressEng",
                table: "RestaurantInfo");

            migrationBuilder.DropColumn(
                name: "HallEndTime",
                table: "RestaurantInfo");

            migrationBuilder.DropColumn(
                name: "HallStartTime",
                table: "RestaurantInfo");

            migrationBuilder.DropColumn(
                name: "HasCoupe",
                table: "RestaurantInfo");

            migrationBuilder.DropColumn(
                name: "KitchenEndTime",
                table: "RestaurantInfo");

            migrationBuilder.DropColumn(
                name: "KitchenStartTime",
                table: "RestaurantInfo");

            migrationBuilder.DropColumn(
                name: "MusicEndTime",
                table: "RestaurantInfo");

            migrationBuilder.DropColumn(
                name: "MusicStartTime",
                table: "RestaurantInfo");

            migrationBuilder.DropColumn(
                name: "Rate",
                table: "RestaurantInfo");

            migrationBuilder.DropColumn(
                name: "BusinessNameEng",
                table: "Restaurant");

            migrationBuilder.DropColumn(
                name: "EmailConfirmation",
                table: "Restaurant");

            migrationBuilder.DropColumn(
                name: "StatusId",
                table: "Restaurant");

            migrationBuilder.RenameColumn(
                name: "UserNameHash",
                table: "RestaurantStaff",
                newName: "UsernameHash");

            migrationBuilder.RenameColumn(
                name: "PhoneConfirmation",
                table: "RestaurantStaff",
                newName: "IsConfirmed");

            migrationBuilder.RenameColumn(
                name: "FullNameGeo",
                table: "RestaurantStaff",
                newName: "NameGeo");

            migrationBuilder.RenameColumn(
                name: "FullNameEng",
                table: "RestaurantStaff",
                newName: "NameEng");

            migrationBuilder.RenameColumn(
                name: "UpdatedAt",
                table: "RestaurantInfo",
                newName: "UpdateAt");

            migrationBuilder.RenameColumn(
                name: "RegionId",
                table: "RestaurantInfo",
                newName: "TerraceQuantity");

            migrationBuilder.RenameColumn(
                name: "RateNumber",
                table: "RestaurantInfo",
                newName: "TableQuantity");

            migrationBuilder.RenameColumn(
                name: "PriceTypeId",
                table: "RestaurantInfo",
                newName: "CoupeQuantity");

            migrationBuilder.RenameColumn(
                name: "HasTerrace",
                table: "RestaurantInfo",
                newName: "TwoStepAuth");

            migrationBuilder.RenameColumn(
                name: "CreatedAt",
                table: "RestaurantInfo",
                newName: "CreateAt");

            migrationBuilder.RenameColumn(
                name: "OfficialEmail",
                table: "Restaurant",
                newName: "UserName");

            migrationBuilder.RenameColumn(
                name: "BusinessNameGeo",
                table: "Restaurant",
                newName: "PasswordHash");

            migrationBuilder.AlterColumn<int>(
                name: "UpdateUserId",
                table: "RestaurantStaff",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "RoleId",
                table: "RestaurantStaff",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ParentConfirmation",
                table: "RestaurantIntro",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ParentId",
                table: "RestaurantIntro",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "RestaurantOtpVerificationId",
                table: "RestaurantIntro",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<double>(
                name: "LocationY",
                table: "RestaurantInfo",
                type: "float",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AlterColumn<double>(
                name: "LocationX",
                table: "RestaurantInfo",
                type: "float",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AddColumn<short>(
                name: "HallEndHour",
                table: "RestaurantInfo",
                type: "smallint",
                nullable: false,
                defaultValue: (short)0);

            migrationBuilder.AddColumn<short>(
                name: "HallEndMinute",
                table: "RestaurantInfo",
                type: "smallint",
                nullable: false,
                defaultValue: (short)0);

            migrationBuilder.AddColumn<short>(
                name: "HallStartHour",
                table: "RestaurantInfo",
                type: "smallint",
                nullable: false,
                defaultValue: (short)0);

            migrationBuilder.AddColumn<short>(
                name: "HallStartMinute",
                table: "RestaurantInfo",
                type: "smallint",
                nullable: false,
                defaultValue: (short)0);

            migrationBuilder.AddColumn<short>(
                name: "KitchenEndHour",
                table: "RestaurantInfo",
                type: "smallint",
                nullable: false,
                defaultValue: (short)0);

            migrationBuilder.AddColumn<short>(
                name: "KitchenEndMinute",
                table: "RestaurantInfo",
                type: "smallint",
                nullable: false,
                defaultValue: (short)0);

            migrationBuilder.AddColumn<short>(
                name: "KitchenStartHour",
                table: "RestaurantInfo",
                type: "smallint",
                nullable: false,
                defaultValue: (short)0);

            migrationBuilder.AddColumn<short>(
                name: "KitchenStartMinute",
                table: "RestaurantInfo",
                type: "smallint",
                nullable: false,
                defaultValue: (short)0);

            migrationBuilder.AddColumn<short>(
                name: "MusicEndHour",
                table: "RestaurantInfo",
                type: "smallint",
                nullable: false,
                defaultValue: (short)0);

            migrationBuilder.AddColumn<short>(
                name: "MusicEndMinute",
                table: "RestaurantInfo",
                type: "smallint",
                nullable: false,
                defaultValue: (short)0);

            migrationBuilder.AddColumn<short>(
                name: "MusicStartHour",
                table: "RestaurantInfo",
                type: "smallint",
                nullable: false,
                defaultValue: (short)0);

            migrationBuilder.AddColumn<short>(
                name: "MusicStartMinute",
                table: "RestaurantInfo",
                type: "smallint",
                nullable: false,
                defaultValue: (short)0);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "Restaurant",
                type: "datetime2",
                nullable: true);

            migrationBuilder.InsertData(
                table: "Customers",
                columns: new[] { "Id", "CreatedAt", "EmailAddress", "EmailConfirmation", "FullName", "HashPassWord", "MobileNumber", "MobileNumberConfirmation", "StatusId" },
                values: new object[] { -1, new DateTime(2024, 2, 9, 18, 14, 44, 851, DateTimeKind.Local).AddTicks(7483), "customer@gmail.com", true, "Customer", "AQAAAAIAAYagAAAAEJxrZCWvKK8PHI7QL+8Sx1H/AiczejZvElTsSoBIaeawczKbqZzsD3p14hnHtFQ+dQ==", "000000000", true, 1 });

            migrationBuilder.InsertData(
                table: "Restaurant",
                columns: new[] { "Id", "CreatedAt", "PasswordHash", "UpdatedAt", "UserName" },
                values: new object[] { -1, new DateTime(2024, 2, 9, 18, 14, 44, 910, DateTimeKind.Local).AddTicks(9401), "AQAAAAIAAYagAAAAEEtrfO9Lb7HwKCaUsx9OyL4+PiimgWfPkMuogV54T56At8XKfWwbE0zx1fXohusTKQ==", null, "restaurant" });
        }
    }
}
