using System;

namespace TmatArt.Geometry
{
	public class Euler
	{
		protected double alpha, beta, gamma;
		
		public Euler (double alpha, double beta, double gamma)
		{
			this.alpha = alpha;
			this.beta  = beta;
			this.gamma = gamma;
		}
		
		/**
		 * @todo
		 * 
		 * operation + (Euler a, Euler b)
		 * operation - (Euler a)
		 */
	}
}

