using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CV_Filtation_System.Core.Entities
{
    public class Skill
    {
        public int SkillId { get; set; }
        public string SkillName { get; set; } = string.Empty;

        public ICollection<UserSkill> UserSkills { get; set; } = new List<UserSkill>();
    }

}
