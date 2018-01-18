using SqsLibraries.Common.Utilities;
using SqsLibraries.Common.Utilities.ResponseObjects;

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text.RegularExpressions;

namespace SqsLibraries.Common.Extensions
{
    public static class StringExtensions
    {
        public static List<ResponseMessage> ToResponseMessages(this string stringValue, string messageSeperator = "|")
        {
            List<ResponseMessage> responseMessages = new List<ResponseMessage>();

            if (string.IsNullOrEmpty(stringValue))
            {
                return responseMessages;
            }

            var responseFieldMessages = stringValue.Split(new string[] { messageSeperator }, StringSplitOptions.RemoveEmptyEntries);

            for (int i = 0; i < responseFieldMessages.Length; i++)
            {
                var responseFieldMessage = responseFieldMessages[i].Trim();
                var responseFieldMessageProperties = Regex.Split(responseFieldMessage, "Mesaage=", RegexOptions.IgnorePatternWhitespace);

                string fieldName = Regex.Split(responseFieldMessageProperties[0].Trim(), "FieldName=")[1];
                string fieldMessage = responseFieldMessageProperties[1];

                responseMessages.Add(new ResponseMessage { FieldName = fieldName, Message = fieldMessage });
            }

            return responseMessages;
        }

        public static DateTime? StringToDateTime(this string dateText)
        {
            DateTime? dateTime = null;
            DateTime date;

            if (!string.IsNullOrEmpty(dateText) && DateTime.TryParseExact(dateText, Constants.DateTimeFormat, CultureInfo.CurrentUICulture, DateTimeStyles.None, out date))
            {
                dateTime = date;
            }

            return dateTime;
        }

        public static DateTime? StringToDate(this string dateText)
        {
            DateTime? dateTime = null;
            DateTime date;

            if (!string.IsNullOrEmpty(dateText) && DateTime.TryParseExact(dateText, Constants.DateFormat, CultureInfo.CurrentUICulture, DateTimeStyles.None, out date))
            {
                dateTime = date;
            }

            return dateTime;
        }

        public static decimal? StringToDecimal(this string decimalText)
        {
            decimal? decimalValue = null;
            decimal value;

            if (!string.IsNullOrEmpty(decimalText) && Decimal.TryParse(decimalText, NumberStyles.AllowDecimalPoint | NumberStyles.AllowThousands, CultureInfo.CurrentUICulture, out value))
            {
                decimalValue = value;
            }

            return decimalValue;
        }

        public static int StringToInteger(this string integerText)
        {
            int intValue;

            if (string.IsNullOrEmpty(integerText) || !int.TryParse(integerText, out intValue))
            {
                intValue = 0;
            }

            return intValue;
        }

        /// <summary>
        /// This method checks if the originalValue is null or empty(after trim the leading and trailing whitespaces) if it is the returns 
        /// the newValue else return the orignalValue.
        /// </summary>
        /// <param name="originalValue"></param>
        /// <param name="newValue"></param>
        /// <returns>string</returns>
        public static string IsNull(this string oldValue, string newValue)
        {
            if (oldValue == null || oldValue.Trim().Length == 0)
            {
                return newValue;
            }

            return oldValue;
        }
    }
}
