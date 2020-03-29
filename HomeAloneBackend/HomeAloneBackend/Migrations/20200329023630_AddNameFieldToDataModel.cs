using Microsoft.EntityFrameworkCore.Migrations;

namespace HomeAloneBackend.Migrations
{
    public partial class AddNameFieldToDataModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Data",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                table: "Data");
        }
    }
}
