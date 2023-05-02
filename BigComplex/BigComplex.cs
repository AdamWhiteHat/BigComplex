using System;
using System.Linq;
using System.Text;
using System.Numerics;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Globalization;

namespace ExtendedNumerics
{
	public struct BigComplex : IEquatable<BigComplex>, IFormattable
	{
		#region Private Data members & Constants

		private BigInteger m_real;
		private BigInteger m_imaginary;
		/* private const Double LOG_10_INV = 0.43429448190325; */

		#endregion

		#region Public Properties

		public BigInteger Real
		{
			get
			{
				return m_real;
			}
		}

		public BigInteger Imaginary
		{
			get
			{
				return m_imaginary;
			}
		}

		public BigInteger Magnitude
		{
			get
			{
				return Abs(this);
			}
		}

		public BigInteger Phase
		{
			get
			{
				return Atan2_BigInteger(m_imaginary, m_real);
			}
		}

		public int Sign
		{
			get
			{
				return (Real == 0) ? Imaginary.Sign : Real.Sign;
			}
		}

		#endregion

		#region Static Values

		public static readonly BigComplex Zero = new BigComplex(BigInteger.Zero, BigInteger.Zero);
		public static readonly BigComplex One = new BigComplex(BigInteger.One, BigInteger.Zero);
		public static readonly BigComplex ImaginaryOne = new BigComplex(BigInteger.Zero, BigInteger.One);

		#endregion

		#region Constructors and factory methods

		public BigComplex(Complex complex)  /* Constructor to create a BigComplex number with rectangular co-ordinates  */
		{
			this.m_real = (BigInteger)complex.Real;
			this.m_imaginary = (BigInteger)complex.Imaginary;
		}

		public BigComplex(BigInteger real)  /* Constructor to create a BigComplex number with rectangular co-ordinates  */
			: this(real, 0)
		{
		}

		public BigComplex(double real, double imaginary)  /* Constructor to create a BigComplex number with rectangular co-ordinates  */
		{
			this.m_real = (BigInteger)real;
			this.m_imaginary = (BigInteger)imaginary;
		}

		public BigComplex(BigInteger real, BigInteger imaginary)  /* Constructor to create a BigComplex number with rectangular co-ordinates  */
		{
			this.m_real = real;
			this.m_imaginary = imaginary;
		}

		public static BigComplex FromPolarCoordinates(BigInteger magnitude, Double phase) /* Factory method to take polar inputs and create a BigComplex object */
		{
			Complex result = new Complex(((double)magnitude * Math.Cos(phase)), ((double)magnitude * Math.Sin(phase)));
			return new BigComplex(result);
		}

		public BigComplex Clone()
		{
			return new BigComplex(this.m_real, this.m_imaginary);
		}

		public static BigComplex Parse(string s)
		{
			if (string.IsNullOrWhiteSpace(s))
			{
				throw new ArgumentException($"Argument {nameof(s)} cannot be null, empty or whitespace");
			}

			string input = new string(s.Where(c => !char.IsWhiteSpace(c)).ToArray());
			input = input.Replace('᠆', '-')
						 .Replace('‐', '-')
						 .Replace('‒', '-')
						 .Replace('–', '-')
						 .Replace('—', '-')
						 .Replace('―', '-')
						 .Replace('⁻', '-')
						 .Replace('₋', '-')
						 .Replace('−', '-')
						 .Replace('﹣', '-')
						 .Replace('－', '-');
			input = input.Replace('➕', '+')
						 .Replace('ᐩ', '+')
						 .Replace('⁺', '+')
						 .Replace('₊', '+')
						 .Replace('˖', '+')
						 .Replace('﹢', '+')
						 .Replace('＋', '+');
			string[] parts = input.Split(new char[] { '(', ')', ',', '+', 'i' }, StringSplitOptions.RemoveEmptyEntries);

			if (parts.Length <= 0 || parts.Length > 2)
			{
				throw new FormatException($"Argument {nameof(s)} not of the correct format. Expecting format: \"(3, 5)\" or 3 + 5i");
			}

			BigInteger imaginary = 0;
			BigInteger real = BigInteger.Parse(parts[0]);

			if (parts.Length == 2)
			{
				imaginary = BigInteger.Parse(parts[1]);
			}

			return new BigComplex(real, imaginary);
		}


		#endregion

		#region Arithmetic Methods

		public static BigComplex Negate(BigComplex value)
		{
			return -value;
		}

		public static BigComplex Add(BigComplex left, BigComplex right)
		{
			return left + right;
		}

		public static BigComplex Subtract(BigComplex left, BigComplex right)
		{
			return left - right;
		}

		public static BigComplex Multiply(BigComplex left, BigComplex right)
		{
			return left * right;
		}

		public static BigComplex Divide(BigComplex dividend, BigComplex divisor)
		{
			return dividend / divisor;
		}

		#endregion

		#region Arithmetic Operator Overloading

		public static BigComplex operator -(BigComplex value)  /* Unary negation of a BigComplex number */
		{

			return (new BigComplex((-value.m_real), (-value.m_imaginary)));
		}

		public static BigComplex operator +(BigComplex left, BigComplex right)
		{
			return (new BigComplex((left.m_real + right.m_real), (left.m_imaginary + right.m_imaginary)));

		}

		public static BigComplex operator -(BigComplex left, BigComplex right)
		{
			return (new BigComplex((left.m_real - right.m_real), (left.m_imaginary - right.m_imaginary)));
		}

		public static BigComplex operator *(BigComplex left, BigComplex right)
		{
			// Multiplication:  (a + bi)(c + di) = (ac -bd) + (bc + ad)i
			BigInteger result_Realpart = (left.m_real * right.m_real) - (left.m_imaginary * right.m_imaginary);
			BigInteger result_Imaginarypart = (left.m_imaginary * right.m_real) + (left.m_real * right.m_imaginary);
			return (new BigComplex(result_Realpart, result_Imaginarypart));
		}

		public static BigComplex operator /(BigComplex left, BigComplex right)
		{
			BigComplex conjg = Conjugate(right);

			var n = left * conjg;
			var d = right * conjg;

			if (n.Imaginary != 0 && d.Imaginary == 0)
			{
				// a+bi/d = a/d + b/di
				return new BigComplex(BigInteger.Divide(n.Real, d.Real), BigInteger.Divide(n.Imaginary, d.Real));
			}
			// (n.Imaginary == 0 && d.Imaginary == 0)
			return new BigComplex(BigInteger.Divide(n.Real, d.Real), 0);
		}

		#endregion

		#region Other arithmetic operations 

		public static BigInteger Abs(BigComplex value)
		{
			//if(Double.IsInfinity(value.m_real) || Double.IsInfinity(value.m_imaginary))
			//{
			//    return double.PositiveInfinity;
			//}

			// |value| == sqrt(a^2 + b^2)
			// sqrt(a^2 + b^2) == a/a * sqrt(a^2 + b^2) = a * sqrt(a^2/a^2 + b^2/a^2)
			// Using the above we can factor out the square of the larger component to dodge overflow.

			BigInteger c = BigInteger.Abs(value.m_real);
			BigInteger d = BigInteger.Abs(value.m_imaginary);

			if (d.IsZero)
			{
				return c;  // c is either 0.0 or NaN
			}

			return ((c * c) + (d * d)).SquareRoot();
		}
		public static BigComplex Conjugate(BigComplex value)
		{
			// Conjugate of a BigComplex number: the conjugate of x+i*y is x-i*y
			return (new BigComplex(value.m_real, BigInteger.Negate(value.m_imaginary)));

		}
		public static BigComplex Reciprocal(BigComplex value)
		{
			// Reciprocal of a BigComplex number : the reciprocal of x+i*y is 1/(x+i*y)
			if ((value.m_real == 0) && (value.m_imaginary == 0))
			{
				return BigComplex.Zero;
			}

			return BigComplex.One / value;
		}

		public BigComplex Mod(BigComplex mod)
		{
			BigComplex negQuot = BigComplex.Negate(BigComplex.Divide(this, mod));
			return BigComplex.Add(this, BigComplex.Multiply(mod, negQuot));
		}

		#endregion

		#region Comparison Operators

		public static bool operator ==(BigComplex left, BigComplex right)
		{
			return ((left.m_real == right.m_real) && (left.m_imaginary == right.m_imaginary));


		}
		public static bool operator !=(BigComplex left, BigComplex right)
		{
			return ((left.m_real != right.m_real) || (left.m_imaginary != right.m_imaginary));

		}

		#endregion

		#region Equality Methods

		public override bool Equals(object obj)
		{
			if (!(obj is BigComplex)) return false;
			return this == ((BigComplex)obj);
		}
		public bool Equals(BigComplex value)
		{
			return ((this.m_real.Equals(value.m_real)) && (this.m_imaginary.Equals(value.m_imaginary)));
		}

		#endregion

		#region Type-casting basic numeric data-types to ComplexNumber

		public static implicit operator BigComplex(Int16 value)
		{
			return (new BigComplex((BigInteger)value));
		}
		public static implicit operator BigComplex(Int32 value)
		{
			return (new BigComplex((BigInteger)value));
		}
		public static implicit operator BigComplex(Int64 value)
		{
			return (new BigComplex((BigInteger)value));
		}
		public static implicit operator BigComplex(UInt16 value)
		{
			return (new BigComplex((BigInteger)value));
		}
		public static implicit operator BigComplex(UInt32 value)
		{
			return (new BigComplex((BigInteger)value));
		}
		public static implicit operator BigComplex(UInt64 value)
		{
			return (new BigComplex((BigInteger)value));
		}
		public static implicit operator BigComplex(SByte value)
		{
			return (new BigComplex((BigInteger)value));
		}
		public static implicit operator BigComplex(Byte value)
		{
			return (new BigComplex((BigInteger)value));
		}
		public static implicit operator BigComplex(Single value)
		{
			return (new BigComplex((BigInteger)value));
		}
		public static implicit operator BigComplex(Double value)
		{
			return (new BigComplex((BigInteger)value));
		}
		public static explicit operator BigComplex(BigInteger value)
		{
			return (new BigComplex(value));
		}
		public static explicit operator BigComplex(Decimal value)
		{
			return (new BigComplex((BigInteger)value));
		}

		#endregion

		#region Formatting/Parsing options 

		public override String ToString()
		{
			if (this.m_imaginary == 0)
			{
				return this.m_real.ToString();
			}
			return String.Format(CultureInfo.CurrentCulture, "({0}, {1})", this.m_real, this.m_imaginary);
		}

		public String ToString(String format)
		{
			if (this.m_imaginary == 0)
			{
				return String.Format(CultureInfo.CurrentCulture, "{0}", this.m_real.ToString(format, CultureInfo.CurrentCulture));
			}
			return String.Format(CultureInfo.CurrentCulture, "({0}, {1})", this.m_real.ToString(format, CultureInfo.CurrentCulture), this.m_imaginary.ToString(format, CultureInfo.CurrentCulture));
		}

		public String ToString(IFormatProvider provider)
		{
			if (this.m_imaginary == 0)
			{
				return String.Format(provider, "{0}", this.m_real);
			}
			return String.Format(provider, "({0}, {1})", this.m_real, this.m_imaginary);
		}

		public String ToString(String format, IFormatProvider provider)
		{
			if (this.m_imaginary == 0)
			{
				return String.Format(provider, "{0}", this.m_real.ToString(format, provider));
			}
			return String.Format(provider, "({0}, {1})", this.m_real.ToString(format, provider), this.m_imaginary.ToString(format, provider));
		}

		public override Int32 GetHashCode()
		{
			Int32 n1 = 99999997;
			Int32 hash_real = this.m_real.GetHashCode() % n1;
			Int32 hash_imaginary = this.m_imaginary.GetHashCode();
			Int32 final_hashcode = hash_real ^ hash_imaginary;
			return (final_hashcode);
		}

		#endregion

		#region Trigonometric operations (methods implementing ITrigonometric) 

		public static BigComplex Sin(BigComplex value)
		{
			double a = (double)value.m_real;
			double b = (double)value.m_imaginary;
			return new BigComplex(Math.Sin(a) * Math.Cosh(b), Math.Cos(a) * Math.Sinh(b));
		}

		public static BigComplex Sinh(BigComplex value) // Hyperbolic sin 
		{
			double a = (double)value.m_real;
			double b = (double)value.m_imaginary;
			return new BigComplex(Math.Sinh(a) * Math.Cos(b), Math.Cosh(a) * Math.Sin(b));

		}

		public static BigComplex Asin(BigComplex value) // Arcsin 
		{
			return (-ImaginaryOne) * Log(ImaginaryOne * value + Sqrt(One - value * value));
		}

		public static BigComplex Cos(BigComplex value)
		{
			double a = (double)value.m_real;
			double b = (double)value.m_imaginary;

			return new BigComplex(Math.Cos(a) * Math.Cosh(b), -(Math.Sin(a) * Math.Sinh(b)));
		}

		public static BigComplex Cosh(BigComplex value) // Hyperbolic cos 
		{
			double a = (double)value.m_real;
			double b = (double)value.m_imaginary;
			return new BigComplex(Math.Cosh(a) * Math.Cos(b), Math.Sinh(a) * Math.Sin(b));
		}

		public static BigComplex Acos(BigComplex value) // Arccos 
		{
			return (-ImaginaryOne) * Log(value + ImaginaryOne * Sqrt(One - (value * value)));

		}

		public static BigComplex Tan(BigComplex value)
		{
			return (Sin(value) / Cos(value));
		}

		public static BigComplex Tanh(BigComplex value) // Hyperbolic tan 
		{
			return (Sinh(value) / Cosh(value));
		}

		public static BigComplex Atan(BigComplex value) // Arctan 
		{
			BigComplex Two = new BigComplex(new BigInteger(2), BigInteger.Zero);
			return (ImaginaryOne / Two) * (Log(One - ImaginaryOne * value) - Log(One + ImaginaryOne * value));
		}

		public static BigInteger Atan(BigInteger value) // Arctan for BigInteger
		{
			BigComplex val = new BigComplex(value);
			BigComplex Two = new BigComplex(new BigInteger(2), BigInteger.Zero);
			return BigComplex.Abs((ImaginaryOne / Two) * (Log(One - ImaginaryOne * val) - Log(One + ImaginaryOne * val)));
		}

		public static BigInteger Atan2_BigInteger(BigInteger y, BigInteger x) // 2-argument arctangent 
		{
			//sqrt(a ^ 2 + b ^ 2)

			return BigInteger.Multiply
			(
				new BigInteger(2),
				BigComplex.Atan
				(
					BigInteger.Divide
					(
						y,
						BigInteger.Add
						(
							(BigInteger.Add(BigInteger.Multiply(x, x), BigInteger.Multiply(y, y))).SquareRoot(),
							x
						)
					)
				)
			);
		}

		public static double Atan2(BigComplex y, BigComplex x) // 2-argument arctangent 
		{
			Complex a = new Complex((double)y.Real, (double)y.Imaginary);
			Complex b = new Complex((double)x.Real, (double)x.Imaginary);

			if (y.Sign > 0)
			{
				return Complex.Abs(Complex.Atan(Complex.Divide(b, a)));
			}
			else if (y.Sign < 0 && x.Sign >= 0)
			{
				Complex tmp = Complex.Atan(Complex.Divide(b, a));
				Complex tmp2 = new Complex(tmp.Real + Math.PI, tmp.Imaginary);
				return Complex.Abs(tmp2);
			}
			else if (y.Sign < 0 && x.Sign < 0)
			{

				Complex tmp = Complex.Atan(Complex.Divide(b, a));
				Complex tmp2 = new Complex(tmp.Real - Math.PI, tmp.Imaginary);
				return Complex.Abs(tmp2);
			}
			else if (y.Sign == 0 && x.Sign > 0)
			{
				return (Math.PI / 2);
			}
			else if (y.Sign == 0 && x.Sign < 0)
			{
				return -(Math.PI / 2);
			}
			else if (y.Sign == 0 && x.Sign == 0)
			{
				return double.NaN;
			}
			else
			{
				return double.NaN;
			}
		}

		private double DegreeToRadian(double angle)
		{
			return Math.PI * angle / 180.0;
		}

		private double RadianToDegree(double angle)
		{
			return angle * (180.0 / Math.PI);
		}

		#endregion

		#region Other numerical functions

		public static BigComplex Log(BigComplex value) // Log of the BigComplex number value to the base of 'e' 
		{
			return (new BigComplex(((BigInteger)BigInteger.Log(BigComplex.Abs(value))), ((BigInteger)(Math.Atan2((double)value.m_imaginary, (double)value.m_real)))));
		}

		public static BigComplex Log(BigInteger value, BigInteger baseValue) // Log of the BigComplex number to a the base of a double
		{
			return (BigInteger.Log(value) / BigInteger.Log(baseValue));
		}

		/*
		public static BigComplex Exp(BigComplex value) // The BigComplex number raised to e 
		{
			Double temp_factor = Math.Exp((double)value.m_real);
			Double result_re = temp_factor * Math.Cos((double)value.m_imaginary);
			Double result_im = temp_factor * Math.Sin((double)value.m_imaginary);
			return (new BigComplex((BigInteger)result_re, (BigInteger)result_im));
		}
		*/

		public static BigComplex Sqrt(BigComplex value) // Square root of the BigComplex number 
		{
			Complex sqrt = Complex.FromPolarCoordinates(Math.Sqrt((double)value.Magnitude), (double)(value.Phase / 2));
			return new BigComplex(sqrt.Real, sqrt.Imaginary);
		}

		/*
		public static BigComplex Log10(Double value) // Log to the base of 10 of the BigComplex number 
		{

			BigComplex temp_log = Math.Log(value);
			return (Scale(temp_log, LOG_10_INV));

		}
		*/

		public static BigComplex Pow(BigComplex value, BigComplex power) // A BigComplex number raised to another BigComplex number 
		{
			if (power == BigComplex.Zero)
			{
				return BigComplex.One;
			}

			if (value == BigComplex.Zero)
			{
				return BigComplex.Zero;
			}

			double a = (double)value.m_real;
			double b = (double)value.m_imaginary;
			double c = (double)power.m_real;
			double d = (double)power.m_imaginary;

			double rho = (double)BigComplex.Abs(value);
			double theta = Math.Atan2(b, a);
			double newRho = c * theta + d * Math.Log(rho);

			double t = Math.Pow(rho, c) * Math.Pow(Math.E, -d * theta);

			return new BigComplex((BigInteger)(t * Math.Cos(newRho)), (BigInteger)(t * Math.Sin(newRho)));
		}

		/*
		public static BigComplex Pow(BigComplex value, Double power) // A BigComplex number raised to a real number 
		{
			return Pow(value, new BigComplex((BigInteger)power, 0));
		}
		*/

		public static BigInteger Norm(BigComplex source)
		{
			return BigComplex.Abs(source);
		}

		public static BigComplex GCD(BigComplex a, BigComplex b)
		{
			BigInteger aNorm = BigComplex.Norm(a);
			BigInteger bNorm = BigComplex.Norm(b);

			if (bNorm > aNorm)
			{
				Swap(ref a, ref b);
			}

			BigComplex result = BigComplex.Zero;
			BigComplex q = BigComplex.Zero;
			BigComplex r = BigComplex.One;

			while (r != BigComplex.Zero)
			{
				q = BigComplex.Divide(a, b);
				r = BigComplex.Subtract(a, BigComplex.Multiply(q, b));

				a = b;
				result = b;
				b = r;
			}

			return result;
		}

		#endregion

		#region Private member functions for internal use

		private static BigComplex Scale(BigComplex value, BigInteger factor)
		{
			BigInteger result_re = factor * value.m_real;
			BigInteger result_im = factor * value.m_imaginary;
			return (new BigComplex((BigInteger)result_re, (BigInteger)result_im));
		}

		private static void Swap(ref BigComplex a, ref BigComplex b)
		{
			BigComplex c = a;
			a = b;
			b = c;
		}

		#endregion
	}
}
