using System;
using System.Diagnostics;
using System.Linq;

namespace WpfApp1
{
    public static class DateTimeExtension
    {
        public static string ToTimeStamp(this DateTime dateTime)
        {
            var tzInfo = TimeZoneInfo.Local;
            var utcDiff = tzInfo.DisplayName.Split()[0];

            return (dateTime.ToString("HH:mm:ss.fff")) + utcDiff;
        }
    }
}
