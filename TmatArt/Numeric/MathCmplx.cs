using System;

namespace TmatArt.Numeric
{
	/// <remarks>DEPRECATED</remarks>		
	public static class MathCmplx
	{
		/* Trigonometric function: SIN */
		public static Complex Sin(Complex arg)
		{
			double eim  = Math.Exp(arg.im) / 2;
			double eim1 = 0.25E0 / eim;
			
			return new Complex(
				Math.Sin(arg.re) * (eim + eim1),
				Math.Cos(arg.re) * (eim - eim1)
			);
		}		

		/* Trigonometric function: COS */
		public static Complex Cos(Complex arg)
		{
			double eim  = Math.Exp(arg.im) / 2;
			double eim1 = 0.25E0 / eim;
			
			return new Complex(
				Math.Cos(arg.re) * (eim1 + eim),
				Math.Sin(arg.re) * (eim1 - eim)
			);
		}		
		
		/* Trigonometric function: Tan */
		public static Complex Tan(Complex arg)
		{
			double eim  = Math.Exp(arg.im);
			double eim1 = 1E0 / eim;
			double delta_p = eim + eim1;
			double delta_m = eim - eim1;
			double sin = Math.Sin(arg.re);
			double cos = Math.Cos(arg.re);
			
			return Complex.c(sin*delta_p, cos*delta_m) / Complex.c(cos*delta_p, -sin*delta_m);
		}		
		
		/* Logarithm */
		public static Complex Log(Complex arg)
		{
			// get normalized presentation of complex number
			double norm = arg.abs();
			double phi  = 0;
			if (System.Math.Abs(norm) > double.Epsilon) phi = System.Math.Asin(arg.im / norm);
			
			// apply operation and return result
			return Complex.c(System.Math.Log(norm), phi);
		}
		
		/* Exponent */
		public static Complex Exp(Complex arg)
		{
			return Complex.c(System.Math.Exp(arg.re), arg.im);
		}

		/* Absolute value */
		public static double Abs(Complex arg)
		{
			return System.Math.Sqrt(arg.re * arg.re + arg.im * arg.im);
		}

		/* Sqrt */
		public static Complex Sqrt(Complex arg)
		{
			// get normalized presentation of complex number
			double norm = arg.abs();
			double phi  = 0;
			if (System.Math.Abs(norm) > double.Epsilon) phi = System.Math.Asin(arg.im / norm);
			
			// apply operation and return result
			return Complex.c(System.Math.Sqrt(norm), phi / 2);
		}
		
		/* Pow */
		public static Complex Pow(Complex arg, double degree)
		{
			// get normalized presentation of complex number
			double norm = arg.abs();
			double phi  = 0;
			if (System.Math.Abs(norm) > double.Epsilon) phi = System.Math.Asin(arg.im / norm);
			
			// apply operation and return result
			return Complex.c(System.Math.Pow(norm, degree), phi * degree);
		}
		public static Complex Pow(Complex arg, Complex degree)
		{
			return MathCmplx.Exp(degree * MathCmplx.Log(arg));
		}
	}
}

