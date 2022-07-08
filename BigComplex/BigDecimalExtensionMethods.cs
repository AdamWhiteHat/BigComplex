using System;
using System.Linq;
using System.Text;
using System.Numerics;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace ExtendedNumerics
{
	public static class BigDecimalExtensionMethods
	{
		public static BigDecimal Square(this BigDecimal number)
		{
			return BigDecimal.Multiply(number, number);
		}

		public static BigDecimal Clone(this BigDecimal source)
		{
			return new BigDecimal(source.Mantissa, source.Exponent);
		}
	}
}
