using System;

namespace TmatArt.Numeric
{
	/// <remarks>DEPRECATED</remarks>		
	public static class Math
	{
		/* Trigonometric function: SIN */
		public static Complex Sin(Complex arg)
		{
			double eim  = System.Math.Exp(arg.im) / 2;
			double eim1 = 0.25E0 / eim;
			
			return new Complex(
				System.Math.Sin(arg.re) * (eim + eim1),
				System.Math.Cos(arg.re) * (eim - eim1)
			);
		}		
		public static double Sin(double arg)
		{
			return System.Math.Sin(arg);
		}

		/* Trigonometric function: COS */
		public static Complex Cos(Complex arg)
		{
			double eim  = System.Math.Exp(arg.im) / 2;
			double eim1 = 0.25E0 / eim;
			
			return new Complex(
				System.Math.Cos(arg.re) * (eim1 + eim),
				System.Math.Sin(arg.re) * (eim1 - eim)
			);
		}		
		public static double Cos(double arg)
		{
			return System.Math.Cos(arg);
		}
		
		/* Trigonometric function: Tan */
		public static Complex Tan(Complex arg)
		{
			double eim  = System.Math.Exp(arg.im);
			double eim1 = 1E0 / eim;
			double delta_p = eim + eim1;
			double delta_m = eim - eim1;
			double sin = System.Math.Sin(arg.re);
			double cos = System.Math.Cos(arg.re);
			
			return Complex.c(sin*delta_p, cos*delta_m) / Complex.c(cos*delta_p, -sin*delta_m);
		}		
		public static double Tan(double arg)
		{
			return System.Math.Tan(arg);
		}
		
		/* Logarithm */
		public static Complex Log(Complex arg)
		{
			double norm = arg.abs();
			double phi  = 0;
			if (System.Math.Abs(norm) > double.Epsilon) phi = System.Math.Asin(arg.im / norm);
			return new Complex(System.Math.Log(norm), phi);
		}
		public static double Log(double arg)
		{
			return System.Math.Log(arg);
		}
		
		/* Exponent */
		public static Complex Exp(Complex arg)
		{
			double tmp = System.Math.Exp(arg.re);
			return new Complex(tmp * System.Math.Cos(arg.im), tmp * System.Math.Sin(arg.im));
		}
		public static double Exp(double arg)
		{
			return System.Math.Exp(arg);
		}
	}
}

