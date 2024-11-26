using CV_Filtation_System.Core.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace CV_Filtation_System.Repository.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Company> Companies { get; set; }
        public DbSet<JobPosting> JobPostings { get; set; }
        public DbSet<Skill> Skills { get; set; }
        public DbSet<UserSkill> UserSkills { get; set; }
        public DbSet<UserCompany> UserCompanies { get; set; }
        public DbSet<CompanyJobPosting> CompanyJobPostings { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserSkill>()
                .HasKey(us => new { us.UserId, us.SkillId });

            modelBuilder.Entity<UserCompany>()
                .HasKey(uc => new { uc.UserId, uc.CompanyId });

            modelBuilder.Entity<CompanyJobPosting>()
                .HasKey(cj => new { cj.CompanyId, cj.JobId });
        }
    }
}
