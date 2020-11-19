using System;
using System.Globalization;

namespace Solid.DataTypes
{
    public readonly struct FileSize : IFormattable, IEquatable<FileSize>, IComparable<FileSize>, IComparable
    {
        private const int Factor = 1024;

        public long Bytes { get; }
        public decimal KiloBytes => this.Bytes / (decimal)Factor;
        public decimal MegaBytes => this.KiloBytes / Factor;
        public decimal GigaBytes => this.MegaBytes / Factor;

        public FileSize(long bytes)
        {
            Bytes = bytes;
        }

        public static readonly FileSize Zero = new FileSize();

        public static FileSize Of(FilePath file)
        {
            var info = file.GetInfo();
            return info.Exists ? new FileSize(info.Length) : new FileSize();
        }

        #region Conversion
        public static explicit operator FileSize(long bytes) => new FileSize(bytes);
        public static explicit operator long(FileSize value) => value.Bytes;
        #endregion

        #region Equality

        public bool Equals(FileSize other)
        {
            return Bytes == other.Bytes;
        }

        public override bool Equals(object obj)
        {
            return obj is FileSize other && Equals(other);
        }

        public override int GetHashCode()
        {
            return Bytes.GetHashCode();
        }

        public static bool operator ==(FileSize left, FileSize right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(FileSize left, FileSize right)
        {
            return !left.Equals(right);
        }

        #endregion

        #region Comparable

        public int CompareTo(FileSize other)
        {
            return Bytes.CompareTo(other.Bytes);
        }

        public int CompareTo(object obj)
        {
            if (ReferenceEquals(null, obj)) return 1;
            return obj is FileSize other ? CompareTo(other) : throw new ArgumentException($"Object must be of type {nameof(FileSize)}");
        }

        public static bool operator <(FileSize left, FileSize right)
        {
            return left.CompareTo(right) < 0;
        }

        public static bool operator >(FileSize left, FileSize right)
        {
            return left.CompareTo(right) > 0;
        }

        public static bool operator <=(FileSize left, FileSize right)
        {
            return left.CompareTo(right) <= 0;
        }

        public static bool operator >=(FileSize left, FileSize right)
        {
            return left.CompareTo(right) >= 0;
        }

        #endregion

        #region Formatting
        public override string ToString()
        {
            return this.ToString("b");
        }

        public string ToString(string format)
        {
            return this.ToString(format, CultureInfo.CurrentCulture);
        }

        public string ToString(string format, IFormatProvider formatProvider)
        {
            if (String.IsNullOrEmpty(format)) format = "b";
            if (formatProvider == null) formatProvider = CultureInfo.CurrentCulture;

            switch (format.ToLower())
            {
                case "f": return GetNiceFormat(formatProvider);
                case "k":
                case "kb":
                    return this.KiloBytes.ToString("n2", formatProvider);
                case "m":
                case "mb":
                    return this.MegaBytes.ToString("n2", formatProvider);
                case "g":
                case "gb": return this.GigaBytes.ToString("n2", formatProvider);
                default: return this.Bytes.ToString("d", formatProvider);
            }

        }

        private string GetNiceFormat(IFormatProvider formatProvider)
        {
            var log = Math.Log(Bytes, Factor);

            if (log < 1) return $"{this.ToString("b", formatProvider)} bytes";
            if (log < 2) return $"{this.ToString("kb", formatProvider)} Kb";
            if (log < 3) return $"{this.ToString("mb", formatProvider)} Mb";

            return $"{this.ToString("gb", formatProvider)} Gb";
        }
        #endregion


    }
}
