using System;

namespace SqsLibraries.Common.Utilities
{
    public static class Constants
    {
        public const string DateReportFileFormat = "dd_MMM_yyyy";
        public const string DateTimeFileFormat = "ddMMyyyyHHmmss";
        public const string DateFileFormat = "ddMMyyyy";
        public const string DateFormat = "dd/MM/yyyy";
        public const string DateTimeFormat = "dd/MM/yyyy HH:mm:ss";
        public const string TimeFormat = "HH:mm:ss";
        public const DayOfWeek StartOfWeek = DayOfWeek.Monday;
        public const decimal VatRate = 0.14m;
        public const string MoneyFormat = "#0.00";
        public const string MoneyDisplayFormat = "# ###.00";
        public const string PercentageFormat = "#0.00";
        public const decimal VatRate100 = 14;
    }
}
