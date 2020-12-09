using System;
using System.Runtime.CompilerServices;

namespace Solid.DataTypes
{
    /// <summary>
    /// Similar to <see cref="Factor"/>, but based on decimal values
    /// </summary>
    /// <remarks>
    /// This class is useful because the following doesn't work in C# (or many other languages)
    /// 
    /// decimal ratio = 1m / 3m;
    /// decimal actual = 3m * ratio;
    /// 
    /// Assert(actual == 1m); // This fails because actual = 0.9999999999999999999999999999m
    /// 
    /// whereas:
    /// 
    /// var ratio = new Ratio(1m, 3m);
    /// var actual = ratio.ApplyTo(3m);
    /// Assert(actual == 1m); // this is OK!
    /// 
    /// </remarks>
    public readonly struct Ratio
    {
        public static readonly Ratio Zero = new Ratio(0);

        private readonly decimal _numerator;
        private readonly decimal _denominator;

        #region Constructors

        public Ratio(decimal numerator) : this(numerator, 1)
        {
        }

        public Ratio(decimal numerator, decimal denominator)
        {
            if (denominator == 0)
            {
                throw new ArgumentException("The denominator cannot be zero");
            }
            _numerator = numerator;
            _denominator = denominator;
        }

        #endregion

        [System.Diagnostics.Contracts.Pure]
        public Ratio Invert()
        {
            if (this._denominator == 0m)
            {
                return new Ratio(0, 1);
            }
            return new Ratio(this._denominator, this._numerator);
        }

        [System.Diagnostics.Contracts.Pure]
        public decimal ApplyTo(decimal value)
        {
            if (this._denominator == 0m)
            {
                return 0m;
            }
            return (value * this._numerator) / this._denominator;
        }

        /// <summary>
        /// Attempts to create a proper fraction from a decimal value.
        /// </summary>
        /// <remarks>
        /// this method tries to create fractions from decimal values.  For Example:
        /// 1/3 from 0.33333333333
        /// 1/28 from 0.0357142857142857142857142857
        ///
        /// ref: https://social.msdn.microsoft.com/Forums/vstudio/en-US/4ef5772b-dcf3-489e-a498-c835672e7c5e/convert-repeating-decimal-to-fraction?forum=csharpgeneral
        /// </remarks>
        /// <param name="value">A decimal value</param>
        /// <returns>A ratio, reduced to a regular fraction if possible</returns>
        public static Ratio From(decimal value)
        {
            // this is an arbitrary tolerance value.
            // it specifies how many decimal places are considered in order to determine the fraction
            // so for example:
            //     0.3333333  is calculated to be 0.3333333
            //     0.33333333 is calculated to be 1/3
            const decimal tolerance = 0.0000001M;

            for (int i = 1; i < 100; i++)
            {
                decimal multipliedValue = value * i;
                var numerator = Math.Round(multipliedValue);
                var absDiff = Math.Abs(numerator - multipliedValue);

                if (absDiff <= tolerance)
                {
                    return new Ratio(numerator, i);
                }
            }
            return new Ratio(value, 1);
        }

        #region Operators
        [System.Diagnostics.Contracts.Pure]
        public static decimal operator *(decimal value, Ratio ratio)
        {
            return ratio.ApplyTo(value);
        }

        [System.Diagnostics.Contracts.Pure]
        public static decimal operator *(Ratio ratio, decimal value)
        {
            return ratio.ApplyTo(value);
        }

        [System.Diagnostics.Contracts.Pure]
        public static explicit operator decimal(Ratio value)
        {
            if (value._denominator == 0m)
            {
                return 0m;
            }
            return value._numerator / value._denominator;
        }

        [System.Diagnostics.Contracts.Pure]
        public static explicit operator decimal?(Ratio? value)
        {
            if (value == null) { return null; }

            return (decimal)value;
        }

        [System.Diagnostics.Contracts.Pure]
        public static explicit operator decimal?(Ratio value)
        {
            return (decimal)value;
        }

        #endregion

        [System.Diagnostics.Contracts.Pure]
        public override string ToString()
        {
            if (this._denominator == 0)
            {
                return "0.00";
            }
            if (this._denominator == 1)
            {
                return this._numerator.ToString("N");
            }
            return $"{this._numerator / this._denominator:N}";
        }

    }
}
