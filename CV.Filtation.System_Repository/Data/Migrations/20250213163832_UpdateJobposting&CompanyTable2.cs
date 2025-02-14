using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace CV_Filtation_System.Repository.Data.Migrations
{
    /// <inheritdoc />
    public partial class UpdateJobpostingCompanyTable2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "0425f0c5-e9f7-4dd8-ba39-ad39f00d52b7");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2599ea19-771a-40d3-8a17-e45267130191");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "e0fa9907-51f6-4c08-b665-faf368643e66");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "12ed6997-4ac6-49ec-8cce-54f238bf4ee9", "1", "Admin", "Admin" },
                    { "2797a31b-6d72-4270-9e0f-02a29ea32c7a", "2", "User", "User" },
                    { "2c181961-6090-496f-9c61-ec12d9abdd05", "3", "HR", "HR" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "0425f0c5-e9f7-4dd8-ba39-ad39f00d52b7", "3", "HR", "HR" },
                    { "2599ea19-771a-40d3-8a17-e45267130191", "2", "User", "User" },
                    { "e0fa9907-51f6-4c08-b665-faf368643e66", "1", "Admin", "Admin" }
                });
        }
    }
}
