namespace MovieCaseDev.Services.Abstract
{
    public interface IEMailService
    {
        Task SendEmailAsync(string toEmail, string subject, string body);
    }
}
