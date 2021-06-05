using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Groups.Database.Migrations
{
    public partial class ResourceAggregate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ResourceAggregate",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GroupId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    AddedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ResourceAggregate", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ResourceAggregate_GroupAggregate_GroupId",
                        column: x => x.GroupId,
                        principalTable: "GroupAggregate",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "File",
                columns: table => new
                {
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Type = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ResourceId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Value = table.Column<byte[]>(type: "varbinary(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_File", x => new { x.ResourceId, x.Name, x.Type });
                    table.ForeignKey(
                        name: "FK_File_ResourceAggregate_ResourceId",
                        column: x => x.ResourceId,
                        principalTable: "ResourceAggregate",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_File_ResourceId",
                table: "File",
                column: "ResourceId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ResourceAggregate_GroupId",
                table: "ResourceAggregate",
                column: "GroupId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "File");

            migrationBuilder.DropTable(
                name: "ResourceAggregate");
        }
    }
}
