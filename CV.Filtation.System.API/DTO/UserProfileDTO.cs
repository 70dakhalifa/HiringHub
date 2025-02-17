using System.ComponentModel.DataAnnotations;

namespace CV.Filtation.System.API.DTO
{
    public class UserProfileDTO
    {
        public string FName { get; set; }
        public string LName { get; set; }
        [EmailAddress]
        public string Email { get; set; }
        public string? Address { get; set; }
        public string Phone { get; set; }
        public string? City { get; set; }
    }
}
