using Microsoft.EntityFrameworkCore.Migrations;

namespace Groups.Database.Migrations
{
    public partial class TimeDurationAndPointsForAnswer : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TimeDuration",
                table: "TestAggregate",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "PointsForAnswer",
                table: "QuestionAggregate",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TimeDuration",
                table: "TestAggregate");

            migrationBuilder.DropColumn(
                name: "PointsForAnswer",
                table: "QuestionAggregate");
        }
    }
}
