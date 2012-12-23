using System;

namespace TmatArt.Numeric.Function
{
	/**
	 * Gamma function
	 * 
	 * @author Vladimir Schmidt
	 * @date 28 Sep 2012
	 * @todo implement complex argument
	 * 
	 * Numerical Recipes in C (2nd ed. Cambridge University Press, 1992) is an efficient algorithm, the so-called Lanczos approximation for computing the
	 * Gamma function for any positive argument (indeed, for any complex argument with a nonnegative real part) to a high level of accuracy. There is 
	 * formula, computing the Gamma function with an error that's smaller than 2E-10 for any complex z for which Re z>0:
	 * 
	 * 		Gamma(z) =  \frac{2\pi}{z}[ p_0 + \sum\limits_{n=1}^6 p_n / (z+n)] * (z+5.5)^{z+5.5} e^{-(z+5.5)}
	 * where
	 * 		p_0 = 1.000000000190015
	 * 		p_1 = 76.18009172947146
	 * 		p_2 = −86.50532032941677
	 * 		p_3 = 24.01409824083091
	 * 		p_4 = −1.231739572450155
	 * 		p_5 = 0.1208650973866179E-2
	 * 		p_6 = −0.5395239384953E-5
	 * 		p_7 = 2.5066282746310005
	 * 
	 * For argument z with negative real part (Re z < 0), the reflection formula for Gamma function is used
	 * 
	 * \Gamma(1-z) * \Gamma(z) = \frac{\pi}{sin(\pi z)}
	 * 
	 */
	public class Gamma
	{
		/**
		 * Compute Gamma function for given argument z
		 * @param z 	[in] argument of Gamma function
		 * @examples
		 * 		compute(1) = 1
		 * 		compute(2) = 1
		 * 		compute(3) = 2
		 * 		compute(4) = 6
		 * 		compute(5) = 24
		 */
		public static double compute (double z)
		{
			if (z < 0.5)
				// using reflection formula
				return System.Math.PI / ( System.Math.Sin(System.Math.PI * z) * compute(1-z) );
			else
				// using Lanczos approximation
				return compute(z, 5.0, ref lanczos_coeff5);
		}
		
		/**
		 * Coeffients within Lanczos approximation formula.
		 * The coefficients depend only on the parameter G, where it is chosen to satisfy Re(z+g+0.5) > 0
		 * @see http://en.wikipedia.org/wiki/Lanczos_approximation
		 */
		/* ... exact 10 digits ... used in Numerical Recipes, g = 5.0, n = 8 */
		private static double [] lanczos_coeff5 = {
			  	1.000000000190015,	76.18009172947146,	-86.50532032941677,
			 	24.01409824083091,	-1.231739572450155,	1.208650973866179E-3,
		    	-5.395239384953E-6
		};
		
		/**
		 * Compute Gamma function using Lanczos approximation formula
		 * @param z [in] argument
		 * @param g [in] parameter within the formula
		 * @param coef [in] expansion coefficients
		 */
		protected static double compute (double z, double g, ref double [] coef)
		{
			double res = coef[0];
			for (int i=1; i<coef.Length; i++) res += coef[i] / (z+i);

			double tmp = z+g+0.5;
			res *= System.Math.Pow(tmp, z+0.5) * System.Math.Exp(-tmp) * System.Math.Sqrt(2 * System.Math.PI) / z;
			
			return res;
		}
	}
}

