using CV_Filtation_System.Core.Results;
using System.Net.Http.Headers;
using System.Net.Http.Json;

namespace CV_Filtation_System.Services.Services
{
    public class AnalysisService : IAnalysisService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        public AnalysisService(IHttpClientFactory httpClientFactory)
         => _httpClientFactory = httpClientFactory;
        public async Task<ExternalAnalysisResult> GetResumeAnalysis(byte[] cvBytes, string fileName, string jobDescription)
        {
            using var httpClient = _httpClientFactory.CreateClient();
            using var content = new MultipartFormDataContent();

            // Add resume file
            var fileContent = new ByteArrayContent(cvBytes);
            fileContent.Headers.ContentType = MediaTypeHeaderValue.Parse("application/pdf");
            content.Add(fileContent, "resume", fileName);

            // Add job description
            content.Add(new StringContent(jobDescription), "job_desc");

            // Call external API
            var response = await httpClient.PostAsync("http://khalfofa.duckdns.org:5000/analyze", content);

            if (!response.IsSuccessStatusCode)
            {
                return null;
            }

            return await response.Content.ReadFromJsonAsync<ExternalAnalysisResult>();
        }
        public async Task<ExternalAnalysisResult> GetPercentageAnalysis(byte[] cvBytes, string fileName, string jobDescription)
        {
            using var httpClient = _httpClientFactory.CreateClient();
            using var content = new MultipartFormDataContent();

            // Add resume file
            var fileContent = new ByteArrayContent(cvBytes);
            fileContent.Headers.ContentType = MediaTypeHeaderValue.Parse("application/pdf");
            content.Add(fileContent, "resume", fileName);

            // Add job description
            content.Add(new StringContent(jobDescription), "job_desc");

            // Call external API
            var response = await httpClient.PostAsync("http://khalfofa.duckdns.org:5000/percentage_match", content);

            if (!response.IsSuccessStatusCode)
            {
                return null;
            }

            return await response.Content.ReadFromJsonAsync<ExternalAnalysisResult>();
        }
        public async Task<ExternalAnalysisResult> GetResumeSkillImprove(byte[] cvBytes, string fileName, string jobDescription)
        {
            using var httpClient = _httpClientFactory.CreateClient();
            using var content = new MultipartFormDataContent();

            // Add resume file
            var fileContent = new ByteArrayContent(cvBytes);
            fileContent.Headers.ContentType = MediaTypeHeaderValue.Parse("application/pdf");
            content.Add(fileContent, "resume", fileName);

            // Add job description
            content.Add(new StringContent(jobDescription), "job_desc");

            // Call external API
            var response = await httpClient.PostAsync("http://khalfofa.duckdns.org:5000/skill_improve", content);

            if (!response.IsSuccessStatusCode)
            {
                return null;
            }

            return await response.Content.ReadFromJsonAsync<ExternalAnalysisResult>();
        }
    }
}
