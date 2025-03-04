namespace CV.Filtation.System.API.DTO
{
    public class CreateJobPostingWithCompaniesDto
    {
        public string Title { get; set; }
        public string Location { get; set; }
        public string JobType { get; set; }
        public string WorkMode { get; set; }
        public string SalaryRange { get; set; }
        public string Description { get; set; }
        public int CompanyId { get; set; }
    }
}
