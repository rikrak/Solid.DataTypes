using System;
using Solid.DataTypes.Infrastructure;

namespace Solid.DataTypes
{
    /// <summary>
    /// Defines a date range
    /// </summary>
    /// <remarks>
    /// This class should be immutable.
    /// Originally named Range, but changed to Extent to avoid conflict with System.Range
    /// </remarks>
    public class Extent<T> : IEquatable<Extent<T>> where T : IComparable<T>
    {
        #region Constructors


        /// <summary>
        /// Creates a new instance of <see cref="DateRange"/>
        /// </summary>
        public Extent(T from, T to, RangeInclusion inclusion)
        {
            if (from.IsNull()) { throw new ArgumentNullException(nameof(from), "from cannot be null");}
            if (to.IsNull()) { throw new ArgumentNullException(nameof(to), "to cannot be null");}
            if (from.CompareTo(to) > 0)
            {
                const string message = "The from must be before the to";
                throw new ArgumentException(message, nameof(to));
            }
            From = from;
            To = to;
            this._inclusion = inclusion;
        }

        #endregion

        #region Properties

        /// <summary>
        /// The inclusive definition of the range
        /// </summary>
        private readonly RangeInclusion _inclusion;

        /// <summary>
        /// The start of the range.
        /// </summary>
        public T From { get; }

        /// <summary>
        /// The end date of the range.
        /// </summary>
        public T To { get; }

        public bool IncludesStart => _inclusion.HasFlag(RangeInclusion.IncludeStart);
        public bool IncludesEnd => _inclusion.HasFlag(RangeInclusion.IncludeEnd);
        public bool IsInclusive => _inclusion.HasFlag(RangeInclusion.Inclusive);

        #endregion

        #region Builder methods

        public Extent<T> WithEffectiveFrom(T from)
        {
            return new Extent<T>(from, this.To, this._inclusion);
        }

        public Extent<T> WithEffectiveTo(T to)
        {
            return new Extent<T>(this.From, to, this._inclusion);
        }

        public Extent<T> Inclusive()
        {
            return new Extent<T>(this.From, this.To, RangeInclusion.Inclusive);
        }

        public Extent<T> Exclusive()
        {
            return new Extent<T>(this.From, this.To, RangeInclusion.Exclusive);
        }

        # endregion

        #region Equality

        public bool Equals(Extent<T> other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return _inclusion == other._inclusion && From.CompareTo(other.From) == 0 && To.CompareTo(other.To) == 0;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((Extent<T>)obj);
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

        public static bool operator ==(Extent<T> left, Extent<T> right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(Extent<T> left, Extent<T> right)
        {
            return !Equals(left, right);
        }

        #endregion

        public bool IsInRange(T value)
        {
            if (value.CompareTo(this.From) < 0) return false;

            if (!this._inclusion.HasFlag(RangeInclusion.IncludeStart) && value.CompareTo(this.From) == 0)
            {
                return false;
            }
            if (value.CompareTo(this.To) > 0)
            {
                return false;
            }
            if (!this._inclusion.HasFlag(RangeInclusion.IncludeEnd) && (value.CompareTo(this.To) == 0))
            {
                return false;
            }

            return true;
        }

        public override string ToString()
        {
            return $"{From} to {To} ({_inclusion})";
        }
    }
}