using CV.Filtation.System.API.DTO;
using CV_Filtation_System.Core.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CV.Filtation.System.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserFavoriteJobsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public UserFavoriteJobsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/UserFavoriteJobs/{userId}
        [HttpGet("{userId}")]
        public async Task<ActionResult<IEnumerable<UserFavoriteJob>>> GetUserFavorites(string userId)
        {
            var favorites = await _context.UserFavoriteJobs
                .Include(uf => uf.JobPosting) // Include JobPosting details
                .Where(uf => uf.UserId == userId)
                .Select(uf => new JobPosting // Project into JobPostingDto
                {
                    JobPostingId = uf.JobPosting.JobPostingId,
                    Title = uf.JobPosting.Title,
                    Location = uf.JobPosting.Location,
                    SalaryRange = uf.JobPosting.SalaryRange,
                    Description = uf.JobPosting.Description,
                    CompanyId = uf.JobPosting.CompanyId,
                    JobType = uf.JobPosting.JobType,
                    WorkMode = uf.JobPosting.WorkMode,
                    JobImageUrl = uf.JobPosting.JobImageUrl,
                    IsFeatured = uf.JobPosting.IsFeatured,
                    IsRecommended = uf.JobPosting.IsRecommended
                })
                .ToListAsync();

            if (!favorites.Any())
            {
                return NotFound("No favorite jobs found for this user.");
            }

            return Ok(favorites);

        }

        // POST: api/UserFavoriteJobs
        [HttpPost]
        public async Task<ActionResult<UserFavoriteJob>> AddFavorite(string UserId, int JobPostingId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Check if the favorite already exists
            var exists = await _context.UserFavoriteJobs
                .AnyAsync(uf => uf.UserId == UserId && uf.JobPostingId == JobPostingId);

            if (exists)
            {
                return Conflict("This job is already in the user's favorites.");
            }

            var ex2 = await _context.Users.AnyAsync(u => u.Id == UserId);

            if (!ex2)
            {
                return Conflict("User do not exist");
            }


            var ex = await _context.JobPostings.AnyAsync(je => je.JobPostingId == JobPostingId);

            if (!ex)
            {
                return Conflict("Job do not exist");
            }

            var favorite = new UserFavoriteJob
            {
                UserId = UserId,
                JobPostingId = JobPostingId,
                FavoriteDate = DateTime.UtcNow
            };

            _context.UserFavoriteJobs.Add(favorite);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetUserFavorites), new { userId = favorite.UserId }, favorite);
        }

        // DELETE: api/UserFavoriteJobs/{userId}/{jobPostingId}
        [HttpDelete("{userId}/{jobPostingId}")]
        public async Task<IActionResult> RemoveFavorite(string userId, int jobPostingId)
        {
            var favorite = await _context.UserFavoriteJobs
                .FirstOrDefaultAsync(uf => uf.UserId == userId && uf.JobPostingId == jobPostingId);

            if (favorite == null)
            {
                return NotFound("Favorite job not found.");
            }

            _context.UserFavoriteJobs.Remove(favorite);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // GET: api/UserFavoriteJobs/{userId}/{jobPostingId}
        [HttpGet("{userId}/{jobPostingId}")]
        public async Task<ActionResult<bool>> IsJobFavorited(string userId, int jobPostingId)
        {
            var isFavorited = await _context.UserFavoriteJobs
                .AnyAsync(uf => uf.UserId == userId && uf.JobPostingId == jobPostingId);

            return Ok(isFavorited);
        }
    }
}
