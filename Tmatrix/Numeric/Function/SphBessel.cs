
using System;

namespace TmatArt.Numeric.Function
{
	public class SphBessel
	{
		/**
		 * Parameters controlling the computation of the spherical Bessel functions
		 * @item ZeroSinXX  (real) - for |z| < ZeroSinXX, we set j_0(z) = sin(z)/z = 1 and  j_{1..N}(z) = 0
		 * @item MaxArgBes  (real) - for |z| > MaxArgBes, the asymptotic expressions of spherical Bessel
		 * 		functions for large arguments are used.
		 * @item FactNBes   (real) - determines the order of the Bessel functions Nstart, 
		 * 		at which the backward recurrence is started, i.e., Nstart = FactNBes * N
		 * @item InitBesVal (real) - initial value of the Bessel function at the beginning of
		 * 		the backward recurrence.
		 * @item UpperBoundSeq (real) - upper bound of the sequence of Bessel functions
		 * 		(if a Bessel function value exceeds this bound the entire sequence is scaled).
		 * @item LowerBoundSeq (real) - lower bound of the sequence of Bessel functions.
		 */
		private const double ZeroSinXX = 1E-3;
		private const double MaxArgBes = 1E3;
		private const int    FactNBes  = 400;
		private const double InitBesVal= 1E-35;
		private const double UpperBoundSeq = 1E10;
		private const double LowerBoundSeq = 1E-10;
		
		public struct Value {
			public int n;
			public double p;
			public double dp;
			
			public Value(int n = 0, double p = 0, double dp = 0)
			{
				this.n = n;
				this.p = p;
				this.dp = dp;
			}
		}

		/**
		 * spherical Bessel function j_n(x)
		 * 
		 * @param n  [in] order
		 * @param x  [in] argument
		 * @param p  [ref] function value
		 * @param dp [ref] functions derivate
		 * 
		 * @note
		 * The routine computes the spherical Bessel functions j(z) and the derivatives
		 * jd(z) = d(z*j(z))/dz. The order of the Bessel functions varies between 0 and N,
		 * and the backward recurrence relation
		 * 		j(n-1,z) + j(n+1,z) = [(2n+1) / z] * j(n,z).
		 * is used for computation. Here j(n,z) denotes the Bessel function of order n and
		 * argument z.
		 */
		public static System.Collections.Generic.IEnumerable<Value> j(double x, int n)
		{
			// validate
			if (n < 0) throw new ArgumentException(String.Format("Parameter n must not negative (n={0})", n));
			
			double xm = System.Math.Abs(x);
			
			if (xm < SphBessel.ZeroSinXX)
			{
				// use approximation formula for small x<<1
				// j_n(z->0) = z^n / (2n+1)!!
				Value val = new Value {
					n  = 0,
					p  = 1E0,
					dp = 0E0
				};
				yield return val;

				for (int k=1; k<=n; k++)
				{
					Value val1 = val;
					val.dp = val1.p * k / (2*k+1);
					val.p  = val1.p * x / (2*k+1);
					val.n  = k;
					yield return val;
				}
			}
			else if (xm >= SphBessel.ZeroSinXX && xm < SphBessel.MaxArgBes)
			{
				// use forward recurrence for the first two orders
				if (n < 2)
				{
					Value val0 = new Value {
						n  = 0,
						p  = System.Math.Sin(x) / x,
						dp = System.Math.Cos(x) / x - System.Math.Sin(x) / x / x
					};
					yield return val0;
					
					if (n == 1)
					{
						double p = (val0.p - System.Math.Cos(x)) / x; 
						Value val1 = new Value {
							n  = 1,
							p  = p,
							dp = x * val0.p - p
						};
						yield return val1;
					}
				}
				else
				// otherwise use backward recurrence relation for the functions
				{
					// get order where Bessel function becomes zero (due to restriction representation of float numbers)
					int Ma = (int)(xm + 4 * System.Math.Pow(xm, 1/3E0) + 2 + System.Math.Sqrt(101 + xm)) + 20;
					int Mn = n + (int)(System.Math.Sqrt(SphBessel.FactNBes * n));
					int M  = System.Math.Max(Ma,Mn);
					double f2 = 0E0;
					double f1 = SphBessel.InitBesVal;
					
					// store two first order
					double j0 = System.Math.Sin(x) / x;
					double j1 = (j0 - System.Math.Cos(x)) / x;
					
					// downward recurrence
					double[] p = new double[n+1];
					for (int k = M; k >= 0; k--)
					{
        				double f  = (2*k + 3) * f1 / x - f2;
						f2 = f1;
						f1 = f;
						if (k <= n) p[k] = f;
						
						// temporary rescale
						if (System.Math.Abs(f1) > SphBessel.UpperBoundSeq)
						{
							f2 = f2 * SphBessel.LowerBoundSeq;
							f1 = f1 * SphBessel.LowerBoundSeq;
          					for (int l = k; l <= n; l++) p[l] *= LowerBoundSeq;
						}
						else if (System.Math.Abs(f1) < SphBessel.LowerBoundSeq)
						{
							f2 = f2 * SphBessel.UpperBoundSeq;
							f1 = f1 * SphBessel.UpperBoundSeq;
          					for (int l = k; l <= n; l++) p[l] *= UpperBoundSeq;
						}
					}

					// rescale computed values and compute derivations
					double scale = System.Math.Abs(f1) > System.Math.Abs(f2) ? j0 / f1 : j1 / f2;

					Value val = new Value {
						n  = 0,
						p  = p[0] * scale,
						dp = 0E0
					};
					yield return val;
					
					for (int k = 1; k <= n; k++)
					{
						val.n = k;
						val.p = p[k] * scale;
						val.dp = (x * p[k-1] - k * p[k]) * scale;
						
						yield return val;
					}
				}
			}
			else
			{
				// use approximation formula for large x>>n
				// j_n(z>>n) = 1/z sin(z - 0.5*pi*n)
				Value val = new Value {
					n  = 0,
					p  = 0E0,
					dp = 0E0
				};
				
    			for (int k=0; k<=n; k++)
				{
			    	double a  = 0.5E0 * (k + 1) * System.Math.PI;
			    	double p = val.p;
					
					val.n  = k;
					val.p  = Math.Cos(x - a) / x;;
					val.dp = x * p - k * val.p;
					
					yield return val;
				}
			}
		}
		
		public static void j1(int n, double x, ref double [] p, ref double [] dp)
		{
			double xm = System.Math.Abs(x);
			
			if (xm < SphBessel.ZeroSinXX)
			{
				// use approximation formula for small x<<1
				// j_n(z->0) = z^n / (2n+1)!!
				p[0]  = 1E0;
				dp[0] = 0E0;

				for (int k=1; k<=n; k++)
				{
					p[k]  = p[k-1] * x / (2*k+1);
					dp[k] = p[k-1] * k / (2*k+1);
				}
			}
			else if (xm >= SphBessel.ZeroSinXX && xm < SphBessel.MaxArgBes)
			{
				// use forward recurrence for the first two orders
				if (n<2)
				{
				    p[0] = System.Math.Sin(x) / x;
					if (n >= 1) p[1] = p[0] / x - System.Math.Cos(x) / x;
				}
				else
				// otherwise use backward recurrence relation for the functions
				{
					// get order where Bessel function becomes zero (due to restriction representation of float numbers)
					int Ma = (int)(xm + 4 * System.Math.Pow(xm, 1/3E0) + 2 + System.Math.Sqrt(101 + xm)) + 20;
					int Mn = n + (int)(System.Math.Sqrt(SphBessel.FactNBes * n));
					int M  = System.Math.Max(Ma,Mn);
					double f2 = 0E0;
					double f1 = SphBessel.InitBesVal;
					
					// store two first order
					double j0 = p[0];
					double j1 = p[1];
					
					// downward recurrence
					for (int k=M; k>=0; k--)
					{
        				double f  = (2*k + 3) * f1 / x - f2;
						f2 = f1;
						f1 = f;
						if (k <= n) p[k] = f;
						
						// temporary rescale
						if (System.Math.Abs(f1) > SphBessel.UpperBoundSeq)
						{
							f2 = f2 * SphBessel.LowerBoundSeq;
							f1 = f1 * SphBessel.LowerBoundSeq;
          					for (int l=k; l<=n; l++) p[l] *= LowerBoundSeq;
						}
						else if (System.Math.Abs(f1) < SphBessel.LowerBoundSeq)
						{
							f2 = f2 * SphBessel.UpperBoundSeq;
							f1 = f1 * SphBessel.UpperBoundSeq;
          					for (int l=k; l<=n; l++) p[l] *= UpperBoundSeq;
						}
					}
					// rescale computed values
					double scale = System.Math.Abs(f1) > System.Math.Abs(f2) ? j0 / f1 : j1 / f2;
					for (int k=0; k<=n; k++) p[k] *= scale;
				}
				
				// compute their derivations
				dp[0] = 0E0;
				for (int k=1; k<=n; k++) dp[k] = x * p[k-1] - k * p[k];
			}
			else
			{
				// use approximation formula for large x>>n
				// j_n(z>>n) = 1/z sin(z - 0.5*pi*n)
    			for (int k=0; k<=n; k++)
				{
			      double a  = 0.5E0 * (k + 1) * System.Math.PI;
			      p[k] = System.Math.Cos(x - a) / x;
				}
				// compute the derivations
				dp[0] = 0E0;
				for (int k=1; k<n; k++) dp[k] = x * p[k-1] - k * p[k];
			}
		}
		
		/**
		 * spherical Neumann function y_n(x)
		 */
		public static void y(int n, double x, ref double [] p, ref double [] dp)
		{
/*			double xm = System.Math.Abs(x);

			if (xm < SphBessel.ZeroSinXX)
			{
				// use approximation formula for small x<<1
				for (int k=0; k<=n; k++)
				{
					p[k]  = 0E0;
					dp[k] = 0E0;
				}
			}
			else if (xm >= SphBessel.ZeroSinXX && xm < SphBessel.MaxArgBes)
			{
				// first compute Bessel functions
				j(n, x, ref p, ref dp);
			
				// further compute Neumann functions from them
				double f2 = p[0];
				double f1 = p[1];
				double x2 = x*x, x3 = x2*x;
				
				p[0] = - Math.Cos(x) / x;
    			p[1] = (p[0] - Math.Sin(x)) / x;
				
				for (int k=2; k<=n; k++)
				{
					double f = p[k];
					
	        		if (Math.Abs(f1) > Math.Abs(f2))
		          		p[k] = (f * p[k-1] - 1 / x2) / f1;
					else
		          		p[k] = (f * p[k-2] - (2*k-1) / x3) / f2;
					
					f2 = f1;
					f1 = f;
				}
			}
			else
			{
				// use approximation formula for large x>>1
    			for (int k=0; k<=n; k++)
				{
			      double a  = 0.5E0 * (k + 1) * Math.PI;
			      p[k] = Math.Sin(x - a) / x;
				}
				// compute the derivations
				dp[0] = 0E0;
				for (int k=1; k<n; k++) dp[k] = x * p[k-1] - k * p[k];
			}			*/
		}
	}
}

