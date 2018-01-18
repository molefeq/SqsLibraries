using System.Collections.Generic;

namespace SqsLibraries.Common.Utilities.ResponseObjects
{
    public class Response<T> where T : class
    {
        public List<ResponseMessage> Messages { get; set; }
        public T Item { get; set; }

        public Response()
        {
            Messages = new List<ResponseMessage>();
        }
    }
}
