using Microsoft.EntityFrameworkCore.Migrations;

namespace Groups.Database.Migrations
{
    public partial class RemoveManyCorrectAnswersColumn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ManyCorrectAnswers",
                table: "QuestionAggregate");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "ManyCorrectAnswers",
                table: "QuestionAggregate",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
