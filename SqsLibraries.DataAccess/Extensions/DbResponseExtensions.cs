using SqsLibraries.Common.Utilities.ResponseObjects;
using SqsLibraries.DataAccess.Models;

using System;
using System.Data.Common;
using System.Linq;

namespace SqsLibraries.DataAccess.Extensions
{
    public static class DbResponseExtensions
    {
        public static Response<T> ToReponse<T>(this DbResponse dbResponse, Func<DbDataReader, T> entityMapper) where T : class
        {
            Response<T> response = new Response<T>();

            if (dbResponse.ReturnValue != null && dbResponse.ReturnValue.Value < 0 && dbResponse.ValidationMessages.Count() > 0)
            {
                response.Messages = dbResponse.ValidationMessages;
                return response;
            }

            using (dbResponse.DataReader)
            {
                if (dbResponse.DataReader.Read())
                {
                    response.Item = entityMapper(dbResponse.DataReader);
                }
            }

            return response;
        }
    }
}
