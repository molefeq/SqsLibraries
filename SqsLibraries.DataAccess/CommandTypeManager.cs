using SqsLibraries.DataAccess.Models;

using System.Data;
using System.Data.Common;
using System.Threading.Tasks;

namespace SqsLibraries.DataAccess
{
    public abstract class CommandTypeManager<T, C> where T : DbParameter where C : DbConnection
    {
        private C connection;

        public CommandTypeManager(C connection)
        {
            this.connection = connection;
        }

        public C Connection
        {
            get
            {
                return connection;
            }
            set
            {
                connection = value;
            }
        }

        public abstract void ExecuteNoneResults(string commandText, params T[] sqlParameters);

        public abstract Task ExecuteNoneResultsAsync(string commandText, params T[] sqlParameters);

        public abstract DataSet GetDataSet(string commandText, params T[] sqlParameters);

        public abstract DbDataReader ExecuteReader(string commandText, params T[] sqlParameters);

        public abstract Task<DbDataReader> ExecuteReaderAsync(string commandText, params T[] sqlParameters);

        public abstract DbResponse ExecuteReaderWithValidation(string commandText, params T[] sqlParameters);

        public abstract Task<DbResponse> ExecuteReaderWithValidationAsync(string commandText, params T[] sqlParameters);
    }
}
