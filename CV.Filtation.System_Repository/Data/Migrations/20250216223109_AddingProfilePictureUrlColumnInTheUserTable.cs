using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace CV_Filtation_System.Repository.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddingProfilePictureUrlColumnInTheUserTable : Migration
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

            migrationBuilder.AddColumn<string>(
                name: "ProfilePictureUrl",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "09ec50ab-cb2d-4608-8b25-feabae668f8d", "3", "HR", "HR" },
                    { "20f75d2d-1a68-4799-9b2f-16f77f7f5acf", "2", "User", "User" },
                    { "ff9069d7-d738-4b36-9ecd-16778f931460", "1", "Admin", "Admin" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "09ec50ab-cb2d-4608-8b25-feabae668f8d");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "20f75d2d-1a68-4799-9b2f-16f77f7f5acf");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "ff9069d7-d738-4b36-9ecd-16778f931460");

            migrationBuilder.DropColumn(
                name: "ProfilePictureUrl",
                table: "AspNetUsers");

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
    }
}
