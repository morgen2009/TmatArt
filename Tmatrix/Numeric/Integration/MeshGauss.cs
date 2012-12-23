using System;
using System.Collections.Generic;

namespace TmatArt.Numeric.Integration
{
	/**
	 * Class to integrate the function over the range (R^1)
	 * using integration rules of Gaussian type.
	 * 
	 * @author Vladimir Schmidt
	 * @date 28 Sep 2012
	 * @description
	 * The approximation of the integral
	 * 				$\int\limits_{a}^{b} f(x) dx$
	 * is given in the form
	 * 				$\sum\limits_{i=1}^{n} f(x_i) w_i$,
	 * where $x_i$ and $w_i$ are positions and weights of nodes. The quadrature rule constructed
	 * to yield an exact result for polynomials $f(x)$ of degree $2n âˆ’ 1$ or less by a suitable
	 * choice of the points $x_i$ and weights $w_i$ for i = 1,...,n.
	 * 
	 * The theory are outlined in the book by Abramowitz & Stegun (1972) p. 887. The points $x_i$
	 * are roots of n-th orthogonal polynomials $f_n(x)$, the weights $w_i$ are Cristoffel numbers.
	 * The polynomials are determined by the weight function $w(x)$.
	 */
	public class MeshGauss : Mesh
	{
		/**
		 * Points and weights of the integration rule
		 */
		protected double [] points, weights;
		
     	/**
	     * Constructor
	     * 
	     * @param count	int number of integration nodes
	     * @param pol   Polynomial.Abstract orthogonal polynomial detemined by weight function
	     * @param left	double left bound of the integration range (optional, default=pol.left)
	     * @param right	double right bound of the integration range (optional, default=pol.right)
	 	 */
		public MeshGauss(int count, Polynomial.Abstract pol, double left = double.NaN, double right = double.NaN) : base(pol.left, pol.right, 1E0)
		{
			// allocate memory for points and weights
			this.points  = new double [count];
			this.weights = new double [count];
			
			// nodes are roots of the polynomial of degree (count)
			int cnt = 0;
			foreach (double x in pol.roots(count))
			{
				this.points[cnt++] = x;
			}

			// weights are Cristoffel numbers for the polynomial at nodes
			for (int i=0; i<cnt; i++)
				this.weights[i] = pol.cristoffel(this.points[i], count);

			// rescale points
			if (left != double.NaN && right != double.NaN)
			{
				double h = (right - left) / (pol.right - pol.left);
				if (!double.IsNaN(h) && !double.IsInfinity(h))
				{
					for (int i=0; i<cnt; i++)
						this.points[i] = (this.points[i] - pol.left) * h + left;
					this._norm = h;
					this._left  = left;
					this._right = right;
				}
			}
		}
		
		/**
		 * @see inherited
		 */
		public override IEnumerable<MeshNode> nodes()
		{
			for (int i=0; i<this.points.Length; i++)
			{
				yield return new MeshNode {
					point  = this.points[i],
					weight = this.weights[i]
				};
			}
		}
	}
}