using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CV_Filtation_System.Repository.Data.Migrations
{
    /// <inheritdoc />
    public partial class UpdateSomeTablesNames : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "JopType",
                table: "JobPostings",
                newName: "JobType");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "JobType",
                table: "JobPostings",
                newName: "JopType");
        }
    }
}
