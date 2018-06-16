using SqsLibraries.Common.Email.Models;

namespace SqsLibraries.Common.Email
{
    public interface IEmailHandler
    {
        void SendEmail(EmailContent email);
    }
}
