using System;
using System.Linq;

namespace TmatArt.Numeric.Polynomial
{
	/**
	 * Laguerre polymonial (also associated polynomials)
	 * 
	 * @author Vladimir Schmidt
	 * @date 28 Sep 2012
	 * @see http://mathworld.wolfram.com/LaguerrePolynomial.html
	 * Here,
	 * 	L_0(x) = 1, L_1(x) = 1-x, L_2(x) = 0.5*(x^2-4x+2) , ...
	 * 
	 * Definition of associated Laguerre polynomials
	 *   L_n^{\alpha+1} (x) = -\frac{d}{dx} L_{n+1}^{\alpha} (x)
	 * 
	 */
	public class Laguerre : Abstract
	{
		/**
		 * Left and right bounds of orthogonality range
		 */
		public override double left
		{
			get
			{
				return 0E0;
			}
		}

		/**
		 * Maximal number of iterations within computation of polynomials roots
		 */
		public const int iterRoot = 1000;
		
		/**
		 * Parameter \alpha of Laguerre polynomial (if \alpha = 0, then this is plain Laguerre polynomial)
		 */
		public double alpha;

		public Laguerre (double alpha = 0E0)
		{
			if (alpha < -1E0)
				throw new Exception("Parameter alpha for Laguerre polynomials could not be less then -1");
			this.alpha = alpha;
		}

		/**
		 * @see inherited
		 */
		public override System.Collections.Generic.IEnumerable<Value> compute (double x, int n)
		{
			double p1 = 1E0, p2 = 0E0, dp1 = 0E0, dp2 = 0E0;
			yield return new Value {
				n  = 0,
				p  = p1,
				dp = dp1
			};
			
			for (int j=1, j0=0; j<=n; j0=j, j++) // j0 = j-1
			{
				double j0a = j0 + this.alpha;
				double p3   = (((j+j0a-x) * p1  - j0a * p2)) / (double)(j);
				double dp3  = (((j+j0a-x) * dp1 - p1 - j0a * dp2)) / (double)(j);
				p2 = p1;
				p1 = p3;
				dp2 = dp1;
				dp1 = dp3;
				
				Value val = new Value {
					n  = j,
					p  = p1,
					dp = dp1
				};
				yield return val;
			}
		}

		/**
		 * @see inherited
		 */
		public override double norm(int n)
		{
			double res = 1E0;
			
			for (int i=1; i<=n; i++)
				res *= (i + this.alpha) / i;
			
			return res * Numeric.Function.Gamma.compute(1E0+this.alpha);
		}
		
		/**
		 * @see inherited
		 */
		public override double cristoffel(double x, int n)
		{
			double pp = this.computeOne(x, n+1).dp;
			return 1E0 / (x * pp * pp);
		}

		/**
		 * @see inherited
		 */
		public override System.Collections.Generic.IEnumerable<double> roots (int n)
		{
			// compute roots with algorithm from numerical recipes (first approximation + Newton's iteration)
		    double z = 0E0, x1 = 0, x2 = 0, x3 = 0;
  			for (int i=1; i<=n; i++)
			{
				// first approximation for i-th root
			    if (i==1)
					z = 3E0 / (1E0 + 2.4E0 * n);
				else if (i==2)
					z = z + 15E0 / (1E0 + 2.5E0 * n);
				else
					z = z + (1E0+2.55E0*(i-2)) / (1.9E0*(i-2)) * (z-x3);
				double pp, p1, z1;
				int    iter = Laguerre.iterRoot;
				do
				{
					// compute Laguerre polynom and its derivation
					Value val = this.computeOne(z, n);
					p1 = val.p;
					pp = val.dp;
					
					// Newton's iteration
      				z1 = z;
      				z  = z1 - p1 / pp; 
    				if (--iter == 0) throw new Exception("The " + i + "-th root of the n-th Laguerre polynom could not be found");
				}
				while (System.Math.Abs(z - z1) > Laguerre.epsRoot);
				x3 = x2;
				x2 = x1;
				x1 = z;
				yield return z; // x[i-1]	
			}
		}
	}

}