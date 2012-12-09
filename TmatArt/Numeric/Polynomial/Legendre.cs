using System;
using System.Linq;

namespace TmatArt.Numeric.Polynomial
{
	/**
	 * Legendre polymonials (also associated polynomials) of the first kind
	 * 
	 * @author Vladimir Schmidt
	 * @date 28 Sep 2012
	 * @see http://mathworld.wolfram.com/LegendrePolynomial.html
	 * Here,
	 * 	P_0(x) = 1, P_1(x) = x, P_2(x) = 0.5*(3x^2-1), ...
	 * 
	 * Definition of associated Legendre polynomials
	 * 		P_n^m(x)  = (-1)^m (1-x^2)^{m/2} \frac{d^m}{dx^m}(P_n(x))
	 * 		P_n^0(x)  = P_n(x)
	 * 
	 * Alternative notations
	 *		P_{nm}(x) = (-1)^m P_n^m(x)
	 * 
	 * Used recurrence relations
	 * 	(n-m+1) P_{n+1}^m(x) = (2n+1) x P_n^m(x) - (n+m) P_{n-1}^m(x)
	 * 	\sqrt{1-x^2} P_{n}^{m+1}(x) = (n-m) x P_n^m(x) - (n+m) P_{n-1}^m(x)
	 */
	public class Legendre : Abstract
	{
		/**
		 * Left and right bounds of orthogonality range
		 */
		public override double left
		{
			get
			{
				return -1E0;
			}
		}
		public override double right
		{
			get
			{
				return 1E0;
			}
		}

		/**
		 * Maximal number of iterations within computation of polynomials roots
		 */
		public const int iterRoot = 1000;
		
		/**
		 * Parameter m of Legendre polynomial (if m = 0, then this is plain Legendre polynomial)
		 */
		public int m;

		public Legendre (int m = 0)
		{
			this.m = m;
		}

		/**
		 * @see inherited
		 * @todo issue in computation of derivation (dp) in the recurence formula
		 * for argument x, that |x| = 1. At the present time, asymptotic value (for m=0) is used.
		 * Let use either other formula to compute derivation, or compute asymptotic value
		 * for these special cases for m>0
		 */
		public override System.Collections.Generic.IEnumerable<Value> compute (double x, int n)
		{
			if (System.Math.Abs(x) > 1)
				throw new ArgumentOutOfRangeException("Argument of Legendre polynom must be in the range [-1, 1]");
			if (n < 0)
				throw new ArgumentOutOfRangeException("Degree of Legendre polynom must be positive");
			
			int m  = this.m;
			int ml = System.Math.Abs(m);
			if (ml > n) yield break;
			double fact = 1;
			if (m < 0)
			{
				for (int j=1; j<=2*ml; j++) fact *= j;
				fact = (m%2==0 ? 1 : -1) / fact;
			}

			// compute P_m^m(x), P_{m-1}^m(x)=0
			double p1 = fact, p2 = 0E0;
			if (ml>0)
			{
				double y = System.Math.Sqrt(1-x*x);
				for (int j=0; j<ml; j++) p1 *= -(2*j+1) * y;
			}
			
			// compute P_{n-1}^m(x), P_{n}^m(x)
			Value val = new Value {
				n  = ml,
				p  = p1,
				dp = p1*ml*x / (x * x - 1E0)
			};
			yield return val;

			for (int j=ml+1; j<=n; j++)
			{
				double p3 = p2;
				p2 = p1;
				p1 = ((2*j-1) * x * p2 - (j-1+m) * p3) / (double)(j-m);
				val.n  = j;
				val.p = p1;
				if (System.Math.Abs(x) >= 1-Double.Epsilon && m == 0)
					val.dp = (n % 2 == 0 ? System.Math.Sign(x) : 1)*n*(n+1)/2;
				else
					val.dp = (n * x * p1 - (n+m) * p2) / (x * x - 1E0);
				yield return val;
			}
		}

		/**
		 * @see inherited
		 */
		public override double norm(int n)
		{
			double res = 2E0/(2*n+1);
			int m = this.m;

			if (m != 0)
			{
				for (int i=n-m+1; i<=n+m; i++)
					res = res * i;
			}
			return System.Math.Sqrt(res);
		}
		
		/**
		 * @see inherited
		 */
		public override double cristoffel(double x, int n)
		{
			double pp = this.computeOne(x, n).dp;
			return 2E0 / ((1E0 - x * x) * pp * pp);
		}

		/**
		 * @see inherited
		 */
		public override System.Collections.Generic.IEnumerable<double> roots (int n)
		{
			if (this.m != 0) throw new Exception("Parameter m for root finding has to be 0");
			
			// compute roots with algorithm from numerical recipes (first approximation + Newton's iteration)
  			int n2 = (int)(0.5E0 * (double)(n + 1));
  			for (int i=1; i<=n2; i++)
			{
  				double pp, p1;
			    double z1 = 0E0;
				double z  = System.Math.Cos(System.Math.PI * (i - 0.25E0) / (n + 0.5E0)); // first approximation for i-th root
				int  iter = Legendre.iterRoot;
			    do
				{
					// compute Legendre polynom and its derivation
					Value val = this.computeOne(z, n);
					p1 = val.p;
					pp = val.dp;
					
					// Newton's iteration
      				z1 = z;
      				z  = z1 - p1 / pp; 
    				if (--iter == 0) throw new Exception("The " + i + "-th root of the n-th Legendre polynom could not be found");
				}
				while (System.Math.Abs(z - z1) > Legendre.epsRoot);
				if (i-1 != n-i) yield return -z; // x[i-1]
				yield return z;  // x[n-i]
  			}
		}
	}

}

