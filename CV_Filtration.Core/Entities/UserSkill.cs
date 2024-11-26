using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CV_Filtation_System.Core.Entities
{
    public class UserSkill
    {
        public int UserId { get; set; }
        public User User { get; set; } = null!;
        public int SkillId { get; set; }
        public Skill Skill { get; set; } = null!;
    }

}
