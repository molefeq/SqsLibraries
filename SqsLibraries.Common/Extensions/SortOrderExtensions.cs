using SqsLibraries.Common.Enums;

namespace SqsLibraries.Common.Extensions
{
    public static class SortOrderExtensions
    {
        public static string ToSqlString(this SortOrder sortOrder)
        {
            if (sortOrder == SortOrder.DESC)
            {
                return "DESC";
            }

            return "ASC";
        }
    }
}
