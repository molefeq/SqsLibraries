using System;

namespace SqsLibraries.DataAccess.Exceptions
{
    public class DataAccessException : Exception
    {
        public DataAccessException()
            : base()
        {

        }
        public DataAccessException(string message)
            : base(message)
        {

        }
    }
}
