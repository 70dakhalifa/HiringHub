using System.ComponentModel.DataAnnotations;

namespace CV.Filtation.System.API.DTO
{
    public class LoginDTO
    {
        [Required(ErrorMessage = "Email Address is required")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; }
    }
}
