using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Chat.Database.Migrations
{
    public partial class Chat : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "GroupChatAggregate",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    GroupId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GroupChatAggregate", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PrivateChatAggregate",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    User1Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    User2Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PrivateChatAggregate", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "GroupChatAggregate_Messages",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Text = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ChatGroupId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GroupChatAggregate_Messages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GroupChatAggregate_Messages_GroupChatAggregate_ChatGroupId",
                        column: x => x.ChatGroupId,
                        principalTable: "GroupChatAggregate",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PrivateChatAggregate_Messages",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Text = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PrivateChatId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PrivateChatAggregate_Messages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PrivateChatAggregate_Messages_PrivateChatAggregate_PrivateChatId",
                        column: x => x.PrivateChatId,
                        principalTable: "PrivateChatAggregate",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_GroupChatAggregate_Messages_ChatGroupId",
                table: "GroupChatAggregate_Messages",
                column: "ChatGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_PrivateChatAggregate_Messages_PrivateChatId",
                table: "PrivateChatAggregate_Messages",
                column: "PrivateChatId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GroupChatAggregate_Messages");

            migrationBuilder.DropTable(
                name: "PrivateChatAggregate_Messages");

            migrationBuilder.DropTable(
                name: "GroupChatAggregate");

            migrationBuilder.DropTable(
                name: "PrivateChatAggregate");
        }
    }
}
