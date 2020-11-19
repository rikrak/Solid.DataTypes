using System;

namespace Solid.DataTypes
{
    public readonly struct FileExtension : IEquatable<FileExtension>, IComparable<FileExtension>, IComparable
    {
        private readonly string _value;

        public FileExtension(string value)
        {
            if (string.IsNullOrWhiteSpace(value)) { throw new ArgumentException("Value cannot be null or whitespace.", nameof(value)); }

            value = value.Trim();
            if (value[0] != '.')
            {
                this._value = $".{value}";
            }
            else
            {
                _value = value;
            }
        }

        public static FileExtension None { get; } = new FileExtension(".");
        public static FileExtension For(string extension) => new FileExtension(extension);

        public static explicit operator string(FileExtension value)
        {
            return value._value;
        }

        public static explicit operator FileExtension(string value)
        {
            return new FileExtension(value);
        }

        #region Equality

        public bool Equals(FileExtension other)
        {
            return string.Equals(_value.TrimStart('.'), other._value.TrimStart('.'), StringComparison.OrdinalIgnoreCase);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((FileExtension)obj);
        }

        public override int GetHashCode()
        {
            return StringComparer.OrdinalIgnoreCase.GetHashCode(_value.TrimStart('.'));
        }

        public static bool operator ==(FileExtension left, FileExtension right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(FileExtension left, FileExtension right)
        {
            return !Equals(left, right);
        }

        #endregion

        public override string ToString()
        {
            return _value;
        }

        public int CompareTo(FileExtension other)
        {
            return string.Compare(_value, other._value, StringComparison.InvariantCultureIgnoreCase);
        }

        public int CompareTo(object obj)
        {
            if (ReferenceEquals(null, obj)) return 1;
            return obj is FileExtension other ? CompareTo(other) : throw new ArgumentException($"Object must be of type {nameof(FileExtension)}");
        }
    }
}