using System;
using System.Data.Common;

namespace SqsLibraries.DataAccess
{
    public class DataServer<C> where C : DbConnection, IDisposable
    {
        private C connection;
        private string _ConnectionString;
        private bool _Disposed;

        public DataServer() { }

        public DataServer(C connection)
        {
            this.connection = connection;
            connection.Open();
        }

        #region Public Properties

        public string ConnectionString
        {
            get
            {
                return _ConnectionString;
            }
            set
            {
                _ConnectionString = value;
            }
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

        #endregion

        #region Public Methods

        protected virtual void Dispose(bool disposing)
        {
            if (!this._Disposed)
            {
                if (disposing)
                {
                    connection.Dispose();
                }
            }

            this._Disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        #endregion
    }
}
