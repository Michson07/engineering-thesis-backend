using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Groups.Database.Migrations
{
    public partial class TestResultAggregate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Answer_Question_QuestionId",
                table: "Answer");

            migrationBuilder.DropForeignKey(
                name: "FK_Question_TestAggregate_TestAggregateId",
                table: "Question");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Question",
                table: "Question");

            migrationBuilder.RenameTable(
                name: "Question",
                newName: "QuestionAggregate");

            migrationBuilder.RenameIndex(
                name: "IX_Question_TestAggregateId",
                table: "QuestionAggregate",
                newName: "IX_QuestionAggregate_TestAggregateId");

            migrationBuilder.AlterColumn<Guid>(
                name: "TestAggregateId",
                table: "QuestionAggregate",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddPrimaryKey(
                name: "PK_QuestionAggregate",
                table: "QuestionAggregate",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "TestResultAggregate",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TestId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Student_Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    State = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TestResultAggregate", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TestResultAggregate_TestAggregate_TestId",
                        column: x => x.TestId,
                        principalTable: "TestAggregate",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "StudentAnswer",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    QuestionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ReceivedAnswers = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PointsForAnswer = table.Column<int>(type: "int", nullable: false),
                    TestResultAggregateId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudentAnswer", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StudentAnswer_QuestionAggregate_QuestionId",
                        column: x => x.QuestionId,
                        principalTable: "QuestionAggregate",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StudentAnswer_TestResultAggregate_TestResultAggregateId",
                        column: x => x.TestResultAggregateId,
                        principalTable: "TestResultAggregate",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_StudentAnswer_QuestionId",
                table: "StudentAnswer",
                column: "QuestionId");

            migrationBuilder.CreateIndex(
                name: "IX_StudentAnswer_TestResultAggregateId",
                table: "StudentAnswer",
                column: "TestResultAggregateId");

            migrationBuilder.CreateIndex(
                name: "IX_TestResultAggregate_TestId",
                table: "TestResultAggregate",
                column: "TestId");

            migrationBuilder.AddForeignKey(
                name: "FK_Answer_QuestionAggregate_QuestionId",
                table: "Answer",
                column: "QuestionId",
                principalTable: "QuestionAggregate",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_QuestionAggregate_TestAggregate_TestAggregateId",
                table: "QuestionAggregate",
                column: "TestAggregateId",
                principalTable: "TestAggregate",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Answer_QuestionAggregate_QuestionId",
                table: "Answer");

            migrationBuilder.DropForeignKey(
                name: "FK_QuestionAggregate_TestAggregate_TestAggregateId",
                table: "QuestionAggregate");

            migrationBuilder.DropTable(
                name: "StudentAnswer");

            migrationBuilder.DropTable(
                name: "TestResultAggregate");

            migrationBuilder.DropPrimaryKey(
                name: "PK_QuestionAggregate",
                table: "QuestionAggregate");

            migrationBuilder.RenameTable(
                name: "QuestionAggregate",
                newName: "Question");

            migrationBuilder.RenameIndex(
                name: "IX_QuestionAggregate_TestAggregateId",
                table: "Question",
                newName: "IX_Question_TestAggregateId");

            migrationBuilder.AlterColumn<Guid>(
                name: "TestAggregateId",
                table: "Question",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Question",
                table: "Question",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Answer_Question_QuestionId",
                table: "Answer",
                column: "QuestionId",
                principalTable: "Question",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Question_TestAggregate_TestAggregateId",
                table: "Question",
                column: "TestAggregateId",
                principalTable: "TestAggregate",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
