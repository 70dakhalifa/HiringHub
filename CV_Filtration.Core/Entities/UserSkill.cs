namespace CV_Filtation_System.Core.Entities
{
    public class UserSkill
    {
        public string UserId { get; set; } = string.Empty;
        public User User { get; set; } = null!;
        public int SkillId { get; set; }
        public Skill Skill { get; set; } = null!;
    }
}
