using System;
using System.Diagnostics.Contracts;

namespace Solid.DataTypes
{
    public readonly struct Factor : IEquatable<Factor>
    {
        public static readonly Factor Zero = new Factor(0);

        private readonly int _numerator;
        private readonly int? _denominator;

        #region Constructors

        public Factor(int factor) : this(factor, 1) { }

        public Factor(int numerator, int denominator)
        {
            if (denominator == 0)
            {
                throw new ArgumentException("A zero value would result in a divide by zero exception", nameof(denominator));
            }

            _numerator = numerator;
            _denominator = denominator;
        }


        #endregion

        private int Numerator => _numerator;
        private int Denominator => _denominator ?? 1;

        [Pure]
        public decimal ApplyTo(decimal value)
        {
            return (value * this._numerator) / this.Denominator;
        }

        /// <summary>
        /// Reduces the factor to its simplest form
        /// </summary>
        /// <remarks>Sourced from: https://www.codeproject.com/Articles/9078/Fraction-class-in-C </remarks>
        /// <returns></returns>
        [Pure]
        public Factor Reduce()
        {
            try
            {
                if (this.Numerator == 0)
                {
                    return new Factor(0, 1);
                }

                var gcd = GreatestCommonDenominator(this.Numerator, this.Denominator);
                if (gcd == 1)
                {
                    return this;
                }
                var numerator = this.Numerator / gcd;
                var denominator = this.Denominator / gcd;

                if (denominator < 0)   // if -ve sign in denominator
                {
                    //pass -ve sign to numerator
                    numerator *= -1;
                    denominator *= -1;
                }
                return new Factor(numerator, denominator);
            } // end try
            catch (Exception exp)
            {
                throw new Exception("Cannot reduce Fraction: " + exp.Message, exp);
            }
        }

        /// <summary>
        /// The function returns GCD of two numbers (used for reducing a Fraction)
        /// </summary>
        private static int GreatestCommonDenominator(int lhs, int rhs)
        {
            // take absolute values
            if (lhs < 0) lhs = -lhs;
            if (rhs < 0) rhs = -rhs;

            do
            {
                if (lhs < rhs)
                {
                    var tmp = lhs;  // swap the two operands
                    lhs = rhs;
                    rhs = tmp;
                }
                lhs = lhs % rhs;
            } while (lhs != 0);
            return rhs;
        }

        #region Equality

        public bool Equals(Factor other)
        {
            return (this.Numerator * other.Denominator) == (other.Numerator * this.Denominator);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((Factor)obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var reduced = this.Reduce();
                return (reduced.Numerator * 397) ^ reduced.Denominator;
            }
        }

        public static bool operator ==(Factor left, Factor right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(Factor left, Factor right)
        {
            return !Equals(left, right);
        }

        #endregion

        public override string ToString()
        {
            if (this.Denominator == 1)
            {
                return this.Numerator.ToString();
            }
            return $"{this.Numerator}/{this.Denominator}";
        }
    }
}