using MailKit.Net.Smtp;

using MimeKit;

using SqsLibraries.Common.Email.Models;

namespace SqsLibraries.Common.Email
{
    public class SmtpMailGunEmailHandler : IEmailHandler
    {
        public void SendEmail(EmailContent email)
        {
            MimeMessage mail = new MimeMessage();

            mail.From.Add(new MailboxAddress(email.From.Name, email.From.Address));

            if (email.To != null && email.To.Count > 0)
            {
                email.To.ForEach(t => mail.To.Add(new MailboxAddress(t.Name, t.Address)));
            }

            if (email.CC != null && email.CC.Count > 0)
            {
                email.CC.ForEach(t => mail.Cc.Add(new MailboxAddress(t.Name, t.Address)));
            }

            mail.Subject = email.Subject;

            var body = new BodyBuilder();

            body.HtmlBody = email.HtmlBody;

            if (email.Attachments != null && email.Attachments.Count > 0)
            {
                email.Attachments.ForEach(a => body.Attachments.Add(a.AttachmentFileName, a.AttachmentStream, ContentType.Parse(a.ContentType)));
            }

            mail.Body = body.ToMessageBody();

            using (var client = new SmtpClient())
            {
                client.ServerCertificateValidationCallback = (s, c, h, e) => true;

                client.Connect(email.Configuration.SmtpServer, email.Configuration.SmtpPortNumber, false);
                client.AuthenticationMechanisms.Remove("XOAUTH2");
                client.Authenticate(email.Configuration.Username, email.Configuration.Password);

                client.Send(mail);
                client.Disconnect(true);
            }
        }
    }
}
