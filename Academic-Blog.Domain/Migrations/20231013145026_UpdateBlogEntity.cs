using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Academic_Blog.Domain.Migrations
{
    /// <inheritdoc />
    public partial class UpdateBlogEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ReviewFromReviewer",
                table: "Account");

            migrationBuilder.AddColumn<string>(
                name: "ReviewFromReviewer",
                table: "Blog",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ReviewFromReviewer",
                table: "Blog");

            migrationBuilder.AddColumn<string>(
                name: "ReviewFromReviewer",
                table: "Account",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
