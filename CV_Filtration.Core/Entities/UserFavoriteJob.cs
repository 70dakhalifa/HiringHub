using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CV_Filtation_System.Core.Entities
{
    public class UserFavoriteJob
    {
        public string UserId { get; set; }
        public int JobPostingId { get; set; }
        public DateTime FavoriteDate { get; set; } = DateTime.UtcNow;

        public User User { get; set; }
        public JobPosting JobPosting { get; set; }
    }
}
