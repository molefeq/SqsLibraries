using SqsLibraries.Common.Utilities.ResponseObjects.Enums;

namespace SqsLibraries.Common.Utilities.ResponseObjects
{
    public class ResponseMessage
    {
        private MessageType messageType = MessageType.ERROR;

        public MessageType MessageType
        {
            get
            {
                return messageType;
            }
            set
            {
                messageType = value;
            }
        }

        public string FieldName { get; set; }
        public string Message { get; set; }
    }
}
