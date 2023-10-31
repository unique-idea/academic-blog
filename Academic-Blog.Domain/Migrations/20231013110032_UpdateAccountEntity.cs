using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Academic_Blog.Domain.Migrations
{
    /// <inheritdoc />
    public partial class UpdateAccountEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Account_BannedInfors_BannedInforId",
                table: "Account");

            migrationBuilder.DropIndex(
                name: "IX_Account_BannedInforId",
                table: "Account");

            migrationBuilder.DropColumn(
                name: "BannedInforId",
                table: "Account");

            migrationBuilder.AddColumn<string>(
                name: "reviewFromReviewer",
                table: "Account",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_BannedInfors_AccountId",
                table: "BannedInfors",
                column: "AccountId");

            migrationBuilder.AddForeignKey(
                name: "Fk_Account_BannedInfor",
                table: "BannedInfors",
                column: "AccountId",
                principalTable: "Account",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "Fk_Account_BannedInfor",
                table: "BannedInfors");

            migrationBuilder.DropIndex(
                name: "IX_BannedInfors_AccountId",
                table: "BannedInfors");

            migrationBuilder.DropColumn(
                name: "reviewFromReviewer",
                table: "Account");

            migrationBuilder.AddColumn<Guid>(
                name: "BannedInforId",
                table: "Account",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Account_BannedInforId",
                table: "Account",
                column: "BannedInforId",
                unique: true,
                filter: "[BannedInforId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_Account_BannedInfors_BannedInforId",
                table: "Account",
                column: "BannedInforId",
                principalTable: "BannedInfors",
                principalColumn: "Id");
        }
    }
}
