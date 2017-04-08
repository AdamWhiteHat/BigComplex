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
		private const Double LOG_10_INV = 0.43429448190325;

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

		public Double Magnitude
		{
			get
			{
				return Complex.Abs(new Complex((double)this.Real, (double)this.Imaginary));
			}
		}

		public Double Phase
		{
			get
			{
				return Math.Atan2((double)m_imaginary, (double)m_real);
			}
		}

		#endregion

		#region Attributes

		public static readonly BigComplex Zero = new BigComplex(0, 0);
		public static readonly BigComplex One = new BigComplex(1, 0);
		public static readonly BigComplex ImaginaryOne = new BigComplex(0, 1);

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

		public BigComplex(BigInteger real, BigInteger imaginary)  /* Constructor to create a BigComplex number with rectangular co-ordinates  */
		{
			this.m_real = real;
			this.m_imaginary = imaginary;
		}

		public static BigComplex FromPolarCoordinates(Double magnitude, Double phase) /* Factory method to take polar inputs and create a BigComplex object */
		{
			Complex result = Complex.FromPolarCoordinates(magnitude, phase);
			return new BigComplex(result);
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
			// Division : Smith's formula.
			Double a = (double)left.m_real;
			Double b = (double)left.m_imaginary;
			Double c = (double)right.m_real;
			Double d = (double)right.m_imaginary;

			if (Math.Abs(d) < Math.Abs(c))
			{
				double doc = (d / c);
				return new BigComplex(new Complex((a + b * doc) / (c + d * doc), (b - a * doc) / (c + d * doc)));
			}
			else
			{
				double cod = (c / d);
				return new BigComplex(new Complex((b + a * cod) / (d + c * cod), (-a + b * cod) / (d + c * cod)));
			}
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

			if (c > d)
			{
				BigInteger r = d / c;
				return c * (BigInteger.One + r * r).SquareRoot();
			}
			else if (d.IsZero)
			{
				return c;  // c is either 0.0 or NaN
			}
			else
			{
				BigInteger r = c / d;
				return (d * (BigInteger.One + r * r).SquareRoot());
			}
		}
		public static BigComplex Conjugate(BigComplex value)
		{
			// Conjugate of a BigComplex number: the conjugate of x+i*y is x-i*y
			return (new BigComplex(value.m_real, (-value.m_imaginary)));

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
		
		#region Formattig/Parsing options 

		public override String ToString()
		{
			return (String.Format(CultureInfo.CurrentCulture, "({0}, {1})", this.m_real, this.m_imaginary));
		}

		public String ToString(String format)
		{
			return (String.Format(CultureInfo.CurrentCulture, "({0}, {1})", this.m_real.ToString(format, CultureInfo.CurrentCulture), this.m_imaginary.ToString(format, CultureInfo.CurrentCulture)));
		}

		public String ToString(IFormatProvider provider)
		{
			return (String.Format(provider, "({0}, {1})", this.m_real, this.m_imaginary));
		}

		public String ToString(String format, IFormatProvider provider)
		{
			return (String.Format(provider, "({0}, {1})", this.m_real.ToString(format, provider), this.m_imaginary.ToString(format, provider)));
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
		/*
		public static BigComplex Sin(BigComplex value) 
		{
		   double a = value.m_real;
		   double b = value.m_imaginary;
		   return new BigComplex(Math.Sin(a) * Math.Cosh(b), Math.Cos(a) * Math.Sinh(b));
		}

		public static BigComplex Sinh(BigComplex value) // Hyperbolic sin 
		{
		   double a = value.m_real;
		   double b = value.m_imaginary;
		   return new BigComplex(Math.Sinh(a) * Math.Cos(b), Math.Cosh(a) * Math.Sin(b));

		}
		public static BigComplex Asin(BigComplex value) // Arcsin 
		{
		   return (-ImaginaryOne) * Log(ImaginaryOne * value + Sqrt(One - value * value));
		}

		public static BigComplex Cos(BigComplex value) 
		{
		   double a = value.m_real;
		   double b = value.m_imaginary;
		   return new BigComplex(Math.Cos(a) * Math.Cosh(b), - (Math.Sin(a) * Math.Sinh(b)));
		}

		public static BigComplex Cosh(BigComplex value) // Hyperbolic cos 
		{
		   double a = value.m_real;
		   double b = value.m_imaginary;
		   return new BigComplex(Math.Cosh(a) * Math.Cos(b), Math.Sinh(a) * Math.Sin(b));
		}

		public static BigComplex Acos(BigComplex value) // Arccos 
		{
		   return (-ImaginaryOne) * Log(value + ImaginaryOne*Sqrt(One - (value * value)));

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
		   BigComplex Two = new BigComplex(2, 0);
		   return (ImaginaryOne / Two) * (Log(One - ImaginaryOne * value) - Log(One + ImaginaryOne * value));
		}
		*/
		#endregion

		#region Other numerical functions         
		/*
		public static BigComplex Log(double value) // Log of the BigComplex number value to the base of 'e' 
		{
		   return (new BigComplex((Math.Log(Math.Abs(value))), (Math.Atan2(value.m_imaginary, value.m_real))));

		}
		*/
		public static BigComplex Log(Double value, Double baseValue) // Log of the BigComplex number to a the base of a double
		{
			return (Math.Log(value) / Math.Log(baseValue));
		}

		public static BigComplex Exp(BigComplex value) // The BigComplex number raised to e 
		{
			Double temp_factor = Math.Exp((double)value.m_real);
			Double result_re = temp_factor * Math.Cos((double)value.m_imaginary);
			Double result_im = temp_factor * Math.Sin((double)value.m_imaginary);
			return (new BigComplex((BigInteger)result_re, (BigInteger)result_im));
		}

		public static BigComplex Sqrt(BigComplex value) // Square root ot the BigComplex number 
		{
			return BigComplex.FromPolarCoordinates(Math.Sqrt(value.Magnitude), value.Phase / 2.0);
		}

		/*
		public static BigComplex Log10(Double value) // Log to the base of 10 of the BigComplex number 
		{
 
			BigComplex temp_log = Math.Log(value);
			return (Scale(temp_log, (Double)LOG_10_INV));
 
		}

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
 
			double a = value.m_real;
			double b = value.m_imaginary;
			double c = power.m_real;
			double d = power.m_imaginary;
 
			double rho = BigComplex.Abs(value);
			double theta = Math.Atan2(b, a);
			double newRho = c * theta + d * Math.Log(rho);
 
			double t = Math.Pow(rho, c) * Math.Pow(Math.E, -d * theta);
 
			return new BigComplex(t * Math.Cos(newRho), t * Math.Sin(newRho));
		}
 
		public static BigComplex Pow(BigComplex value, Double power) // A BigComplex number raised to a real number 
		{
			return Pow(value, new BigComplex(power, 0));
		}
		*/
		#endregion

		#region Private member functions for internal use
		/*
		private static BigComplex Scale(BigComplex value, Double factor)
		{ 
			Double result_re = factor * value.m_real;
			Double result_im = factor * value.m_imaginary;
			return (new BigComplex(result_re, result_im));
		}
		*/
		#endregion
	}
}
