using System;

namespace SqsLibraries.DataAccess.Exceptions
{
    public class StoredProcedureException : Exception
    {
        public StoredProcedureException()
            : base()
        {

        }
        public StoredProcedureException(string message)
            : base(message)
        {

        }
    }
}
