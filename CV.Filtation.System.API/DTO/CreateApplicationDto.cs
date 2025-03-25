using System.ComponentModel.DataAnnotations;

namespace CV.Filtation.System.API.DTO
{
    public class CreateApplicationDto
    {
        [Required]
        public int JobPostingId { get; set; }
        [Required]
        public string UserId { get; set; }
        [Required]
        public IFormFile CV { get; set; }
    }
}
