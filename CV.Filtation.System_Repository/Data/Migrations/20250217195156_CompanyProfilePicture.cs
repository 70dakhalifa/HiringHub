using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace CV_Filtation_System.Repository.Data.Migrations
{
    /// <inheritdoc />
    public partial class CompanyProfilePicture : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "6809e408-69a9-4cb8-8417-d2ccf741d2b3");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "70e8c18d-bedf-4d83-ba20-2766cd0a4cdc");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "88a2334d-75da-432b-a181-29cea03ff960");

            migrationBuilder.AddColumn<string>(
                name: "ProfilePicture",
                table: "Companies",
                type: "nvarchar(max)",
                nullable: true);

        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ProfilePicture",
                table: "Companies");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "6809e408-69a9-4cb8-8417-d2ccf741d2b3", "1", "Admin", "Admin" },
                    { "70e8c18d-bedf-4d83-ba20-2766cd0a4cdc", "3", "HR", "HR" },
                    { "88a2334d-75da-432b-a181-29cea03ff960", "2", "User", "User" }
                });
        }
    }
}
