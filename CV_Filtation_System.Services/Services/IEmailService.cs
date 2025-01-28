using CV_Filtation_System.Services.Models;


namespace CV_Filtation_System.Services.Services
{
    public interface IEmailService
    {
        void SendEmail(Message message);    
    }
}
