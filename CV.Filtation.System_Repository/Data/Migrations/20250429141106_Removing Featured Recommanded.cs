using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CV_Filtation_System.Repository.Data.Migrations
{
    /// <inheritdoc />
    public partial class RemovingFeaturedRecommanded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsFeatured",
                table: "JobPostings");

            migrationBuilder.DropColumn(
                name: "IsRecommended",
                table: "JobPostings");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsFeatured",
                table: "JobPostings",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsRecommended",
                table: "JobPostings",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
