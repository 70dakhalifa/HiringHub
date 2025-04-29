using CV_Filtation_System.Core.Results;

namespace CV_Filtation_System.Services.Services
{
    public interface IAnalysisService
    {
        Task<ExternalAnalysisResult> GetResumeAnalysis(byte[] cvBytes, string fileName, string jobDescription);
        Task<ExternalAnalysisResult> GetPercentageAnalysis(byte[] cvBytes, string fileName, string jobDescription);
        Task<ExternalAnalysisResult> GetResumeSkillImprove(byte[] cvBytes, string fileName, string jobDescription);
        Task<JobRecommandResult> GetExpectedPosition(byte[] cvBytes, string fileName);
    }
}