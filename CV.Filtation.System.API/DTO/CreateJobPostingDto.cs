using System.ComponentModel.DataAnnotations;

namespace CV.Filtation.System.API.DTO
{
    public class CreateJobPostingDto
    {
        [Required]
        public string Title { get; set; }

        [Required]
        public string Location { get; set; }

        [Required]
        public string SalaryRange { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public int CompanyId { get; set; }

        [Required]
        public string JobType { get; set; }

        [Required]
        public string WorkMode { get; set; }

        public IFormFile JobImageUrl { get; set; }

        public bool IsFeatured { get; set; }

        public bool IsRecommended { get; set; }
    }
}
