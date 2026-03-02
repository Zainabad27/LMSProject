using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LmsApp2.Api.Migrations
{
    /// <inheritdoc />
    public partial class addedIscheckedFieldInAssginmentSubmissions : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Isgraded",
                table: "assignmentsubmission",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Isgraded",
                table: "assignmentsubmission");
        }
    }
}
