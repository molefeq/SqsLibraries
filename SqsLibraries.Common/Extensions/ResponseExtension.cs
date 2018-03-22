using SqsLibraries.Common.Utilities.ResponseObjects;
using SqsLibraries.Common.Utilities.ResponseObjects.Enums;
using System.Linq;

namespace SqsLibraries.Common.Extensions
{
    public static class ResponseExtension
    {
        public static bool IsError<T>(this Response<T> response) where T : class
        {
            return response.Messages != null && response.Messages.Where(r => r.MessageType == MessageType.ERROR).Count() > 0;
        }
    }
}
