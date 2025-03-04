using System.ComponentModel.DataAnnotations;

namespace CV.Filtation.System.API.DTO
{
    public class RegisterCompanyDTO
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Website { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Location { get; set; }
    }
}
