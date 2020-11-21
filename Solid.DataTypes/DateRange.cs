using System;
using System.Security.Cryptography;

namespace Solid.DataTypes
{
    /// <summary>
    /// Defines a date range
    /// </summary>
    /// <remarks>
    /// This class should be immutable
    /// </remarks>
    public class DateRange : IEquatable<DateRange>
    {
        #region Constructors

        /// <summary>
        /// Creates a new instance of <see cref="DateRange"/>
        /// </summary>
        public DateRange()
        {
            // setup forever!
            this.From = DateTime.MinValue;
            this.To = DateTime.MaxValue;
            this._inclusion = RangeInclusion.Inclusive;
        }

        /// <summary>
        /// Creates a new instance of <see cref="DateRange"/>
        /// </summary>
        public DateRange(DateTime from, DateTime to, RangeInclusion inclusion)
        {
            if (from > to)
            {
                const string message = "The from date must be before the to date";
                throw new ArgumentException(message, nameof(to));
            }
            From = from.Date;
            To = to.Date;
            this._inclusion = inclusion;
        }

        #endregion

        #region Properties

        /// <summary>
        /// The inclusive definition of the range
        /// </summary>
        private readonly RangeInclusion _inclusion;

        /// <summary>
        /// The start date of the range.
        /// </summary>
        public DateTime From { get; }

        /// <summary>
        /// The end date of the range.
        /// </summary>
        public DateTime To { get; }

        public bool IncludesStart => _inclusion.HasFlag(RangeInclusion.IncludeStart);
        public bool IncludesEnd => _inclusion.HasFlag(RangeInclusion.IncludeEnd);
        public bool IsInclusive => _inclusion.HasFlag(RangeInclusion.Inclusive);

        #endregion

        #region Builder methods

        public DateRange WithEffectiveFrom(DateTime from)
        {
            return new DateRange(from, this.To, this._inclusion);
        }

        public DateRange WithEffectiveTo(DateTime to)
        {
            return new DateRange(this.From, to, this._inclusion);
        }

        public DateRange Inclusive()
        {
            return new DateRange(this.From, this.To, RangeInclusion.Inclusive);
        }

        public DateRange Exclusive()
        {
            return new DateRange(this.From, this.To, RangeInclusion.Exclusive);
        }

        # endregion

        public int ElapsedDays()
        {
            if ((this._inclusion == RangeInclusion.Exclusive) && (this.From.Date == this.To.Date))
            {
                return 0;
            }
            var effectiveStartDate = this._inclusion.HasFlag(RangeInclusion.IncludeStart)
                ? this.From.Date
                : this.From.Date.AddDays(1);
            var effectiveEndDate = this._inclusion.HasFlag(RangeInclusion.IncludeEnd)
                ? this.To.Date.AddDays(1)
                : this.To.Date;

            return (effectiveEndDate - effectiveStartDate).Days;
        }

        #region Equality

        public bool Equals(DateRange other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return _inclusion == other._inclusion && From.Equals(other.From) && To.Equals(other.To);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((DateRange)obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = (int)_inclusion;
                hashCode = (hashCode * 397) ^ From.GetHashCode();
                hashCode = (hashCode * 397) ^ To.GetHashCode();
                return hashCode;
            }
        }

        public static bool operator ==(DateRange left, DateRange right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(DateRange left, DateRange right)
        {
            return !Equals(left, right);
        }

        #endregion

        public bool IsInRange(DateTime value)
        {
            if (value < this.From) return false;

            if (!this._inclusion.HasFlag(RangeInclusion.IncludeStart) && value == this.From)
            {
                return false;
            }
            if (value > this.To)
            {
                return false;
            }
            if (!this._inclusion.HasFlag(RangeInclusion.IncludeEnd) && (value == this.To))
            {
                return false;
            }

            return true;
        }

        public override string ToString()
        {
            return $"{From:dd MMM yyyy} to {To:dd MMM yyyy} ({_inclusion})";
        }
    }
}
