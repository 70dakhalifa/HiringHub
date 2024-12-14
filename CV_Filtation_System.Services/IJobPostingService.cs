using CV.Filtation.System.API.DTO;
using CV_Filtation_System.Core.Entities;

namespace CV_Filtation_System.Services
{
    public interface IJobPostingService
    {
        Task<JobPosting> GetJobPostingByTitleAsync(string title);
        Task<JobPosting> CreateJobPostingWithCompaniesAsync(CreateJobPostingWithCompaniesDto dto);
    }

}
