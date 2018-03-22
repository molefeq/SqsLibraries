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

        public static Response<T> Success(T item)
        {
            return new Response<T>
            {
                Item = item
            };
        }

        public static Response<T> Fail(ResponseMessage message)
        {
            return new Response<T>
            {
                Messages = new List<ResponseMessage> { message }
            };
        }

        public static Response<T> Fail(List<ResponseMessage> messages)
        {
            return new Response<T>
            {
                Messages = messages
            };
        }
    }
}
