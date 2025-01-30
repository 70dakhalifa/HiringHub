using System.ComponentModel.DataAnnotations;

namespace CV.Filtation.System.API.DTO
{
    public class RegisterUserDTO
    {
        [Required(ErrorMessage = "First Name is required")]
        public string FName { get; set; }
        [Required(ErrorMessage = "Last Name is required")]
        public string LName { get; set; }
        [EmailAddress]
        [Required(ErrorMessage = "Email is required")]
        public string Email { get; set; }
        public string? Address { get; set; }
        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public string Phone { get; set; }
        public string? City { get; set; }




    }
}
