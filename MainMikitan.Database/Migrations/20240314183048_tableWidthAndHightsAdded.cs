using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MainMikitan.Database.Migrations
{
    /// <inheritdoc />
    public partial class tableWidthAndHightsAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<float>(
                name: "Height",
                table: "TableInfo",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<float>(
                name: "Width",
                table: "TableInfo",
                type: "real",
                nullable: false,
                defaultValue: 0f);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Height",
                table: "TableInfo");

            migrationBuilder.DropColumn(
                name: "Width",
                table: "TableInfo");
        }
    }
}
