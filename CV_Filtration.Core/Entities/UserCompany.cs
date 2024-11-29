namespace CV_Filtation_System.Core.Entities
{
    public class UserCompany
    {
        public string UserId { get; set; } = string.Empty;
        public User User { get; set; } = null!;
        public int CompanyId { get; set; }
        public Company Company { get; set; } = null!;
        public int Score { get; set; }
    }
}
