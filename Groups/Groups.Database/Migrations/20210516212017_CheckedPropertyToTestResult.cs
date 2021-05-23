using Microsoft.EntityFrameworkCore.Migrations;

namespace Groups.Database.Migrations
{
    public partial class CheckedPropertyToTestResult : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Checked",
                table: "TestResultAggregate",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Checked",
                table: "TestResultAggregate");
        }
    }
}
