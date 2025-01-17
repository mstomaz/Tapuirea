using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace blog_rpg.Migrations
{
    /// <inheritdoc />
    public partial class ChangingTaleStoryPropNameToContent : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Story",
                table: "Tales",
                newName: "Content");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Content",
                table: "Tales",
                newName: "Story");
        }
    }
}
