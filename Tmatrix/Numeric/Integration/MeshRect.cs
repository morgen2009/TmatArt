using System;
using System.Collections.Generic;

namespace TmatArt.Numeric.Integration
{
	/**
	 * Class to integrate the function over the range (R^1)
	 * using the trapezoidal, Simpson's or Simpson's 3/8 integration rule.
	 * 
	 * @author Vladimir Schmidt
	 * @date 28 Sep 2012
	 *
	 * These formulas use the integration rule, which is exact for polynomials of degree N,
	 * for each following set consisting of (N+1) integration points. The number N is equal to 
	 * 1 for the trapezoidal, 2 for Simpson's and 3 Simpson's 3/8 integration rule.
	 * 
	 * The integration points are homogeneously distributed on the integration range. The total number
	 * of integration points has to be N * k + 1, where k is integer.
	 */
	public class MeshRect : Mesh
	{
		/** Number of nodes */
		protected int count;
		
		/** Weights of nodes for the current window */
		protected double [] weights;
		
		/** Type of the integration rule (trapezoidal, Simpson's or Simpson's 3/8) */
		public enum IntegralType { Trapezoidal = 1, Simpson = 2, Simpson38 = 3 };
		
     	/**
	     * Constructor
	     * 
	     * @param count	int number of integration nodes
	     * @param left	double left bound of the integration range (optional, default=0)
	     * @param right	double right bound of the integration range (optional, default=1)
	     * @param itype	IntegralType subtype of integration rule (optional, default=IntegralType.Trapezoidal)
	 	 */
		public MeshRect(int count, double left = 0E0, double right = 1E0, IntegralType itype = IntegralType.Trapezoidal) : base(left, right, 1E0)
		{
			if ((count - 1) % ((int)itype) != 0)
				throw new Exception("Wrong number of the integration points. The number of points has to be " + (int)itype + " * N + 1");
			
			this.count = count;

			/* compute weights for window */
			double h  = (right - left) / (count - 1);
			switch (itype)
			{
			case IntegralType.Trapezoidal : /* for each set weights are 1 1 */
				this._norm = h / 2E0;
				this.weights = new double [] { 1E0, 1E0 };
				break;
			case IntegralType.Simpson :     /* for each set weights are 1 4 1 */
				this._norm = h / 3E0;
				this.weights = new double [] { 1E0, 4E0, 1E0 };
				break;
			case IntegralType.Simpson38 :   /* for each set weights are 1 3 3 1 */
				this._norm = 3E0 * h / 8E0;
				this.weights = new double [] { 1E0, 3E0, 3E0, 1E0 };
				break;
			default :
				throw new Exception("Unknown integration rule is used. The allowed integration types are Trapezoidal, Simpson or Simpson38 ");
			}
		}

		/**
		 * Iteration over all node elements
		 */
		public override IEnumerable<MeshNode> nodes()
		{
			double h      = (this.right - this.left) / (this.count - 1);
			int    t_last = this.count-1;
			int    w_last = this.weights.Length-1;
			
			// left bound
			MeshNode res;
			res.point  = this.left;
			res.weight = this.weights[0];
			yield return res;
			
			// middle points and right bound
			for (int i=1; i<=t_last;)
			{
				for (int j=1; j<=w_last; j++, i++)
				{
					res.point += h;
					res.weight = (j==w_last && i!=t_last) ? this.weights[0] + this.weights[w_last] : this.weights[j];
					
					yield return res;
				}
			}
		}
	}
}