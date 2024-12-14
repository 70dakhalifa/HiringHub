using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CV_Filtation_System.Repository.Data.Migrations
{
    /// <inheritdoc />
    public partial class LastUpdates1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_CompanyJobPostings",
                table: "CompanyJobPostings");

            migrationBuilder.DropColumn(
                name: "CompanyId",
                table: "JobPostings");

            migrationBuilder.RenameColumn(
                name: "JobId",
                table: "CompanyJobPostings",
                newName: "CompanyJobPostingId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CompanyJobPostings",
                table: "CompanyJobPostings",
                columns: new[] { "CompanyId", "JobPostingId" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_CompanyJobPostings",
                table: "CompanyJobPostings");

            migrationBuilder.RenameColumn(
                name: "CompanyJobPostingId",
                table: "CompanyJobPostings",
                newName: "JobId");

            migrationBuilder.AddColumn<int>(
                name: "CompanyId",
                table: "JobPostings",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_CompanyJobPostings",
                table: "CompanyJobPostings",
                columns: new[] { "CompanyId", "JobId" });
        }
    }
}
