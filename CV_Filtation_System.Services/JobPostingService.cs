using CV.Filtation.System.API.DTO;
using CV_Filtation_System.Core.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CV_Filtation_System.Services
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
            var jobPosting = new JobPosting
            {
                Title = dto.Title,
                Location = dto.Location,
                EmploymentType = dto.EmploymentType,
                SalaryRange = dto.SalaryRange,
                Description = dto.Description
            };

            _context.JobPostings.Add(jobPosting);
            await _context.SaveChangesAsync();

            foreach (var companyId in dto.CompanyIds)
            {
                var companyJobPosting = new CompanyJobPosting
                {
                    JobPostingId = jobPosting.JobPostingId,
                    CompanyId = companyId,
                    JobPosting = jobPosting,
                    Company = await _context.Companies.FindAsync(companyId)
                };
                _context.CompanyJobPostings.Add(companyJobPosting);
            }

            await _context.SaveChangesAsync();

            return jobPosting;
        }
    }
}
