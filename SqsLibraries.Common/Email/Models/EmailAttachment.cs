using System.IO;

namespace SqsLibraries.Common.Email.Models
{
    public class EmailAttachment
    {
        public Stream AttachmentStream { get; set; }
        public string AttachmentFileName { get; set; }
        public string ContentType { get; set; }
    }
}
