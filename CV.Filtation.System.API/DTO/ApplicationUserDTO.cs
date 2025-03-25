namespace CV.Filtation.System.API.DTO
{
    public class ApplicationUserDTO
    {
        public int ApplicationId { get; set; }
        public string Status { get; set; }
        public DateTime ApplicationDate { get; set; }
        public string CV_Path { get; set; }
        public JobPostingDto JobPosting { get; set; }
        public string CompanyName { get; set; }
    }
}
