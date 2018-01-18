using SqsLibraries.Common.Utilities.ResponseObjects;

using System.Collections.Generic;
using System.Data.Common;

namespace SqsLibraries.DataAccess.Models
{
    public class DbResponse
    {
        public int? ReturnValue { get; set; }
        public DbDataReader DataReader { get; set; }
        public List<ResponseMessage> ValidationMessages { get; set; }
    }
}
