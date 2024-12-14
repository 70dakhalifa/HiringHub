using CV_Filtation_System.Services;

namespace CV.Filtation.System.API.DTO
{
    public class JobPostingDto
    {
        public int JobPostingId { get; set; }
        public string Title { get; set; } = string.Empty;
        public string? Location { get; set; }
        public string? EmploymentType { get; set; }
        public string? SalaryRange { get; set; }
        public string? Description { get; set; }

        // Optionally, include the companies associated with this job posting
        public List<CompanyPostsDto> Companies { get; set; } = new();
    }
}
