using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace ExtendedNumerics.ExtensionMethods
{
	public static class ComplexExtensionMethods
	{
		public static string FormatString(this Complex source)
		{
			var real = source.Real;
			var im = source.Imaginary;

			bool includeReal = (real != 0 && real != double.NaN && real != double.NegativeInfinity && real != double.PositiveInfinity);
			bool includeIm = (im != 0 && im != double.NaN && im != double.NegativeInfinity && im != double.PositiveInfinity);

			if (!includeReal && !includeIm)
			{
				return "0";
			}

			string result = "";
			if (includeReal)
			{
				result = real.ToString();
			}

			if (includeIm)
			{
				bool imNegative = (im < 0);
				string signText = imNegative ? "-" : "+";

				if (includeReal)
				{
					result += $" {signText} ";
				}
				else if (imNegative)
				{
					result += signText;
				}

				double absIm = Math.Abs(im);
				if (absIm > 1)
				{
					result += $"{absIm}";
				}

				result += "i";
			}

			return result;
		}

		public static Complex Clone(this Complex source)
		{
			return new Complex(source.Real, source.Imaginary);
		}

		public static int Sign(this Complex source)
		{
			return (source.Real == 0) ? Math.Sign(source.Imaginary) : Math.Sign(source.Real);
		}

		public static Complex Mod(this Complex source, Complex other)
		{
			Complex negQuot = Complex.Negate(Complex.Divide(source, other));

			Complex ceil = new Complex(Math.Round(negQuot.Real), Math.Round(negQuot.Imaginary));

			return Complex.Add(source, Complex.Multiply(other, ceil));
		}
	}
}
