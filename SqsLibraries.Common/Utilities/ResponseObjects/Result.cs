using System;
using System.Collections.Generic;
using System.Text;

namespace SqsLibraries.Common.Utilities.ResponseObjects
{
    public class Result<T> where T : class
    {
        public int TotalItems { get; set; }
        public List<T> Items { get; set; }

        public Result()
        {
            Items = new List<T>();
        }
    }
}
