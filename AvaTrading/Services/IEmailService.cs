namespace AvaTrading.Services
{
    public interface IEmailService
    {
        Task<bool> SendEmail(string email, string subject, string body);
    }
}