using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CV_Filtation_System.Core.Entities
{
    public class CompanyJobPosting
    {
        public int CompanyId { get; set; }
        public Company Company { get; set; } = null!;
        public int JobId { get; set; }
        public JobPosting JobPosting { get; set; } = null!;
    }
}
