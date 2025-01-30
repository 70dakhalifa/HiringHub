using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CV_Filtation_System.Services.Models
{
    public class LoginResponse
    {
        public string Status { get; set; }
        public string Message { get; set; }
        public string Token { get; set; }
        public DateTime? Expiration { get; set; }
    }

}
