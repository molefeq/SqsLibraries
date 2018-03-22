using SqsLibraries.Common.Email.Models;

using System;
using System.Collections.Generic;
using System.Net.Mail;
using System.Text;

namespace SqsLibraries.Common.Email
{
    public class EmailHandler
    {
        public static void SendEmail(string smtpServerAddress, int smtpServerPortNumber,
                                     string emailFrom, string emailTo, List<string> ccEmailAddresses,
                                     string emailSubject, string emailBody, List<EmailAttachment> emailAttachments)
        {
            try
            {
                List<Attachment> attachments = new List<Attachment>();

                foreach (EmailAttachment emailAttachment in emailAttachments)
                {
                    attachments.Add(new Attachment(emailAttachment.AttachmentStream, emailAttachment.AttachmentFileName, emailAttachment.ContentType));
                }

                SendEmail(smtpServerAddress, smtpServerPortNumber, emailFrom, emailTo, ccEmailAddresses, emailSubject, emailBody, attachments);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public static void SendEmail(string smtpServerAddress, int smtpServerPortNumber,
                                     string emailFrom, string emailTo, List<string> ccEmailAddresses,
                                     string emailSubject, string emailBody)
        {
            SendEmail(smtpServerAddress, smtpServerPortNumber, emailFrom, emailTo, ccEmailAddresses, emailSubject, emailBody, new List<Attachment>());
        }

        private static void SendEmail(string smtpServerAddress, int smtpServerPortNumber,
                                     string emailFrom, string emailTo, List<string> ccEmailAddresses,
                                     string emailSubject, string emailBody, List<Attachment> attachments)
        {
            try
            {
                MailMessage mailMessage = new MailMessage();
                SmtpClient smtpClient = new SmtpClient(smtpServerAddress, smtpServerPortNumber);

                mailMessage.Priority = MailPriority.High;
                mailMessage.From = new MailAddress(emailFrom);
                mailMessage.Subject = emailSubject;

                foreach (var address in emailTo.Split(new[] { ";" }, StringSplitOptions.RemoveEmptyEntries))
                {
                    mailMessage.To.Add(new MailAddress(address));
                }

                if (attachments != null && attachments.Count > 0)
                {
                    attachments.ForEach(a => mailMessage.Attachments.Add(a));
                }

                if (ccEmailAddresses != null)
                {
                    ccEmailAddresses.ForEach(item => mailMessage.CC.Add(new MailAddress(item)));
                }

                mailMessage.IsBodyHtml = true;
                mailMessage.Body = emailBody;
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
