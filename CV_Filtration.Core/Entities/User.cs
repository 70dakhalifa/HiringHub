using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CV_Filtation_System.Core.Entities
{
    public class User
    {
        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string? CV_FilePath { get; set; }
        public ICollection<UserSkill> UserSkills { get; set; } = new List<UserSkill>();
        public ICollection<UserCompany> UserCompanies { get; set; } = new List<UserCompany>();


    }
}
