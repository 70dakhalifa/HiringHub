using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CV_Filtation_System.Core.Entities
{
    public class JobApplication
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public int JobPostingId { get; set; }
        public DateTime ApplicationDate { get; set; } = DateTime.UtcNow;
        public string Status { get; set; }
        public string CV_FilePath { get; set; }
        public User User { get; set; }
        public JobPosting JobPosting { get; set; }
    }
}
