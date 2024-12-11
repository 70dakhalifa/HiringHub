using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CV_Filtation_System.Services
{
    public interface ITokenService
    {
        string GenerateToken(string username, string role);
    }

}
