namespace CV.Filtation.System.API.DTO
{
    public class AnalyzeRequestDto
    {
        public string userId { get; set; }
        public IFormFile Resume { get; set; }
        public string JobDescription { get; set; }
    }
}
