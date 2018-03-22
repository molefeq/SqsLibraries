using Npgsql;
using NpgsqlTypes;

using SqsLibraries.Common.Extensions;
using SqsLibraries.Common.Utilities.ResponseObjects;

using SqsLibraries.DataAccess;
using SqsLibraries.DataAccess.Exceptions;
using SqsLibraries.DataAccess.Models;

using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Threading.Tasks;

namespace SqsLibraries.PostgresLibrary
{
    public class StoredProcedureManager : CommandTypeManager<NpgsqlParameter, NpgsqlConnection>
    {
        public StoredProcedureManager(NpgsqlConnection pgsqlConnection) : base(pgsqlConnection) { }

        #region Public Methods

        public override async Task ExecuteNoneResultsAsync(string commandText, params NpgsqlParameter[] sqlParameters)
        {
            try
            {
                Initialise(commandText);

                using (NpgsqlCommand sqlCommand = new NpgsqlCommand(commandText, Connection) { CommandType = CommandType.StoredProcedure })
                {
                    SetSqlCommandParameters(sqlCommand, false, sqlParameters);
                    await sqlCommand.ExecuteNonQueryAsync();
                }
            }
            catch (Exception ex)
            {
                throw new StoredProcedureException(ex.Message);
            }
        }

        public override void ExecuteNoneResults(string commandText, params NpgsqlParameter[] sqlParameters)
        {
            try
            {
                Initialise(commandText);

                using (NpgsqlCommand sqlCommand = new NpgsqlCommand(commandText, Connection) { CommandType = CommandType.StoredProcedure })
                {
                    SetSqlCommandParameters(sqlCommand, false, sqlParameters);
                    sqlCommand.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                throw new StoredProcedureException(ex.Message);
            }
        }

        public override DataSet GetDataSet(string commandText, params NpgsqlParameter[] sqlParameters)
        {
            try
            {
                DataSet dataSet = new DataSet();

                Initialise(commandText);

                using (NpgsqlCommand sqlCommand = new NpgsqlCommand(commandText, Connection) { CommandType = CommandType.StoredProcedure })
                {
                    SetSqlCommandParameters(sqlCommand, false, sqlParameters);
                    NpgsqlDataAdapter sqlDataAdapter = new NpgsqlDataAdapter(sqlCommand);

                    sqlDataAdapter.Fill(dataSet);
                }

                return dataSet;
            }
            catch (Exception ex)
            {
                throw new StoredProcedureException(ex.Message);
            }
        }

        public override DbDataReader ExecuteReader(string commandText, params NpgsqlParameter[] sqlParameters)
        {
            try
            {
                Initialise(commandText);

                using (NpgsqlCommand sqlCommand = new NpgsqlCommand(commandText, Connection) { CommandType = CommandType.StoredProcedure })
                {
                    SetSqlCommandParameters(sqlCommand, false, sqlParameters);
                    return sqlCommand.ExecuteReader();
                }
            }
            catch (Exception ex)
            {
                throw new StoredProcedureException(ex.Message);
            }
        }

        public override async Task<DbDataReader> ExecuteReaderAsync(string commandText, params NpgsqlParameter[] sqlParameters)
        {
            try
            {
                Initialise(commandText);

                using (NpgsqlCommand sqlCommand = new NpgsqlCommand(commandText, Connection) { CommandType = CommandType.StoredProcedure })
                {
                    SetSqlCommandParameters(sqlCommand, false, sqlParameters);

                    return await sqlCommand.ExecuteReaderAsync();
                }
            }
            catch (Exception ex)
            {
                throw new StoredProcedureException(ex.Message);
            }
        }

        public override DbResponse ExecuteReaderWithValidation(string commandText, params NpgsqlParameter[] sqlParameters)
        {
            try
            {
                DbResponse dbResponse = new DbResponse();

                Initialise(commandText);

                using (NpgsqlCommand sqlCommand = new NpgsqlCommand(commandText, Connection) { CommandType = CommandType.StoredProcedure })
                {
                    SetSqlCommandParameters(sqlCommand, true, sqlParameters);

                    dbResponse.DataReader = sqlCommand.ExecuteReader();
                    dbResponse.ReturnValue = GetReturnValue(sqlCommand);
                    dbResponse.ValidationMessages = GetValidationMessages(sqlCommand);

                    return dbResponse;
                }
            }
            catch (Exception ex)
            {
                throw new StoredProcedureException(ex.Message);
            }
        }

        public override async Task<DbResponse> ExecuteReaderWithValidationAsync(string commandText, params NpgsqlParameter[] sqlParameters)
        {
            try
            {
                DbResponse dbResponse = new DbResponse();

                Initialise(commandText);

                using (NpgsqlCommand sqlCommand = new NpgsqlCommand(commandText, Connection) { CommandType = CommandType.StoredProcedure })
                {
                    SetSqlCommandParameters(sqlCommand, true, sqlParameters);

                    dbResponse.DataReader = await sqlCommand.ExecuteReaderAsync();
                    dbResponse.ReturnValue = GetReturnValue(sqlCommand);
                    dbResponse.ValidationMessages = GetValidationMessages(sqlCommand);

                    return dbResponse;
                }
            }
            catch (Exception ex)
            {
                throw new StoredProcedureException(ex.Message);
            }
        }

        #endregion

        #region Private Methods

        private void Initialise(string commandText)
        {
            if (string.IsNullOrEmpty(commandText))
            {
                throw new StoredProcedureException(string.Format("{0} cannot be null or empty.", "Store Procedure Name"));
            }

            if (Connection.State != ConnectionState.Open)
            {
                throw new StoredProcedureException("Database connection is not opened");
            }
        }

        private void SetSqlCommandParameters(NpgsqlCommand sqlCommand, bool includeReturnValue, params NpgsqlParameter[] sqlParameters)
        {
            if (sqlParameters != null && sqlParameters.Length > 0)
            {
                foreach (NpgsqlParameter sqlParameter in sqlParameters)
                {
                    sqlCommand.Parameters.Add(sqlParameter);
                }

                if (includeReturnValue)
                {
                    sqlCommand.Parameters.Add("@RETURN_VALUE", NpgsqlDbType.Integer).Direction = ParameterDirection.ReturnValue;
                }
            }
        }

        private int? GetReturnValue(NpgsqlCommand sqlCommand)
        {
            if (sqlCommand.Parameters["@RETURN_VALUE"] == null || sqlCommand.Parameters["@RETURN_VALUE"].Value == null)
            {
                return null;
            }

            return sqlCommand.Parameters["@RETURN_VALUE"].Value.ToNullableInteger();
        }

        private List<ResponseMessage> GetValidationMessages(NpgsqlCommand sqlCommand)
        {
            List<ResponseMessage> validationMessages = new List<ResponseMessage>();

            if (sqlCommand.Parameters["ValidationMessages"] == null || sqlCommand.Parameters["ValidationMessages"].Value == null)
            {
                return validationMessages;
            }

            string validationMessage = sqlCommand.Parameters["ValidationMessages"].Value.ToString();

            return validationMessage.ToResponseMessages();
        }

        #endregion
    }
}

