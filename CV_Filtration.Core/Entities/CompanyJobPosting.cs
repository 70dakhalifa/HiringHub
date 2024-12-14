using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CV_Filtation_System.Core.Entities
{
    public class CompanyJobPosting
    {
        public int CompanyJobPostingId { get; set; }
        public int JobPostingId { get; set; }
        public int CompanyId { get; set; }

        // Navigation properties
        public JobPosting JobPosting { get; set; }
        public Company Company { get; set; }
    }
}
