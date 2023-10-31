using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Academic_Blog.Domain.Migrations
{
    /// <inheritdoc />
    public partial class UpdateComment : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comment_Comment_CommentRepliedId",
                table: "Comment");

            migrationBuilder.DropIndex(
                name: "IX_Comment_CommentRepliedId",
                table: "Comment");

            migrationBuilder.DropColumn(
                name: "CommentRepliedId",
                table: "Comment");

            migrationBuilder.CreateIndex(
                name: "IX_Comment_ReplyToId",
                table: "Comment",
                column: "ReplyToId");

            migrationBuilder.AddForeignKey(
                name: "FK_Comment_Comment_ReplyToId",
                table: "Comment",
                column: "ReplyToId",
                principalTable: "Comment",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comment_Comment_ReplyToId",
                table: "Comment");

            migrationBuilder.DropIndex(
                name: "IX_Comment_ReplyToId",
                table: "Comment");

            migrationBuilder.AddColumn<Guid>(
                name: "CommentRepliedId",
                table: "Comment",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Comment_CommentRepliedId",
                table: "Comment",
                column: "CommentRepliedId");

            migrationBuilder.AddForeignKey(
                name: "FK_Comment_Comment_CommentRepliedId",
                table: "Comment",
                column: "CommentRepliedId",
                principalTable: "Comment",
                principalColumn: "Id");
        }
    }
}
