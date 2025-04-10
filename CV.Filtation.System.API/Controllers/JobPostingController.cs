using CV.Filtation.System.API.DTO;
using CV.Filtation.System.API.Helpers;
using CV_Filtation_System.Core.Entities;
using CV_Filtation_System.Services.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace CV.Filtation.System.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JobPostingsController : ControllerBase
    {
        private readonly IJobPostingService _jobPostingService;
        private readonly AppDbContext _context;
        private readonly ILogger<JobPostingsController> _logger;

        public JobPostingsController(IJobPostingService jobPostingService, AppDbContext context, ILogger<JobPostingsController> logger)
        {
            _jobPostingService = jobPostingService;
            _context = context;
            _logger = logger;
        }

        [HttpPost]
        public async Task<IActionResult> CreateJobPosting([FromForm] DTO.CreateJobPostingDto dto)
        {
            if (dto == null)
            {
                return BadRequest("Invalid data.");
            }

            var companyExists = await _context.Companies.AnyAsync(c => c.CompanyId == dto.CompanyId);
            if (!companyExists)
            {
                return BadRequest($"Company with ID {dto.CompanyId} does not exist.");
            }

            var jobPosting = new JobPosting
            {
                Title = dto.Title,
                Location = dto.Location,
                SalaryRange = dto.SalaryRange,
                Description = dto.Description,
                CompanyId = dto.CompanyId,
                JobType = dto.JobType,
                WorkMode = dto.WorkMode,
                IsFeatured = dto.IsFeatured,
                IsRecommended = dto.IsRecommended
            };

            if (dto.JobImageUrl != null)
            {
                if (dto.JobImageUrl.Length > 5 * 1024 * 1024) // 5MB limit
                {
                    return BadRequest("The image file is too large.");
                }

                var allowedExtensions = new[] { ".jpg", ".jpeg", ".png", ".gif" };
                var fileExtension = Path.GetExtension(dto.JobImageUrl.FileName).ToLower();
                if (!allowedExtensions.Contains(fileExtension))
                {
                    return BadRequest("Invalid file type. Only image files are allowed.");
                }

                string uploadFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "job_images");
                string fileName = await FileUploadHelper.SaveUploadedFileAsync(dto.JobImageUrl, uploadFolder);
                jobPosting.JobImageUrl = "/job_images/" + fileName;
            }

            _context.JobPostings.Add(jobPosting);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while creating the job posting.");
                return StatusCode(500, "An unexpected error occurred. Please try again later.");
            }

            return CreatedAtAction(nameof(GetJobPostingById), new { id = jobPosting.JobPostingId }, jobPosting);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetJobPostingById(int id)
        {
            var jobPosting = await _context.JobPostings
                .Include(jp => jp.Company)
                .FirstOrDefaultAsync(jp => jp.JobPostingId == id);

            if (jobPosting == null)
            {
                return NotFound();
            }

            return Ok(jobPosting);
        }

        [HttpGet("GetAllJobPostings")]
        public async Task<IActionResult> GetAllJobPostings(
            [FromQuery] string title,
            [FromQuery] string location,
            [FromQuery] int page = 1,
            [FromQuery] int pageSize = 10)
        {
            var jobPostingsQuery = _context.JobPostings.AsQueryable();

            if (!string.IsNullOrEmpty(title))
                jobPostingsQuery = jobPostingsQuery.Where(jp => jp.Title.ToLower().Contains(title.ToLower()));

            if (!string.IsNullOrEmpty(location))
                jobPostingsQuery = jobPostingsQuery.Where(jp => jp.Location.ToLower().Contains(location.ToLower()));

            var totalCount = await jobPostingsQuery.CountAsync();
            var jobPostings = await jobPostingsQuery
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .Select(jp => new
                {
                    title = jp.Title,
                    location = jp.Location,
                    salaryRange = jp.SalaryRange,
                    description = jp.Description,
                    jobType = jp.JobType,
                    workMode = jp.WorkMode,
                    jobImageUrl = jp.JobImageUrl,
                    companyName = jp.Company != null ? jp.Company.Name : null,
                    jobId = jp.JobPostingId
                })
                .ToListAsync();

            return Ok(new { TotalCount = totalCount, JobPostings = jobPostings });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateJobPosting(int id, [FromBody] UpdateJobPostingDto dto)
        {
            if (dto == null)
            {
                return BadRequest("Invalid data.");
            }

            var jobPosting = await _context.JobPostings.FindAsync(id);
            if (jobPosting == null)
            {
                return NotFound();
            }

            jobPosting.Title = dto.Title ?? jobPosting.Title;
            jobPosting.Location = dto.Location ?? jobPosting.Location;
            jobPosting.SalaryRange = dto.SalaryRange ?? jobPosting.SalaryRange;
            jobPosting.Description = dto.Description ?? jobPosting.Description;
            jobPosting.WorkMode = dto.WorkMode ?? jobPosting.WorkMode;
            jobPosting.JobType = dto.JobType ?? jobPosting.JobType;
            jobPosting.IsFeatured = dto.IsFeatured ?? jobPosting.IsFeatured;
            jobPosting.IsRecommended = dto.IsRecommended ?? jobPosting.IsRecommended;

            _context.JobPostings.Update(jobPosting);
            await _context.SaveChangesAsync();

            return Ok(jobPosting);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteJobPosting(int id)
        {
            var jobPosting = await _context.JobPostings.FindAsync(id);
            if (jobPosting == null) return NotFound();

            if (!string.IsNullOrEmpty(jobPosting.JobImageUrl))
            {
                var imagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", jobPosting.JobImageUrl.TrimStart('/'));

                if (global::System.IO.File.Exists(imagePath))
                    global::System.IO.File.Delete(imagePath);
            }

            _context.JobPostings.Remove(jobPosting);
            await _context.SaveChangesAsync();

            return NoContent();
        }


        [HttpGet("company/{companyId}")]
        public async Task<IActionResult> GetJobPostingsByCompany(int companyId)
        {
            var jobPostings = await _context.JobPostings
                .Where(jp => jp.CompanyId == companyId)
                .ToListAsync();

            if (jobPostings == null || !jobPostings.Any())
            {
                return NotFound();
            }

            return Ok(jobPostings);
        }

        [HttpGet("featured")]
        public async Task<IActionResult> GetFeaturedJobPostings()
        {
            var featuredJobPostings = await _context.JobPostings
                .Where(jp => jp.IsFeatured)
                .Select(jp => new
                {
                    title = jp.Title,
                    location = jp.Location,
                    salaryRange = jp.SalaryRange,
                    description = jp.Description,
                    jobType = jp.JobType,
                    workMode = jp.WorkMode,
                    jobImageUrl = jp.JobImageUrl,
                    companyName = jp.Company != null ? jp.Company.Name : null
                })
                .ToListAsync();

            return Ok(featuredJobPostings);
        }

        [HttpGet("recommended")]
        public async Task<IActionResult> GetRecommendedJobPostings()
        {
            var recommendedJobPostings = await _context.JobPostings
                .Where(jp => jp.IsRecommended)
                .Select(jp => new
                {
                    title = jp.Title,
                    location = jp.Location,
                    salaryRange = jp.SalaryRange,
                    description = jp.Description,
                    jobType = jp.JobType,
                    workMode = jp.WorkMode,
                    jobImageUrl = jp.JobImageUrl,
                    companyName = jp.Company != null ? jp.Company.Name : null
                })
                .ToListAsync();

            return Ok(recommendedJobPostings);
        }
    }
}