namespace CV.Filtation.System.API.DTO.Company
{
    public class CompanyProfileDTO
    {
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; }
        public string Website { get; set; }
        public string Location { get; set; }
        public string Email { get; set; } = string.Empty;
        public string ProfilePicture { get; set; }
    }
}
