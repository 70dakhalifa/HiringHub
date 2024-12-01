using Microsoft.AspNetCore.Identity;
namespace CV_Filtation_System.Core.Entities
{
    public class User : IdentityUser
    {
        [required]
        public string FName { get; set; }
        public string LName { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string? CV_FilePath { get; set; }
        public ICollection<UserSkill> UserSkills { get; set; } = new List<UserSkill>();
        public ICollection<UserCompany> UserCompanies { get; set; } = new List<UserCompany>();


    }
}
