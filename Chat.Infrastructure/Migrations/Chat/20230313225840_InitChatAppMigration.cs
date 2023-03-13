using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Chat.Infrastructure.Migrations.Chat
{
    /// <inheritdoc />
    public partial class InitChatAppMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "chat");

            migrationBuilder.EnsureSchema(
                name: "message");

            migrationBuilder.CreateTable(
                name: "ChatRooms",
                schema: "chat",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChatRooms", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Messages",
                schema: "message",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ChatRoomId = table.Column<int>(type: "int", nullable: false),
                    Data = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Messages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Messages_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Messages_ChatRooms_ChatRoomId",
                        column: x => x.ChatRoomId,
                        principalSchema: "chat",
                        principalTable: "ChatRooms",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Notifications",
                schema: "chat",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ChatRoomId = table.Column<int>(type: "int", nullable: false),
                    UnreadMessages = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Notifications", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Notifications_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Notifications_ChatRooms_ChatRoomId",
                        column: x => x.ChatRoomId,
                        principalSchema: "chat",
                        principalTable: "ChatRooms",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "UserReferences",
                schema: "chat",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ChatRoomId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserReferences", x => new { x.ChatRoomId, x.UserId });
                    table.ForeignKey(
                        name: "FK_UserReferences_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserReferences_ChatRooms_ChatRoomId",
                        column: x => x.ChatRoomId,
                        principalSchema: "chat",
                        principalTable: "ChatRooms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MessageProperties",
                schema: "message",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SenderId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ReceiverId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ChatRoomId = table.Column<int>(type: "int", nullable: false),
                    MessageId = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MessageProperties", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MessageProperties_AspNetUsers_ReceiverId",
                        column: x => x.ReceiverId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_MessageProperties_AspNetUsers_SenderId",
                        column: x => x.SenderId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_MessageProperties_ChatRooms_ChatRoomId",
                        column: x => x.ChatRoomId,
                        principalSchema: "chat",
                        principalTable: "ChatRooms",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_MessageProperties_Messages_MessageId",
                        column: x => x.MessageId,
                        principalSchema: "message",
                        principalTable: "Messages",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_ChatRooms_Name",
                schema: "chat",
                table: "ChatRooms",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_MessageProperties_ChatRoomId",
                schema: "message",
                table: "MessageProperties",
                column: "ChatRoomId");

            migrationBuilder.CreateIndex(
                name: "IX_MessageProperties_MessageId",
                schema: "message",
                table: "MessageProperties",
                column: "MessageId");

            migrationBuilder.CreateIndex(
                name: "IX_MessageProperties_ReceiverId",
                schema: "message",
                table: "MessageProperties",
                column: "ReceiverId");

            migrationBuilder.CreateIndex(
                name: "IX_MessageProperties_SenderId",
                schema: "message",
                table: "MessageProperties",
                column: "SenderId");

            migrationBuilder.CreateIndex(
                name: "IX_Messages_ChatRoomId",
                schema: "message",
                table: "Messages",
                column: "ChatRoomId");

            migrationBuilder.CreateIndex(
                name: "IX_Messages_UserId",
                schema: "message",
                table: "Messages",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Notifications_ChatRoomId",
                schema: "chat",
                table: "Notifications",
                column: "ChatRoomId");

            migrationBuilder.CreateIndex(
                name: "IX_Notifications_UserId",
                schema: "chat",
                table: "Notifications",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserReferences_UserId",
                schema: "chat",
                table: "UserReferences",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MessageProperties",
                schema: "message");

            migrationBuilder.DropTable(
                name: "Notifications",
                schema: "chat");

            migrationBuilder.DropTable(
                name: "UserReferences",
                schema: "chat");

            migrationBuilder.DropTable(
                name: "Messages",
                schema: "message");

            migrationBuilder.DropTable(
                name: "ChatRooms",
                schema: "chat");
        }
    }
}
