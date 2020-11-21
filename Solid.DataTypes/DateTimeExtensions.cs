using System;

namespace Solid.DataTypes
{
    public static class DateTimeExtensions
    {
        public static DateRange To (this DateTime from, DateTime to, RangeInclusion inclusion) => new DateRange(from, to, inclusion);
    }
}