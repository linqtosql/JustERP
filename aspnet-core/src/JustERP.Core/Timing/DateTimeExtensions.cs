using System;

namespace JustERP.Timing
{
    public static class DateTimeExtensions
    {
        public static long GetTime(this DateTime value)
        {
            return (long)value.Subtract(new DateTime(1970, 1, 1)).TotalMilliseconds;
        }

        public static long? GetTime(this DateTime? value)
        {
            return value?.GetTime();
        }
    }
}
