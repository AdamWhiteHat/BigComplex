using System;
using System.Linq;
using System.Text;
using System.Numerics;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Globalization;

namespace ExtendedNumerics
{
	public struct BigComplex : IEquatable<BigComplex>
	{
		#region Private Data members & Constants

		private BigDecimal m_real;
		private BigDecimal m_imaginary;

		#endregion

		#region Public Properties

		public BigDecimal Real
		{
			get
			{
				return m_real;
			}
		}

		public BigDecimal Imaginary
		{
			get
			{
				return m_imaginary;
			}
		}

		public BigDecimal Magnitude
		{
			get
			{
				return Abs(this);
			}
		}

		public BigDecimal Phase
		{
			get
			{
				return Atan2(m_imaginary, m_real);
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

		public static readonly BigComplex MinusOne = new BigComplex(BigDecimal.MinusOne, BigDecimal.Zero);
		public static readonly BigComplex Zero = new BigComplex(BigDecimal.Zero, BigDecimal.Zero);
		public static readonly BigComplex One = new BigComplex(BigDecimal.One, BigDecimal.Zero);
		public static readonly BigComplex ImaginaryOne = new BigComplex(BigDecimal.Zero, BigDecimal.One);

		public static int Precision
		{
			get { return BigDecimal.Precision; }
			set
			{
				BigDecimal.Precision = value;
			}
		}

		static BigComplex()
		{
			BigComplex.Precision = 20;
		}

		#endregion

		#region Constructors and factory methods

		public BigComplex(Complex complex)  /* Constructor to create a BigComplex number with rectangular co-ordinates  */
			: this(complex.Real, complex.Imaginary)
		{
		}

		public BigComplex(BigDecimal real)  /* Constructor to create a BigComplex number with rectangular co-ordinates  */
			: this(real, BigDecimal.Zero)
		{
		}

		public BigComplex(double real, double imaginary)  /* Constructor to create a BigComplex number with rectangular co-ordinates  */
			: this((BigDecimal)real, (BigDecimal)imaginary)
		{
		}

		public BigComplex(BigDecimal real, BigDecimal imaginary)  /* Constructor to create a BigComplex number with rectangular co-ordinates  */
		{
			this.m_real = real.Clone();
			this.m_imaginary = imaginary.Clone();
		}

		public static BigComplex FromPolarCoordinates(BigDecimal magnitude, BigDecimal phase) /* Factory method to take polar inputs and create a BigComplex object */
		{
			BigDecimal magnitudeCos = magnitude * BigDecimal.Cos(phase);
			BigDecimal magnitudeSin = magnitude * BigDecimal.Sin(phase);

			return new BigComplex(magnitudeCos, magnitudeSin);
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

			string input = s.Trim();// new string(s.Where(c => !char.IsWhiteSpace(c)).ToArray());
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
			string[] parts = input.Split(new char[] { '(', ')', ',', 'i', ' ', '+' }, StringSplitOptions.RemoveEmptyEntries);

			bool isNegative = false;
			if (parts.Length == 3 && parts[1] == "-")
			{
				parts = new string[] { parts[0], parts[2] };
				isNegative = true;
			}

			if (parts.Length <= 0 || parts.Length > 2)
			{
				throw new FormatException($"Argument {nameof(s)} not of the correct format. Expecting format: \"(3, 5)\"");
			}

			BigDecimal imaginary = 0;
			BigDecimal real = BigDecimal.Parse(parts[0]);

			if (parts.Length == 2)
			{
				imaginary = BigDecimal.Parse(parts[1]);
			}

			if (isNegative)
			{
				imaginary = BigDecimal.Negate(imaginary);
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
			BigDecimal result_Realpart = (left.m_real * right.m_real) - (left.m_imaginary * right.m_imaginary);
			BigDecimal result_Imaginarypart = (left.m_imaginary * right.m_real) + (left.m_real * right.m_imaginary);
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
				return new BigComplex(BigDecimal.Divide(n.Real, d.Real), BigDecimal.Divide(n.Imaginary, d.Real));
			}
			// (n.Imaginary == 0 && d.Imaginary == 0)
			return new BigComplex(BigDecimal.Divide(n.Real, d.Real), 0);
		}

		#endregion

		#region Other arithmetic operations 

		public BigComplex Round()
		{
			return BigComplex.Round(this);
		}

		public static BigComplex Round(BigComplex input)
		{
			return BigComplex.Round(input, Precision);
		}

		public static BigComplex Round(BigComplex input, int precision)
		{
			return new BigComplex(BigDecimal.Round(input.Real, precision), BigDecimal.Round(input.Imaginary, precision));
		}

		public static BigComplex Round(BigComplex input, int precision, RoundingStrategy roundingStrategy)
		{
			return new BigComplex(BigDecimal.Round(input.Real, precision, roundingStrategy), BigDecimal.Round(input.Imaginary, precision, roundingStrategy));
		}

		public static BigComplex ToIntegerParts(BigComplex input)
		{
			return BigComplex.Round(input, 0, RoundingStrategy.AwayFromZero);			
		}

		public static BigDecimal Abs(BigComplex value)
		{
			// |value| == sqrt(a^2 + b^2)
			// sqrt(a^2 + b^2) == a/a * sqrt(a^2 + b^2) = a * sqrt(a^2/a^2 + b^2/a^2)
			// Using the above we can factor out the square of the larger component to dodge overflow.

			BigDecimal c = BigDecimal.Abs(value.m_real);
			BigDecimal d = BigDecimal.Abs(value.m_imaginary);

			if (c.IsZero())
			{
				return BigDecimal.NthRoot((c * c) + (d * d), 2, Precision);
			}
			else if (c > d)
			{
				BigDecimal r = d / c;
				return c * BigDecimal.NthRoot(BigDecimal.One + r * r, 2, Precision);
			}
			else if (d.IsZero())
			{
				return c;  // c is either 0.0 or NaN
			}

			return BigDecimal.NthRoot((c * c) + (d * d), 2, Precision);
		}
		public static BigComplex Conjugate(BigComplex value)
		{
			// Conjugate of a BigComplex number: the conjugate of x+i*y is x-i*y
			return (new BigComplex(value.m_real, BigDecimal.Negate(value.m_imaginary)));

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
			BigComplex negQuot = Negate(Divide(this, mod));
			return Add(this, Multiply(mod, negQuot));
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

		public static implicit operator BigComplex((double, double) value)
		{
			return new BigComplex(value.Item1, value.Item2);
		}
		public static implicit operator BigComplex(Int16 value)
		{
			return (new BigComplex((BigDecimal)value));
		}
		public static implicit operator BigComplex(Int32 value)
		{
			return (new BigComplex((BigDecimal)value));
		}
		public static implicit operator BigComplex(Int64 value)
		{
			return (new BigComplex((BigDecimal)value));
		}
		public static implicit operator BigComplex(UInt16 value)
		{
			return (new BigComplex((BigDecimal)value));
		}
		public static implicit operator BigComplex(UInt32 value)
		{
			return (new BigComplex((BigDecimal)value));
		}
		public static implicit operator BigComplex(UInt64 value)
		{
			return (new BigComplex((BigDecimal)value));
		}
		public static implicit operator BigComplex(SByte value)
		{
			return (new BigComplex((BigDecimal)value));
		}
		public static implicit operator BigComplex(Byte value)
		{
			return (new BigComplex((BigDecimal)value));
		}
		public static implicit operator BigComplex(Single value)
		{
			return (new BigComplex((BigDecimal)value));
		}
		public static implicit operator BigComplex(Double value)
		{
			return (new BigComplex((BigDecimal)value));
		}
		public static explicit operator BigComplex(BigDecimal value)
		{
			return (new BigComplex(value));
		}
		public static explicit operator BigComplex(Decimal value)
		{
			return (new BigComplex((BigDecimal)value));
		}

		#endregion

		#region Trigonometric operations (methods implementing ITrigonometric) 

		public static BigComplex Sin(BigComplex value)
		{
			BigDecimal a = value.m_real;
			BigDecimal b = value.m_imaginary;
			return new BigComplex(BigDecimal.Sin(a) * BigDecimal.Cosh(b), BigDecimal.Cos(a) * BigDecimal.Sinh(b));
		}

		public static BigComplex Sinh(BigComplex value) // Hyperbolic sin 
		{
			BigDecimal a = value.m_real;
			BigDecimal b = value.m_imaginary;
			return new BigComplex(BigDecimal.Sinh(a) * BigDecimal.Cos(b), BigDecimal.Cosh(a) * BigDecimal.Sin(b));

		}

		public static BigComplex Asin(BigComplex value) // Asin 
		{
			return (-ImaginaryOne) * Log(ImaginaryOne * value + Sqrt(One - value * value));
		}

		public static BigComplex Cos(BigComplex value)
		{
			BigDecimal a = value.m_real;
			BigDecimal b = value.m_imaginary;

			return new BigComplex(BigDecimal.Cos(a) * BigDecimal.Cosh(b), -(BigDecimal.Sin(a) * BigDecimal.Sinh(b)));
		}

		public static BigComplex Cosh(BigComplex value) // Hyperbolic cos 
		{
			BigDecimal a = value.m_real;
			BigDecimal b = value.m_imaginary;
			return new BigComplex(BigDecimal.Cosh(a) * BigDecimal.Cos(b), BigDecimal.Sinh(a) * BigDecimal.Sin(b));
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
			BigComplex Two = new BigComplex(new BigDecimal(2), BigDecimal.Zero);

			BigComplex log1 = Log(One - Multiply(ImaginaryOne, value));
			BigComplex log2 = Log(One + Multiply(ImaginaryOne, value));

			BigComplex div = Divide(ImaginaryOne, Two);

			return Multiply(div, Subtract(log1, log2));
		}

		public static BigDecimal Atan(BigDecimal value) // Arctan for BigDecimal
		{
			BigComplex val = new BigComplex(value);
			BigComplex Two = new BigComplex(new BigDecimal(2), BigDecimal.Zero);

			BigComplex log1 = Log(One - ImaginaryOne * val);
			BigComplex log2 = Log(One + ImaginaryOne * val);

			BigComplex div = Divide(ImaginaryOne, Two);

			BigComplex ttl = Multiply(div, Subtract(log1, log2));

			return Abs(ttl);
		}

		public static BigComplex Atan2_BigDecimal(BigDecimal y, BigDecimal x) // 2-argument arctangent 
		{
			//sqrt(a ^ 2 + b ^ 2)

			BigComplex numerator = new BigComplex(y, x);

			BigDecimal addSquares = BigDecimal.Add(y.Square(), x.Square());
			BigDecimal sqrt = BigDecimal.NthRoot(addSquares, 2, Precision);

			BigComplex denominator = new BigComplex(sqrt);

			BigComplex quotient = numerator / denominator;

			BigComplex negI = new BigComplex(BigDecimal.Zero, BigDecimal.MinusOne);

			BigComplex result = negI * Log(quotient);

			return result;
		}


		public static BigDecimal Atan2(BigDecimal y, BigDecimal x)
		{
			BigDecimal result = 0;
			if (x > 0)
			{
				result = BigDecimal.Arctan(y / x);
			}
			else if (x < 0 && y >= 0)
			{
				result = BigDecimal.Arctan(y / x) + BigDecimal.Pi;
			}
			else if (x < 0 && y < 0)
			{
				result = BigDecimal.Arctan(y / x) - BigDecimal.Pi;
			}
			else if (x == 0 && y > 0)
			{
				result = BigDecimal.Pi / 2;
			}
			else if (x == 0 && y < 0)
			{
				result = -BigDecimal.Pi / 2;
			}
			return result;
		}


		public static BigDecimal Atan2(BigComplex y, BigComplex x) // 2-argument arctangent 
		{
			BigComplex a = new BigComplex(y.Real, y.Imaginary);
			BigComplex b = new BigComplex(x.Real, x.Imaginary);

			if (y.Sign > 0)
			{
				return BigComplex.Abs(BigComplex.Atan(BigComplex.Divide(b, a)));
			}
			else if (y.Sign < 0 && x.Sign >= 0)
			{
				BigComplex tmp = BigComplex.Atan(BigComplex.Divide(b, a));
				BigComplex tmp2 = new BigComplex(tmp.Real + BigDecimal.Pi, tmp.Imaginary);
				return BigComplex.Abs(tmp2);
			}
			else if (y.Sign < 0 && x.Sign < 0)
			{
				BigComplex tmp = BigComplex.Atan(BigComplex.Divide(b, a));
				BigComplex tmp2 = new BigComplex(tmp.Real - BigDecimal.Pi, tmp.Imaginary);
				return BigComplex.Abs(tmp2);
			}
			else if (y.Sign == 0 && x.Sign > 0)
			{
				return (BigDecimal.Pi / 2);
			}
			else if (y.Sign == 0 && x.Sign < 0)
			{
				return BigDecimal.Negate(BigDecimal.Pi / 2);
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

		public BigDecimal DegreeToRadian(BigDecimal angle)
		{
			return BigDecimal.Pi * angle / 180.0;
		}

		public BigDecimal RadianToDegree(BigDecimal angle)
		{
			return angle * (180.0 / BigDecimal.Pi);
		}

		#endregion

		#region Other numerical functions

		public static BigComplex Log(BigComplex value) // Log of the BigComplex number value to the base of 'e' 
		{
			BigDecimal arctan = Atan2(value.m_imaginary, value.m_real);

			BigDecimal abs = Abs(value);

			BigDecimal log = BigDecimal.Ln(abs);

			return new BigComplex(log, arctan);
		}

		public static BigComplex Log(BigDecimal value, BigDecimal baseValue) // Log of the BigComplex number of some base
		{
			return new BigComplex(BigDecimal.Ln(value) / BigDecimal.Ln(baseValue));
		}


		public static BigComplex Exp(BigComplex value) // The BigComplex number raised to e 
		{
			BigDecimal temp_factor = BigDecimal.Exp(value.m_real);
			BigDecimal result_re = temp_factor * BigDecimal.Cos(value.m_imaginary);
			BigDecimal result_im = temp_factor * BigDecimal.Sin(value.m_imaginary);
			return (new BigComplex(result_re, result_im));
		}


		public static BigComplex Sqrt(BigComplex value, int? precision = null) // Square root of the BigComplex number 
		{
			BigDecimal sqrtMagnitude = BigDecimal.NthRoot(value.Magnitude, 2, precision.HasValue ? precision.Value : Precision);

			BigDecimal halfPhase = value.Phase / new BigDecimal(2);

			return BigComplex.FromPolarCoordinates(sqrtMagnitude, halfPhase);
		}
		/*
		public static BigComplex Sqrt3(BigComplex value) // Square root of the BigComplex number 
		{
			BigDecimal sqrtMagnitude = BigDecimal.NthRoot(value.Magnitude, 2, Precision);
			BigDecimal halfPhase = (value.Phase) / new BigDecimal(2);

			BigComplex sqrt = BigComplex.FromPolarCoordinates(sqrtMagnitude, halfPhase);
			return sqrt;
		}

		public static BigComplex Sqrt4(BigComplex value, int? precision = null) // Square root of the BigComplex number 
		{
			BigDecimal x = BigDecimal.Subtract(value.Real.Square(), value.Imaginary.Square());
			BigDecimal y = BigDecimal.Multiply(2, BigDecimal.Multiply(value.Real, value.Imaginary));

			BigDecimal xy2 = BigDecimal.Add(x.Square(), y.Square());
			BigDecimal xy2Sqrt = BigDecimal.NthRoot(xy2, 2, precision.HasValue ? precision.Value : Precision);

			BigDecimal a2 = BigDecimal.Divide(BigDecimal.Add(xy2Sqrt, x), 2);
			BigDecimal b2 = BigDecimal.Divide(BigDecimal.Subtract(xy2Sqrt, x), 2);

			BigDecimal a = BigDecimal.NthRoot(a2, 2, precision.HasValue ? precision.Value : Precision);
			BigDecimal b = BigDecimal.NthRoot(b2, 2, precision.HasValue ? precision.Value : Precision);

			return new BigComplex(a, b);
		}

		public static BigComplex Sqrt5(BigComplex value, int? precision = null) // Square root of the BigComplex number 
		{
			BigComplex z = value.Clone();
			BigDecimal absR = Abs(value);
			BigComplex r = new BigComplex(absR);

			BigComplex top = Add(z, r);
			BigComplex bottom = new BigComplex(Abs(top));

			BigComplex quotient = Divide(top, bottom);

			BigDecimal sqrtR = BigDecimal.NthRoot(absR, 2, precision.HasValue ? precision.Value : Precision);

			BigComplex result = Multiply(new BigComplex(sqrtR), quotient);

			return result;
		}

		public static BigComplex Sqrt6(BigComplex value, int? precision = null) // Square root of the BigComplex number 
		{
			BigDecimal two = new BigDecimal(2);
			BigDecimal sqrXY = BigDecimal.NthRoot(value.Real.Square() + value.Imaginary.Square(), 2, precision.HasValue ? precision.Value : Precision);

			BigComplex sgn = new BigComplex(new BigDecimal(value.Imaginary.Sign == 0 ? 1 : value.Imaginary.Sign));

			BigDecimal A = BigDecimal.Divide(BigDecimal.Add(sqrXY, value.Real), two);
			BigDecimal B1 = BigDecimal.Subtract(sqrXY, value.Real);
			BigDecimal B = BigDecimal.Divide(B1, two);

			BigComplex left = new BigComplex(BigDecimal.NthRoot(BigDecimal.Abs(A), 2, precision.HasValue ? precision.Value : Precision));
			BigComplex right = new BigComplex(BigDecimal.NthRoot(BigDecimal.Abs(B), 2, precision.HasValue ? precision.Value : Precision));

			BigComplex rhs = BigComplex.Multiply(BigComplex.Multiply(BigComplex.ImaginaryOne, sgn), right);

			BigComplex result = BigComplex.Add(left, rhs);

			return new BigComplex(
									BigDecimal.Round(result.Real, precision.HasValue ? precision.Value : Precision),
								BigDecimal.Round(result.Imaginary, precision.HasValue ? precision.Value : Precision)
								);
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

			BigDecimal a = value.m_real;
			BigDecimal b = value.m_imaginary;
			BigDecimal c = power.m_real;
			BigDecimal d = power.m_imaginary;

			BigDecimal rho = BigComplex.Abs(value);
			BigDecimal theta = BigComplex.Atan2(b, a);
			BigDecimal newRho = c * theta + d * BigDecimal.Ln(rho);

			BigDecimal t = BigDecimal.Pow(rho, c, Precision) * BigDecimal.Pow(BigDecimal.E, BigDecimal.Negate(d) * theta, Precision);

			return new BigComplex(t * BigDecimal.Cos(newRho), t * BigDecimal.Sin(newRho));
		}

		public static BigDecimal Norm(BigComplex source)
		{
			return BigComplex.Abs(source);
		}

		public static BigComplex GCD(BigComplex a, BigComplex b)
		{
			BigDecimal aNorm = BigComplex.Norm(a);
			BigDecimal bNorm = BigComplex.Norm(b);

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

		private static BigComplex Scale(BigComplex value, BigDecimal factor)
		{
			BigDecimal result_re = factor * value.m_real;
			BigDecimal result_im = factor * value.m_imaginary;
			return (new BigComplex((BigDecimal)result_re, (BigDecimal)result_im));
		}

		private static void Swap(ref BigComplex a, ref BigComplex b)
		{
			BigComplex c = a;
			a = b;
			b = c;
		}

		#endregion

		#region Formatting/Parsing options 
		public override Int32 GetHashCode()
		{
			Int32 n1 = 99999997;
			Int32 hash_real = this.m_real.GetHashCode() % n1;
			Int32 hash_imaginary = this.m_imaginary.GetHashCode();
			Int32 final_hashcode = hash_real ^ hash_imaginary;
			return (final_hashcode);
		}

		public override String ToString()
		{
			if (this.m_imaginary == 0)
			{
				BigDecimal fractional = this.m_real.GetFractionalPart();
				if (fractional.IsZero())
				{
					return this.m_real.WholeValue.ToString();
				}
				else
				{
					return this.m_real.ToString();
				}
			}
			string sign = "+";
			if (this.m_imaginary.Sign == -1)
			{
				sign = "-";
			}
			return String.Format(CultureInfo.CurrentCulture, "{0} {1} {2}i", this.m_real, sign, BigDecimal.Abs(this.m_imaginary));
		}

		#endregion

	}
}
