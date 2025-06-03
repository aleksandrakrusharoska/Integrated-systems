using LibraryDomain.Email;

namespace LibraryService.Interfaces
{
    public interface IEmailService
    {
        Task SendEmail(EmailMessage message);
    }
}