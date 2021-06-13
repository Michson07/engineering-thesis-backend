using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Chat.Database.Migrations
{
    public partial class ChatUserIdToUserEmail : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GroupChatAggregate_Messages_GroupChatAggregate_ChatGroupId",
                table: "GroupChatAggregate_Messages");

            migrationBuilder.DropForeignKey(
                name: "FK_PrivateChatAggregate_Messages_PrivateChatAggregate_PrivateChatId",
                table: "PrivateChatAggregate_Messages");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PrivateChatAggregate_Messages",
                table: "PrivateChatAggregate_Messages");

            migrationBuilder.DropPrimaryKey(
                name: "PK_GroupChatAggregate_Messages",
                table: "GroupChatAggregate_Messages");

            migrationBuilder.DropColumn(
                name: "User1Id",
                table: "PrivateChatAggregate");

            migrationBuilder.DropColumn(
                name: "User2Id",
                table: "PrivateChatAggregate");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "PrivateChatAggregate_Messages");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "GroupChatAggregate_Messages");

            migrationBuilder.RenameTable(
                name: "PrivateChatAggregate_Messages",
                newName: "PrivateChatMessages");

            migrationBuilder.RenameTable(
                name: "GroupChatAggregate_Messages",
                newName: "GroupChatMessages");

            migrationBuilder.RenameIndex(
                name: "IX_PrivateChatAggregate_Messages_PrivateChatId",
                table: "PrivateChatMessages",
                newName: "IX_PrivateChatMessages_PrivateChatId");

            migrationBuilder.RenameIndex(
                name: "IX_GroupChatAggregate_Messages_ChatGroupId",
                table: "GroupChatMessages",
                newName: "IX_GroupChatMessages_ChatGroupId");

            migrationBuilder.AddColumn<string>(
                name: "User1Email",
                table: "PrivateChatAggregate",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "User2Email",
                table: "PrivateChatAggregate",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserEmail",
                table: "PrivateChatMessages",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserEmail",
                table: "GroupChatMessages",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_PrivateChatMessages",
                table: "PrivateChatMessages",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_GroupChatMessages",
                table: "GroupChatMessages",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_GroupChatMessages_GroupChatAggregate_ChatGroupId",
                table: "GroupChatMessages",
                column: "ChatGroupId",
                principalTable: "GroupChatAggregate",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PrivateChatMessages_PrivateChatAggregate_PrivateChatId",
                table: "PrivateChatMessages",
                column: "PrivateChatId",
                principalTable: "PrivateChatAggregate",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GroupChatMessages_GroupChatAggregate_ChatGroupId",
                table: "GroupChatMessages");

            migrationBuilder.DropForeignKey(
                name: "FK_PrivateChatMessages_PrivateChatAggregate_PrivateChatId",
                table: "PrivateChatMessages");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PrivateChatMessages",
                table: "PrivateChatMessages");

            migrationBuilder.DropPrimaryKey(
                name: "PK_GroupChatMessages",
                table: "GroupChatMessages");

            migrationBuilder.DropColumn(
                name: "User1Email",
                table: "PrivateChatAggregate");

            migrationBuilder.DropColumn(
                name: "User2Email",
                table: "PrivateChatAggregate");

            migrationBuilder.DropColumn(
                name: "UserEmail",
                table: "PrivateChatMessages");

            migrationBuilder.DropColumn(
                name: "UserEmail",
                table: "GroupChatMessages");

            migrationBuilder.RenameTable(
                name: "PrivateChatMessages",
                newName: "PrivateChatAggregate_Messages");

            migrationBuilder.RenameTable(
                name: "GroupChatMessages",
                newName: "GroupChatAggregate_Messages");

            migrationBuilder.RenameIndex(
                name: "IX_PrivateChatMessages_PrivateChatId",
                table: "PrivateChatAggregate_Messages",
                newName: "IX_PrivateChatAggregate_Messages_PrivateChatId");

            migrationBuilder.RenameIndex(
                name: "IX_GroupChatMessages_ChatGroupId",
                table: "GroupChatAggregate_Messages",
                newName: "IX_GroupChatAggregate_Messages_ChatGroupId");

            migrationBuilder.AddColumn<Guid>(
                name: "User1Id",
                table: "PrivateChatAggregate",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "User2Id",
                table: "PrivateChatAggregate",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "UserId",
                table: "PrivateChatAggregate_Messages",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "UserId",
                table: "GroupChatAggregate_Messages",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddPrimaryKey(
                name: "PK_PrivateChatAggregate_Messages",
                table: "PrivateChatAggregate_Messages",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_GroupChatAggregate_Messages",
                table: "GroupChatAggregate_Messages",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_GroupChatAggregate_Messages_GroupChatAggregate_ChatGroupId",
                table: "GroupChatAggregate_Messages",
                column: "ChatGroupId",
                principalTable: "GroupChatAggregate",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PrivateChatAggregate_Messages_PrivateChatAggregate_PrivateChatId",
                table: "PrivateChatAggregate_Messages",
                column: "PrivateChatId",
                principalTable: "PrivateChatAggregate",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
