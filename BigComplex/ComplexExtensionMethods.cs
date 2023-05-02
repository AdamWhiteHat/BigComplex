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
