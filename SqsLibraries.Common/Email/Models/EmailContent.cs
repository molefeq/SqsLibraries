using System.Collections.Generic;

namespace SqsLibraries.Common.Email.Models
{
    public class EmailContent
    {
        public EmailConfiguration Configuration { get; set; }
        public EmailAddress From { get; set; }
        public List<EmailAddress> To { get; set; }
        public List<EmailAddress> CC { get; set; }
        public string Subject { get; set; }
        public string HtmlBody { get; set; }
        public List<EmailAttachment> Attachments { get; set; }
    }
}
