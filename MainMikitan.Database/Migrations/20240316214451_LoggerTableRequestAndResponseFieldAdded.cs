using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MainMikitan.Database.Migrations
{
    /// <inheritdoc />
    public partial class LoggerTableRequestAndResponseFieldAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Request",
                table: "Logger",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Response",
                table: "Logger",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Request",
                table: "Logger");

            migrationBuilder.DropColumn(
                name: "Response",
                table: "Logger");
        }
    }
}
