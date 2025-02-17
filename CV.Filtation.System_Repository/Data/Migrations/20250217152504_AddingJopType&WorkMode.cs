using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace CV_Filtation_System.Repository.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddingJopTypeWorkMode : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "12ed6997-4ac6-49ec-8cce-54f238bf4ee9");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2797a31b-6d72-4270-9e0f-02a29ea32c7a");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2c181961-6090-496f-9c61-ec12d9abdd05");

            migrationBuilder.DropColumn(
                name: "EmploymentType",
                table: "JobPostings");

            migrationBuilder.AddColumn<string>(
                name: "JopType",
                table: "JobPostings",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "WorkMode",
                table: "JobPostings",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            //migrationBuilder.InsertData(
            //    table: "AspNetRoles",
            //    columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
            //    values: new object[,]
            //    {
            //        { "6809e408-69a9-4cb8-8417-d2ccf741d2b3", "1", "Admin", "Admin" },
            //        { "70e8c18d-bedf-4d83-ba20-2766cd0a4cdc", "3", "HR", "HR" },
            //        { "88a2334d-75da-432b-a181-29cea03ff960", "2", "User", "User" }
            //    });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.DropColumn(
                name: "JopType",
                table: "JobPostings");

            migrationBuilder.DropColumn(
                name: "WorkMode",
                table: "JobPostings");

            migrationBuilder.AddColumn<string>(
                name: "EmploymentType",
                table: "JobPostings",
                type: "nvarchar(max)",
                nullable: true);

            //migrationBuilder.InsertData(
            //    table: "AspNetRoles",
            //    columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
            //    values: new object[,]
            //    {
            //        { "12ed6997-4ac6-49ec-8cce-54f238bf4ee9", "1", "Admin", "Admin" },
            //        { "2797a31b-6d72-4270-9e0f-02a29ea32c7a", "2", "User", "User" },
            //        { "2c181961-6090-496f-9c61-ec12d9abdd05", "3", "HR", "HR" }
            //    });
        }
    }
}
