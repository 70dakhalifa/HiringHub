using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CV_Filtation_System.Core.Entities
{
    public class UserCompany
    {
        public int UserId { get; set; }
        public User User { get; set; } = null!;
        public int CompanyId { get; set; }
        public Company Company { get; set; } = null!;
        public int Score { get; set; }
    }

}
