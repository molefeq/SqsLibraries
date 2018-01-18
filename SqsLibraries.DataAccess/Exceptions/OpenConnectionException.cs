using System;

namespace SqsLibraries.DataAccess.Exceptions
{
    public class OpenConnectionException : Exception
    {
        public OpenConnectionException()
            : base()
        {

        }
        public OpenConnectionException(string message)
            : base(message)
        {

        }
    }
}
