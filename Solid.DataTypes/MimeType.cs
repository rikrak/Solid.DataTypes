
namespace Solid.DataTypes
{
    using System;
    using System.Linq;

    /// <summary>
    /// represents a Multipurpose Internet Mail Extension (MIME) 
    /// </summary>
    /// <remarks>
    /// see: https://developer.mozilla.org/en-US/docs/Web/HTTP/Basics_of_HTTP/MIME_types
    /// </remarks>
    public readonly struct MimeType : IEquatable<MimeType>
    {
        public static readonly MimeType None = new MimeType();
        public static readonly MimeType Default = new MimeType("application/octet-stream");  // catch-all

        private readonly string _type;
        private readonly string _subType;
        private readonly string _parameters;

        public MimeType(string value)
        {
            if (string.IsNullOrWhiteSpace(value)) { throw new ArgumentException("Value cannot be null or whitespace.", nameof(value)); }

            var parts = value.Split('/');
            if (parts.Length != 2) throw new ArgumentException("A MIME type should have a single '/'", nameof(value));

            if (parts.Any(string.IsNullOrWhiteSpace))
            {
                throw new ArgumentException("A MIME type should be in the format \"type/subtype[;parameter=value]\"", nameof(value));
            }

            this._type = parts[0].Trim().ToLower();
            var subTypeAndParam = parts[1];
            parts = subTypeAndParam.Split(';');
            this._subType = parts[0].Trim().ToLower();
            if (string.IsNullOrWhiteSpace(this._subType))
            {
                throw new ArgumentException("A MIME type should be in the format \"type/subtype[;parameter=value]\"", nameof(value));
            }
            this._parameters = parts.Length > 1
                ? parts[1].Trim()
                : null;
        }

        #region Equality

        public bool Equals(MimeType other)
        {
            return string.Equals(_type, other._type, StringComparison.InvariantCultureIgnoreCase)
                   && string.Equals(_subType, other._subType, StringComparison.InvariantCultureIgnoreCase)
                   && string.Equals(_parameters, other._parameters, StringComparison.InvariantCultureIgnoreCase);
        }

        public override bool Equals(object obj)
        {
            return obj is MimeType other && Equals(other);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = (_type != null ? StringComparer.InvariantCultureIgnoreCase.GetHashCode(_type) : 0);
                hashCode = (hashCode * 397) ^ (_subType != null ? StringComparer.InvariantCultureIgnoreCase.GetHashCode(_subType) : 0);
                hashCode = (hashCode * 397) ^ (_parameters != null ? StringComparer.InvariantCultureIgnoreCase.GetHashCode(_parameters) : 0);
                return hashCode;
            }
        }

        public static bool operator ==(MimeType left, MimeType right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(MimeType left, MimeType right)
        {
            return !left.Equals(right);
        }

        #endregion

        public override string ToString()
        {
            if (string.IsNullOrWhiteSpace(this._parameters))
            {
                return $"{this._type}/{this._subType}";
            }

            return $"{this._type}/{this._subType};{this._parameters}";
        }

        public static bool TryParse(string candidate, out MimeType mimeType)
        {
            mimeType = None;
            if (string.IsNullOrWhiteSpace(candidate) || candidate.Count(c => c == '/') != 1)
            {
                return false;
            }

            var parts = candidate.Split('/');
            if (parts.Any(p => p.Trim().Length == 0))
            {
                return false;
            }

            if (parts[1].Trim()[0] == ';')
            {
                return false;
            }
            mimeType = new MimeType(candidate);
            return true;
        }
    }
}

