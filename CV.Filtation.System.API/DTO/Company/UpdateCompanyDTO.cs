namespace CV.Filtation.System.API.DTO.Company
{
    public class UpdateCompanyDTO
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Description { get; set; }
        public string Location { get; set; }
        public string Website { get; set; }
        //public string Password { get; set; } // Optional for password change
    }
}
