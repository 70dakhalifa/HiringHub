

namespace CV_Filtation_System.Services.Services
{
    public interface ITokenService
    {
        string GenerateToken(string username, string role);
    }

}
