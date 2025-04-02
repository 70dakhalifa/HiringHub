using CV.Filtation.System.API.DTO;
using CV.Filtation.System.API.Helpers;
using CV_Filtation_System.Core.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace CV.Filtation.System.API.Controllers
{
    public class JobApplicationsController : APIBaseController
    {
        private readonly AppDbContext _context;
        private readonly UserManager<User> _userManager;

        public JobApplicationsController(
            AppDbContext context,
            UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // DTOs



        public class UpdateStatusDto
        {
            [Required]
            public string Status { get; set; }
        }

        [HttpPost]
        public async Task<ActionResult> CreateApplication(CreateApplicationDto dto)
        {
            var user = await _userManager.FindByIdAsync(dto.UserId);
            if (user == null) return Unauthorized();

            // Check for existing application
            var exists = await _context.JobApplication
                .AnyAsync(ja => ja.UserId == user.Id && ja.JobPostingId == dto.JobPostingId);

            if (exists) return Conflict("You've already applied to this job");

            var exists2 = await _context.JobPostings
                .AnyAsync(ja => ja.JobPostingId == dto.JobPostingId);

            if (!exists2) return Conflict("The Job Id Do not exist");

            string uploadFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Application_Job_CVs");
            string fileName = await FileUploadHelper.SaveUploadedFileAsync(dto.CV, uploadFolder);

            var application = new JobApplication
            {
                UserId = dto.UserId,
                JobPostingId = dto.JobPostingId,
                ApplicationDate = DateTime.UtcNow,
                Status = "Pending",
                CV_FilePath = "/Application_Job_CVs/" + fileName,
            };

            _context.JobApplication.Add(application);
            await _context.SaveChangesAsync();

            return Ok(new { Message = "Job applied successfully!" });
        }

        // GET: api/JobApplications/user
        [HttpGet("user")]
        public async Task<ActionResult<IEnumerable<ApplicationDto>>> GetUserApplications(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null) return NotFound("User not found");

            var applications = await _context.JobApplication
                .Where(ja => ja.UserId == user.Id).Include(ja => ja.JobPosting)
                .ThenInclude(jp => jp.Company)
                .OrderByDescending(ja => ja.ApplicationDate)
                .Select(ja => new ApplicationUserDTO
                {
                    ApplicationId = ja.Id,
                    Status = ja.Status,
                    ApplicationDate = ja.ApplicationDate,
                    CV_Path = ja.CV_FilePath,
                    JobPosting = new JobPostingDto
                    {
                        JobPostingId = ja.JobPosting.JobPostingId,
                        Title = ja.JobPosting.Title,
                        Location = ja.JobPosting.Location,
                        EmploymentType = ja.JobPosting.WorkMode,
                        SalaryRange = ja.JobPosting.SalaryRange,
                        Description = ja.JobPosting.Description,
                    },
                    CompanyName = ja.JobPosting.Company.Name
                })
                .ToListAsync();

            return Ok(applications);
        }

        //[HttpGet("company")]
        //public async Task<ac>

        // GET: api/JobApplications/job/5
        [HttpGet("job/{jobPostingId}")]
        public async Task<ActionResult<IEnumerable<ApplicationDto>>> GetJobApplications(int jobPostingId)
        {
            var applications = await _context.JobApplication
                .Include(ja => ja.User)
                .Include(ja => ja.JobPosting)
                .Where(ja => ja.JobPostingId == jobPostingId)
                .Select(ja => new ApplicationDto
                {
                    ApplicationId = ja.Id,
                    Status = ja.Status,
                    ApplicationDate = ja.ApplicationDate,
                    CV_Path = ja.CV_FilePath,
                    Applicant = new UserProfileDTO
                    {
                        FName = ja.User.FName,
                        LName = ja.User.LName,
                        Email = ja.User.Email,
                        Address = ja.User.Address,
                        City = ja.User.City,
                        Phone = ja.User.PhoneNumber
                    }
                })
                .ToListAsync();

            return Ok(applications);
        }
        // PUT: api/JobApplications/5/status
        //[HttpPut("{id}/status")]
        //public async Task<IActionResult> UpdateApplicationStatus(int id, UpdateStatusDto dto)
        //{
        //    var application = await _context.JobApplications
        //        .Include(ja => ja.JobPosting)
        //        .FirstOrDefaultAsync(ja => ja.ApplicationId == id);

        //    if (application == null) return NotFound();

        //    // Verify company owns the job posting
        //    var company = await _context.Companies
        //        .FirstOrDefaultAsync(c => c.Email == User.Identity.Name);

        //    if (application.JobPosting.CompanyId != company.CompanyId)
        //        return Forbid();

        //    application.Status = dto.Status;
        //    await _context.SaveChangesAsync();

        //    return NoContent();
        //}

        //DELETE: api/JobApplications/5

        [HttpDelete("{applicationId}")]
        public async Task<IActionResult> DeleteApplication(int applicationId)
        {
            var application = await _context.JobApplication.FindAsync(applicationId);
            if (application == null) return NotFound("Application not found");

            _context.JobApplication.Remove(application);
            await _context.SaveChangesAsync();

            return Content("Job Application has been deleted successfully");
        }
    }
}
