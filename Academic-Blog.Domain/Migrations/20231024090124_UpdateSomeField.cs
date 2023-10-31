using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Academic_Blog.Domain.Migrations
{
    /// <inheritdoc />
    public partial class UpdateSomeField : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ShortDescription",
                table: "Blog",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Avatar",
                table: "Account",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ShortDescription",
                table: "Blog");

            migrationBuilder.DropColumn(
                name: "Avatar",
                table: "Account");
        }
    }
}
