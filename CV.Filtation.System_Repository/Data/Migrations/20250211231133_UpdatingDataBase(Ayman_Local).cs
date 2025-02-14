using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace CV_Filtation_System.Repository.Data.Migrations
{
    /// <inheritdoc />
    public partial class UpdatingDataBaseAyman_Local : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "5128984b-7e4c-40b4-8bb0-eab9ac66ff1b");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "afabb691-dbd3-4aa6-a429-f87bff53726c");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "c0bec878-f284-4d66-b95f-33a2de47d738");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "93bb24f7-ca23-44af-91e9-4a1ee0e80d6a", "2", "User", "User" },
                    { "94a29064-5a15-405b-b843-7740428b7ad6", "1", "Admin", "Admin" },
                    { "ea89e4f9-5fe0-45bd-bc59-abc9b826e05d", "3", "HR", "HR" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "93bb24f7-ca23-44af-91e9-4a1ee0e80d6a");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "94a29064-5a15-405b-b843-7740428b7ad6");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "ea89e4f9-5fe0-45bd-bc59-abc9b826e05d");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "5128984b-7e4c-40b4-8bb0-eab9ac66ff1b", "2", "User", "User" },
                    { "afabb691-dbd3-4aa6-a429-f87bff53726c", "3", "HR", "HR" },
                    { "c0bec878-f284-4d66-b95f-33a2de47d738", "1", "Admin", "Admin" }
                });
        }
    }
}
