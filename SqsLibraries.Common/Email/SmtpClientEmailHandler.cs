using SqsLibraries.Common.Email.Models;

using System;
using System.Net.Mail;
using System.Text;

namespace SqsLibraries.Common.Email
{
    public class SmtpClientEmailHandler : IEmailHandler
    {
        public void SendEmail(EmailContent email)
        {
            try
            {
                MailMessage mailMessage = new MailMessage();
                SmtpClient smtpClient = new SmtpClient(email.Configuration.SmtpServer, email.Configuration.SmtpPortNumber);

                mailMessage.Priority = MailPriority.High;
                mailMessage.From = new MailAddress(email.From.Address, email.From.Name);
                mailMessage.Subject = email.Subject;

                foreach (var address in email.To)
                {
                    mailMessage.To.Add(new MailAddress(address.Address, address.Name));
                }

                if (email.Attachments != null && email.Attachments.Count > 0)
                {
                    email.Attachments.ForEach(a => mailMessage.Attachments.Add(new Attachment(a.AttachmentStream, a.AttachmentFileName, a.ContentType)));
                }

                if (email.CC != null && email.CC.Count > 0)
                {
                    email.CC.ForEach(item => mailMessage.CC.Add(new MailAddress(item.Address, item.Name)));
                }

                mailMessage.IsBodyHtml = true;
                mailMessage.Body = email.HtmlBody;
                smtpClient.Send(mailMessage);
            }
            catch (SmtpFailedRecipientsException frs)
            {
                StringBuilder sb = new StringBuilder();
                sb.AppendLine("Failed to send to: ");

                foreach (var fail in frs.InnerExceptions)
                {
                    sb.AppendLine(fail.FailedRecipient);
                }

                throw new Exception(sb.ToString());
            }
            catch (SmtpFailedRecipientException fr)
            {
                throw new Exception("Failed to send mail to: " + fr.FailedRecipient);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
