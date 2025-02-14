using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace CV_Filtation_System.Repository.Data.Migrations
{
    /// <inheritdoc />
    public partial class UpdateJobpostingCompanyTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CompanyJobPostings");

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

            migrationBuilder.AddColumn<int>(
                name: "CompanyId",
                table: "JobPostings",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "0425f0c5-e9f7-4dd8-ba39-ad39f00d52b7", "3", "HR", "HR" },
                    { "2599ea19-771a-40d3-8a17-e45267130191", "2", "User", "User" },
                    { "e0fa9907-51f6-4c08-b665-faf368643e66", "1", "Admin", "Admin" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_JobPostings_CompanyId",
                table: "JobPostings",
                column: "CompanyId");

            migrationBuilder.AddForeignKey(
                name: "FK_JobPostings_Companies_CompanyId",
                table: "JobPostings",
                column: "CompanyId",
                principalTable: "Companies",
                principalColumn: "CompanyId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_JobPostings_Companies_CompanyId",
                table: "JobPostings");

            migrationBuilder.DropIndex(
                name: "IX_JobPostings_CompanyId",
                table: "JobPostings");

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

            migrationBuilder.DropColumn(
                name: "CompanyId",
                table: "JobPostings");

            migrationBuilder.CreateTable(
                name: "CompanyJobPostings",
                columns: table => new
                {
                    CompanyId = table.Column<int>(type: "int", nullable: false),
                    JobPostingId = table.Column<int>(type: "int", nullable: false),
                    CompanyJobPostingId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompanyJobPostings", x => new { x.CompanyId, x.JobPostingId });
                    table.ForeignKey(
                        name: "FK_CompanyJobPostings_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "CompanyId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CompanyJobPostings_JobPostings_JobPostingId",
                        column: x => x.JobPostingId,
                        principalTable: "JobPostings",
                        principalColumn: "JobPostingId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "93bb24f7-ca23-44af-91e9-4a1ee0e80d6a", "2", "User", "User" },
                    { "94a29064-5a15-405b-b843-7740428b7ad6", "1", "Admin", "Admin" },
                    { "ea89e4f9-5fe0-45bd-bc59-abc9b826e05d", "3", "HR", "HR" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_CompanyJobPostings_JobPostingId",
                table: "CompanyJobPostings",
                column: "JobPostingId");
        }
    }
}
