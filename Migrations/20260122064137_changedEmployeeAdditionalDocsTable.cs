using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LmsApp2.Api.Migrations
{
    /// <inheritdoc />
    public partial class changedEmployeeAdditionalDocsTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_employeedocuments_employeeid",
                table: "employeedocuments");

            migrationBuilder.CreateIndex(
                name: "IX_employeedocuments_employeeid",
                table: "employeedocuments",
                column: "employeeid",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_employeedocuments_employeeid",
                table: "employeedocuments");

            migrationBuilder.CreateIndex(
                name: "IX_employeedocuments_employeeid",
                table: "employeedocuments",
                column: "employeeid");
        }
    }
}
