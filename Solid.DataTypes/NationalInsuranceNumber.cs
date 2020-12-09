using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace Solid.DataTypes
{
    /// <summary>
    /// Represents a UK National Insurance number
    /// </summary>
    public readonly struct NationalInsuranceNumber : IEquatable<NationalInsuranceNumber>, IFormattable
    {
        public static readonly NationalInsuranceNumber Empty = new NationalInsuranceNumber();

        private static readonly string[] InvalidPrefixes;
        private const string NiPattern = @"^(?!BGbg)(?!GBgb)(?!NKnk)(?!KNkn)(?!TNtn)(?!NTnt)(?!ZZzz)(?:[A-CEGHJ-PR-TW-Za-ceghj-pr-tw-z][A-CEGHJ-NPR-TW-Za-ceghj-npr-tw-z])(?:\s*\d\s*){6}([A-Da-d]|\s)$";
        private static readonly Regex ValidationRegex;

        private readonly string _value;

        #region Constructor

        static NationalInsuranceNumber()
        {
            var defaultTimeout = TimeSpan.FromMilliseconds(500);
            var regexOptions = RegexOptions.Compiled;
            ValidationRegex = new Regex(NiPattern, regexOptions, defaultTimeout);

            // special cases
            // these prefixes are not to be used: BG, GB, KN, NK, NT, TN and ZZ
            InvalidPrefixes = new[] { "BG", "GB", "KN", "NK", "NT", "TN", "ZZ" };
        }

        public NationalInsuranceNumber(string niNumber)
        {
            if (string.IsNullOrWhiteSpace(niNumber))
            {
                throw new ArgumentException("Value cannot be null or whitespace.", nameof(niNumber));
            }

            if (!IsValid(niNumber))
            {
                throw new ArgumentException($"\"{niNumber}\" is not a valid NI format", nameof(niNumber));
            }
            _value = niNumber.Replace(" ", "").ToUpper();
        }

        #endregion

        #region Conversion

        public static bool TryParse(string candidate, out NationalInsuranceNumber result)
        {
            if (!IsValid(candidate))
            {
                result = NationalInsuranceNumber.Empty;
                return false;
            }
            result = new NationalInsuranceNumber(candidate);
            return true;
        }

        public static explicit operator string(NationalInsuranceNumber value)
        {
            return value.ToString();
        }


        #endregion

        #region Equality

        public bool Equals(NationalInsuranceNumber other)
        {
            return string.Equals(_value, other._value, StringComparison.InvariantCultureIgnoreCase);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            return obj is NationalInsuranceNumber other && Equals(other);
        }

        public override int GetHashCode()
        {
            return StringComparer.InvariantCultureIgnoreCase.GetHashCode(_value);
        }

        public static bool operator ==(NationalInsuranceNumber left, NationalInsuranceNumber right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(NationalInsuranceNumber left, NationalInsuranceNumber right)
        {
            return !Equals(left, right);
        }

        #endregion

        #region ToString
        public string ToString(string format, IFormatProvider formatProvider)
        {
            if (string.IsNullOrWhiteSpace(this._value))
            {
                return string.Empty;
            }

            if (string.IsNullOrWhiteSpace(format))
            {
                format = "C";
            }

            if (format.Equals("C", StringComparison.InvariantCultureIgnoreCase))
            {
                return _value;
            }
            if (format.Equals("E", StringComparison.InvariantCultureIgnoreCase))
            {
                var prefix = _value.Substring(0, 2);
                var num1 = _value.Substring(2, 2);
                var num2 = _value.Substring(4, 2);
                var num3 = _value.Substring(6, 2);
                var suffix = _value.Substring(8, 1);
                return $"{prefix} {num1} {num2} {num3} {suffix}";
            }
            throw new FormatException($"The {format} format string is not supported.");
        }

        public override string ToString()
        {
            return this.ToString("C", null);
        }
        #endregion

        /// <summary>
        /// Determines the validity of a National Insurance number
        /// </summary>
        /// <remarks>
        /// see: https://www.gov.uk/hmrc-internal-manuals/national-insurance-manual/nim39110
        /// Note this method only verifies the format of the number, it does not verify that
        /// the number has been issued by the government.
        /// </remarks>
        /// <param name="value">The candidate value</param>
        /// <returns>
        ///     true - the <paramref name="value"/> is a valid National Insurance number
        ///     false - the <paramref name="value"/> is not a valid National Insurance number
        /// </returns>
        public static bool IsValid(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                return false;
            }

            // remove common whitespace characters
            value = value.Replace(" ", "")
                .Replace("\t", "")
                .Replace("\n", "")
                .Replace("\r", "");
            if (value.Length != 9)
            {
                // quick and simple check
                return false;
            }

            var m = ValidationRegex.Match(value);

            // We are looking for an exact match, not just a search hit. This matches what
            // the RegularExpressionValidator control does
            var isValid = (m.Success && m.Index == 0 && m.Length == value.Length);

            if (!isValid) return false;

            var prefix = value.Substring(0, 2);
            isValid = !InvalidPrefixes.Any(p => string.Equals(p, prefix, StringComparison.InvariantCultureIgnoreCase));

            return isValid;
        }
    }
}
