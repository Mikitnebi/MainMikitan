using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MainMikitan.Database.Migrations
{
    /// <inheritdoc />
    public partial class EventDetailsEngsAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Name",
                table: "EventDetails",
                newName: "NameGeo");

            migrationBuilder.RenameColumn(
                name: "EventAddress",
                table: "EventDetails",
                newName: "EventAddressGeo");

            migrationBuilder.RenameColumn(
                name: "Description",
                table: "EventDetails",
                newName: "NameEng");

            migrationBuilder.AddColumn<string>(
                name: "DescriptionEng",
                table: "EventDetails",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "DescriptionGeo",
                table: "EventDetails",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "EventAddressEng",
                table: "EventDetails",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DescriptionEng",
                table: "EventDetails");

            migrationBuilder.DropColumn(
                name: "DescriptionGeo",
                table: "EventDetails");

            migrationBuilder.DropColumn(
                name: "EventAddressEng",
                table: "EventDetails");

            migrationBuilder.RenameColumn(
                name: "NameGeo",
                table: "EventDetails",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "NameEng",
                table: "EventDetails",
                newName: "Description");

            migrationBuilder.RenameColumn(
                name: "EventAddressGeo",
                table: "EventDetails",
                newName: "EventAddress");
        }
    }
}
