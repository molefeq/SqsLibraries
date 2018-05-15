using System;

namespace SqsLibraries.Common.Extensions
{
    public static class ObjectExtensions
    {
        public static string ToString(this object objectValue)
        {
            if (objectValue == null || string.IsNullOrEmpty(objectValue.ToString()))
            {
                return string.Empty;
            }

            return objectValue.ToString();
        }

        public static int ToInteger(this object objectValue)
        {
            if (objectValue == null || string.IsNullOrEmpty(objectValue.ToString()))
            {
                return 0;
            }

            return Convert.ToInt32(objectValue.ToString());
        }

        public static int? ToNullableInteger(this object objectValue)
        {
            if (objectValue == null || string.IsNullOrEmpty(objectValue.ToString()))
            {
                return default(int?);
            }

            return Convert.ToInt32(objectValue.ToString());
        }


        public static long ToLong(this object objectValue)
        {
            if (objectValue == null || string.IsNullOrEmpty(objectValue.ToString()))
            {
                return 0;
            }

            return Convert.ToInt64(objectValue.ToString());
        }

        public static long? ToNullableLong(this object objectValue)
        {
            if (objectValue == null || string.IsNullOrEmpty(objectValue.ToString()))
            {
                return default(long?);
            }

            return Convert.ToInt64(objectValue.ToString());
        }

        public static Guid ToGuid(this object objectValue)
        {
            if (objectValue == null || string.IsNullOrEmpty(objectValue.ToString()))
            {
                return Guid.Empty;
            }

            return new Guid(objectValue.ToString());
        }

        public static Guid? ToNullableGuid(this object objectValue)
        {
            Guid returnValue;

            if (objectValue == null || !Guid.TryParse(objectValue.ToString(), out returnValue))
            {
                return default(Guid?);
            }

            return returnValue;
        }

        public static decimal ToDecimal(this object objectValue)
        {
            if (objectValue == null || string.IsNullOrEmpty(objectValue.ToString()))
            {
                return 0;
            }

            return Convert.ToDecimal(objectValue.ToString());
        }

        public static decimal? ToNullableDecimal(this object objectValue)
        {
            if (objectValue == null || string.IsNullOrEmpty(objectValue.ToString()))
            {
                return default(decimal?);
            }

            return Convert.ToDecimal(objectValue.ToString());
        }

        public static bool ToBoolean(this object objectValue)
        {
            if (objectValue == null || string.IsNullOrEmpty(objectValue.ToString()))
            {
                return false;
            }

            return Convert.ToBoolean(objectValue.ToString());
        }

        public static bool? ToNullableBoolean(this object objectValue)
        {
            if (objectValue == null || string.IsNullOrEmpty(objectValue.ToString()))
            {
                return default(bool?);
            }

            return Convert.ToBoolean(objectValue.ToString());
        }

        public static DateTime ToDateTime(this object objectValue)
        {
            if (objectValue == null || string.IsNullOrEmpty(objectValue.ToString()))
            {
                return DateTime.MinValue;
            }

            return Convert.ToDateTime(objectValue.ToString());
        }

        public static DateTime? ToNullableDateTime(this object objectValue)
        {
            if (objectValue == null || string.IsNullOrEmpty(objectValue.ToString()))
            {
                return default(DateTime?);
            }

            return Convert.ToDateTime(objectValue.ToString());
        }
    }
}
