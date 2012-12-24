using System;

namespace TmatArt.Numeric.Mathematics
{
	public class MathC : IMath<Complex, double>
	{
		private static MathC instance = null;

		public static MathC Instance()
		{
			if (MathC.instance == null) {
				MathC.instance = new MathC();
			}
			return MathC.instance;
		}

		public Complex Cos (Complex arg)
		{
			double eim  = System.Math.Exp(arg.im) / 2;
			double eim1 = 0.25E0 / eim;
			
			return new Complex(
				System.Math.Cos(arg.re) * (eim1 + eim),
				System.Math.Sin(arg.re) * (eim1 - eim)
			);
		}

		public Complex Sin (Complex arg)
		{
			double eim  = System.Math.Exp(arg.im) / 2;
			double eim1 = 0.25E0 / eim;
			
			return new Complex(
				System.Math.Sin(arg.re) * (eim + eim1),
				System.Math.Cos(arg.re) * (eim - eim1)
			);
		}

		public Complex Tan (Complex arg)
		{
			double eim  = System.Math.Exp(arg.im);
			double eim1 = 1E0 / eim;
			double delta_p = eim + eim1;
			double delta_m = eim - eim1;
			double sin = System.Math.Sin(arg.re);
			double cos = System.Math.Cos(arg.re);
			
			return new Complex(sin*delta_p, cos*delta_m) / new Complex(cos*delta_p, -sin*delta_m);
		}

		public Complex Acos (Complex arg)
		{
			return -Complex.AIM * this.Log(arg + Complex.AIM * this.Sqrt(1 - arg*arg));
		}
		
		public Complex Asin (Complex arg)
		{
			return -Complex.AIM * this.Log(this.Sqrt(1 - arg*arg) + Complex.AIM * arg);
		}
		
		public Complex Atan (Complex arg)
		{
			return Complex.AIM / 2.0 * this.Log((Complex.AIM + arg) / (Complex.AIM - arg));
		}
		
		public Complex Log (Complex arg)
		{
			// get normalized presentation of complex number
			double norm = this.Abs(arg);
			double phi  = this.Argument(arg, norm);

			// apply operation and return result
			return new Complex(System.Math.Log(norm), phi);
		}

		public Complex Exp (Complex arg)
		{
			return Complex.Euler(System.Math.Exp(arg.re), arg.im);
		}

		public Complex Sqrt (Complex arg)
		{
			// get normalized presentation of complex number
			double norm = this.Abs(arg);
			double phi  = this.Argument(arg, norm);

			// apply operation and return result
			return Complex.Euler(System.Math.Sqrt(norm), phi / 2);
		}

		public Complex Pow (Complex arg, Complex deg)
		{
			return this.Exp(deg * this.Log(arg));
		}

		public Complex Pow (Complex arg, double deg)
		{
			// get normalized presentation of complex number
			double norm = this.Abs(arg);
			double phi  = this.Argument(arg, norm);

			// apply operation and return result
			return Complex.Euler(System.Math.Pow(norm, deg), phi * deg);
		}

		public Complex Pow (Complex arg, int deg)
		{
			return this.Pow(arg, (double)deg);
		}

		public double Abs (Complex arg)
		{
			return System.Math.Sqrt(arg.re * arg.re + arg.im * arg.im);
		}

		public Complex Aim (int deg)
		{
			switch (deg % 4) {
				case 3:  return new Complex( 0,-1);
				case 2:  return new Complex(-1, 0);
				case 1:  return new Complex( 0, 1);
				default: return new Complex( 1, 0);
			}
		}

		public Complex Conjugate (Complex arg)
		{
			return new Complex(arg.re, -arg.im);
		}

		private double Argument (Complex arg, double norm)
		{
			double phi  = 0;
			
			if (norm > double.Epsilon) {
				phi = System.Math.Acos(arg.re / norm);
			}
			if (arg.im < 0) {
				phi = 2*System.Math.PI - phi;
			}

			return phi;
		}

		public double Argument (Complex arg)
		{
			return this.Argument(arg, this.Abs(arg));
		}
	}
}

