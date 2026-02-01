using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LmsApp2.Api.Migrations
{
    /// <inheritdoc />
    public partial class AddedDesignationAgainInEmployeeTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Employeedesignation",
                table: "employees",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "Employeeid",
                table: "AspNetUsers",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefreshToken",
                table: "AspNetUsers",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "TokenExpiry",
                table: "AspNetUsers",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_Employeeid",
                table: "AspNetUsers",
                column: "Employeeid");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_employees_Employeeid",
                table: "AspNetUsers",
                column: "Employeeid",
                principalTable: "employees",
                principalColumn: "employeeid");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_employees_Employeeid",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_Employeeid",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Employeedesignation",
                table: "employees");

            migrationBuilder.DropColumn(
                name: "Employeeid",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "RefreshToken",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "TokenExpiry",
                table: "AspNetUsers");
        }
    }
}
