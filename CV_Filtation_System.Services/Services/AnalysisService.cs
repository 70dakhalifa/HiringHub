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
            var response = await httpClient.PostAsync("https://6dd2-156-195-106-67.ngrok-free.app/analyze", content);

            if (!response.IsSuccessStatusCode)
            {
                return null;
            }

            return await response.Content.ReadFromJsonAsync<ExternalAnalysisResult>();
        }
        public async Task<PercentageMatchResult> GetPercentageAnalysis(byte[] cvBytes, string fileName, string jobDescription)
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
            var response = await httpClient.PostAsync("https://6dd2-156-195-106-67.ngrok-free.app/percentage_match", content);

            if (!response.IsSuccessStatusCode)
            {
                return null;
            }

            return await response.Content.ReadFromJsonAsync<PercentageMatchResult>();
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
            var response = await httpClient.PostAsync("https://6dd2-156-195-106-67.ngrok-free.app/skill_improve", content);

            if (!response.IsSuccessStatusCode)
            {
                return null;
            }

            return await response.Content.ReadFromJsonAsync<ExternalAnalysisResult>();
        }

        public async Task<JobRecommandResult> GetExpectedPosition(byte[] cvBytes, string fileName)
        {
            using var httpClient = _httpClientFactory.CreateClient();
            using var content = new MultipartFormDataContent();

            var fileContent = new ByteArrayContent(cvBytes);
            fileContent.Headers.ContentType = MediaTypeHeaderValue.Parse("application/pdf");
            content.Add(fileContent, "file", fileName);

            var response = await httpClient.PostAsync("https://24d1-156-195-106-67.ngrok-free.app/recommend_job", content);

            if (!response.IsSuccessStatusCode)
            {
                return null;
            }

            return await response.Content.ReadFromJsonAsync<JobRecommandResult>();
        }
    }
}
