using SqsLibraries.Common.Extensions;
using SqsLibraries.Common.Utilities.ResponseObjects;

using SqsLibraries.DataAccess.Extensions;
using SqsLibraries.DataAccess.Models;

using System;
using System.Collections.Generic;
using System.Data.Common;

namespace SqsLibraries.DataAccess
{
    public class DataAccessEntitySet<TEntity, T, C> where TEntity : class where T : DbParameter where C : DbConnection
    {
        private CommandTypeManager<T, C> commandTypeManager;

        public DataAccessEntitySet(CommandTypeManager<T, C> commandTypeManager, C connection)
        {
            this.commandTypeManager = commandTypeManager;
            this.commandTypeManager.Connection = connection;
        }

        public DataAccessEntitySet(CommandTypeManager<T, C> commandTypeManager)
        {
            this.commandTypeManager = commandTypeManager;
        }

        public CommandTypeManager<T, C> CommandTypeManager
        {
            get
            {
                return commandTypeManager;
            }
        }

        public virtual TEntity Find(string commandText, Func<DbDataReader, TEntity> entityMapper, params T[] parameters)
        {
            TEntity entity = null;

            using (var reader = CommandTypeManager.ExecuteReader(commandText, parameters))
            {
                while (reader.Read())
                {
                    entity = entityMapper(reader);
                }
            }

            return entity;
        }

        public virtual List<TEntity> GetEntities(string commandText, Func<DbDataReader, TEntity> entityMapper, params T[] parameters)
        {
            List<TEntity> items = new List<TEntity>();

            using (DbDataReader sqlDataReader = CommandTypeManager.ExecuteReader(commandText, parameters))
            {
                while (sqlDataReader.Read())
                {
                    items.Add(entityMapper(sqlDataReader));
                }
            }

            return items;
        }

        public virtual Result<TEntity> GetPagedEntities(string commandText, Func<DbDataReader, TEntity> entityMapper, params T[] parameters)
        {
            Result<TEntity> result = new Result<TEntity>();

            using (DbDataReader sqlDataReader = CommandTypeManager.ExecuteReader(commandText, parameters))
            {
                while (sqlDataReader.Read())
                {
                    result.TotalItems = sqlDataReader["TotalRows"].ToInteger();
                }

                sqlDataReader.NextResult();

                while (sqlDataReader.Read())
                {
                    result.Items.Add(entityMapper(sqlDataReader));
                }
            }

            return result;
        }

        public virtual Response<TEntity> Save(string commandText, Func<DbDataReader, TEntity> entityMapper, params T[] parameters)
        {
            DbResponse dbResponse = CommandTypeManager.ExecuteReaderWithValidation(commandText, parameters);

            return dbResponse.ToReponse<TEntity>(entityMapper);
        }
    }
}
