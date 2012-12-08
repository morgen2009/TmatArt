using System;
using System.Linq;

namespace TmatArt.Numeric.Polynomial
{
	/**
	 * Orthogonal polymonials
	 * @note The polynomials determined by the weight function or by its moments
	 */
	public class Arbitrary : Abstract
	{
		/**
		 * Coefficients for recurrence formula
		 */
		public double [] coef_a, coef_b;
		
		/**
		 * Weight function
		 */
		private Func<double, double> _weight;
		public Func<double, double> weight
		{
			get { return this._weight; }
		}
		
		/**
		 * Norn of ||p_0||^2
		 */
		private double _norm0 = 0E0;
		
		/**
		 * Left and right bounds of orthogonality range
		 */
		private double _left, _right;
		public override double left
		{
			get
			{
				return _left;
			}
		}
		public override double right
		{
			get
			{
				return _right;
			}
		}
		
		/**
		 * Create class of orthogonal polynomials determined by the weight function
		 * 
		 * @param left left bound of the range
		 * @param right right bound of the range
		 * @param order maximal order of the polynomial
		 * @param weight weight function defining orthogonal polanomials
		 * @param weight_n number of intergration points (within Gauss integration rule) to compute internal coefficients
		 */
		public Arbitrary(double left, double right, int order, Func<double, double> weight, int weight_n)
		{
			// save parameters
			this._left  = left;
			this._right = right;
			this._weight = weight;

			// allocate memory for elements
			this.coef_a = new double [order+1];
			this.coef_b = new double [order+1];
			
			Legendre l = new Legendre();
			Integration.MeshGauss m = new Integration.MeshGauss(weight_n, l, left, right);
			
			// loop over coefficients
			int i = 0;
			double pp_prev = 0E0;
			do
			{
				// compute integrals <p_n,p_n> and <x*p_n,p_n>
				double pp = 0E0, xpp = 0E0;
				
				foreach (Integration.MeshNode n in m.nodes())
				{
					double val = this.computeOne(n.point, i).p;
					val = val * val * weight(n.point) * n.weight * m.norm;
					
					pp  += val;
					xpp += val * n.point;
				}
				
				// compute coefficients
				this.coef_a[i] = xpp / pp;
				this.coef_b[i] = i > 0 ? pp / pp_prev : 0E0;
				if (i==0) this._norm0 = pp;
				pp_prev = pp;
				i++;
			} while (i <= order);
		}
		
		/**
		 * Create class of orthogonal polynomials based on moments of the weight function
		 * 
		 * @param left left bound of the range
		 * @param right right bound of the range
		 * @param order maximal order of the polynomial
		 * @param moment array of computed moments of weight function
		 * 
		 * @note Moments are computed as follows
		 *           M_k = \int_{left}^{right} w(x) x^k dx,
		 * where w(x) is the weight function.
		 */
		public Arbitrary(double left, double right, int order, double [] moment)
		{
			// save parameters
			this._left  = left;
			this._right = right;

			// allocate temporary memory
			double [] coef = new double [(order+1)*(order+2)/2];
			for (int j=1; j<coef.Length; j++) coef[j] = 0E0;
			coef[0] = 1E0;

			// allocate memory for elements
			this.coef_a = new double [order+1];
			this.coef_b = new double [order+1];
			
			// loop over coefficients
			int i = 0;
			double pp_prev = 0E0;
			do
			{
				// compute integrals <p_i,p_i> and <x*p_n,p_n>
				int j1 = (i+1)*i/2;
				double pp = 0E0, xpp = 0E0;
				for (int j=0; j<=i; j++)
					for (int k=0; k<=i; k++)
					{
						pp  += coef[j1+j]*coef[j1+k]*moment[j+k];
						xpp += coef[j1+j]*coef[j1+k]*moment[j+k+1];
					}
				
				// compute coefficients
				this.coef_a[i] = xpp / pp;
				this.coef_b[i] = i > 0 ? pp / pp_prev : 0E0;
				if (i==0) this._norm0 = pp;
				pp_prev = pp;
				
				// recompute coefficients of polynomials
				int j0 = j1 - i;
				int j2 = j1 + i + 1;
				for (int j=0; j<=i-1; j++)
					coef[j2+j] = -this.coef_b[i]*coef[j0+j]; // -\gamma_n * p_{n-1}
				for (int j=0; j<=i; j++)
				{
					coef[j2+j]   += -this.coef_a[i]*coef[j1+j]; // -\alpha_n * p_n
					coef[j2+j+1] += coef[j1+j]; // x * p_n
				}
				i++;
			} while (i <= order);
		}
		
		/**
		 * @see inherited
		 */
		public override System.Collections.Generic.IEnumerable<Value> compute (double x, int n)
		{
			if (n > this.coef_a.Length)
				throw new ArgumentOutOfRangeException(String.Format("Maximum degree of computed orthogomal polynom is exceed ({0} > {1})", n, this.coef_a.Length));
			
			yield return new Value {
				n  = 0,
				p  = 1E0,
				dp = 0E0
			};
			if (n == 0) yield break;
			
			int i = 0;
			double p1 = 0E0, p2 = 1E0, dp1 = 0E0, dp2 = 0E0;
			do
			{
				double dpt = dp2;
				dp2 = p2 + (x - this.coef_a[i]) * dpt - this.coef_b[i] * dp1;
				dp1 = dpt;

				double pt = p2;
				p2  = (x - this.coef_a[i]) * pt - this.coef_b[i] * p1;
				p1 = pt;

				i++;
				
				yield return new Value {
					n  = i,
					p  = p2,
					dp = dp2
				};
			} while (i < n);
		}
		
		/**
		 * @see inherited
		 */
		public override double norm(int n)
		{
			double res = this._norm0; // ||p_0||^2
			
			for (int i=1; i<=n; i++) // ||p_{n}||^2 = \beta_{n} * ||p_{n-1}||^2
				res *= this.coef_b[i];
			
			return res;
		}
		
		/**
		 * @see inherited
		 */
		public override double cristoffel(double x, int n)
		{
			double p1 = this.computeOne(x, n+1).p;
			double pp = this.computeOne(x, n).dp;
			return -this.norm(n) / (p1 * pp);
		}

		/**
		 * @see inherited
		 */
		public override System.Collections.Generic.IEnumerable<double> roots (int n)
		{
			return this.roots_interlacing(this._left, this._right, n);
		}
	}
}

