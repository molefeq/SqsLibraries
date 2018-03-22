using SqsLibraries.Common.Enums;

namespace SqsLibraries.Common.Utilities.ResponseObjects
{
    public class PageData
    {
        public int? Take { get; set; }
        public int? Skip { get; set; }
        public SortOrder SortOrder { get; set; }
        public string SortColumn { get; set; }
        public bool IncludeAllData { get; set; }
    }
}
