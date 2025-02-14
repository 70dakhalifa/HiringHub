using CV.Filtation.System.API.DTO;
using CV_Filtation_System.Core.Entities;
using Microsoft.EntityFrameworkCore;


namespace CV_Filtation_System.Services.Services
{
    public class JobPostingService : IJobPostingService
    {
        private readonly AppDbContext _context;

        public JobPostingService(AppDbContext context)
        {
            _context = context;
        }
        public async Task<JobPosting> GetJobPostingByTitleAsync(string title)
        {
            return await _context.JobPostings
                .Where(j => j.Title.ToLower() == title.ToLower()) // Case-insensitive comparison
                .FirstOrDefaultAsync();
        }
        public async Task<JobPosting> CreateJobPostingWithCompaniesAsync(CreateJobPostingWithCompaniesDto dto)
        {
            var company = await _context.Companies.FindAsync(dto.CompanyId);
            if (company == null)
            {
                throw new Exception("Company not found");
            }

            var jobPosting = new JobPosting
            {
                Title = dto.Title,
                Location = dto.Location,
                EmploymentType = dto.EmploymentType,
                SalaryRange = dto.SalaryRange,
                Description = dto.Description,
                CompanyId = dto.CompanyId, // Set the foreign key
                Company = company // Assign the navigation property
            };

            _context.JobPostings.Add(jobPosting);
            await _context.SaveChangesAsync();

            return jobPosting;
        }
    }
}
