using System;
using System.Linq;
using TmatArt.Numeric;

namespace TmatArt.Scattering.Basis
{
	public class Angular
	{
		public class Value<T>
		{
			public int n;
			public T p, tau, pi;
		}
		
		public static System.Collections.Generic.IEnumerable<Value<double>> compute (double theta, int n, int m)
		{
			// check input
			if (n < 0)
				throw new ArgumentOutOfRangeException("Wrong: n < 0");
			if (m < 0)
				throw new ArgumentOutOfRangeException("Wrong: m < 0");
			
			double cos = System.Math.Cos(theta);
			double sin = System.Math.Sin(theta);
			if (m == 0)
			{
				/**
				 * pi_n^0  = 0
				 * tau_n^0 = -sin(theta) * P'_n(cos(theta))
				 */
				double p1 = System.Math.Sqrt(2)/2, p2 = 0;
				
				// n = 0
				Value<double> val = new Value<double> {
					n   = 0,
					p   = p1,
					pi  = 0,
					tau = 0
				};
				yield return val;
				if (n == 0) yield break;
				
				// n > 0
				double j3 = 0, j2 = 1, j1 = 1;
				for (int j = 1; j <= n; j++)
				{
					j3 = j2;
					j2 = j1;
					j1 = System.Math.Sqrt(2*j+1);
					
					double pp = j1 / j * (j2 * cos * p1 - (j-1) / j3 * p2);
					val.n   = j;
					val.p   = pp;
					val.tau = j1 / j2*( -sin*j*p1 + cos*val.tau);
					yield return val;
					
					p2 = p1;
					p1 = pp;
				}
			}
			else
			{
				/**
				 * pi_n^m  recurrence
				 * tau_n^0 recurrence
				 */
				
				// n = m
				// pi_m^m(\theta) = \sqrt(2m+1/2/(2m!)) * (2m-1)!! sin^{m-1}(\theta)
				double p1 = 1, p2 = 0;
				
				p1 = (2*m+1) / 2.0;
				for (int j=1; j<=m; j++) p1 *= (1 - 0.5/j);
				p1 = System.Math.Sqrt(p1) * sin.Pow(m - 1);
				
				Value<double> val = new Value<double> {
					n   = m,
					p   = p1 * sin,
					pi  = p1,
					tau = m * cos * p1
				};
				yield return val;
				if (n == 0) yield break;

				// n > m
				double j3 = 0, j2 = 1, j1 = System.Math.Sqrt(2*m+1);
				double i2 = 0, i1 = 0;
				for (int j = m+1; j <= n; j++)
				{
					j3 = j2; // <- \sqrt(2n-3)
					j2 = j1; // <- \sqrt(2n-1)
					j1 = System.Math.Sqrt(2*j+1); // <- \sqrt(2n+1)
					
					i2 = i1; // <- \sqrt((n-1-m)(n-1+m))
					i1 = System.Math.Sqrt((j-m)*(j+m)); // <- \sqrt((n-m)(n+m))
					//Console.WriteLine(String.Format("{0} {1} {2}", j, j1.Pow(2), i1.Pow(2)));
					double pp = j1 / i1 * (j2 * cos * p1 - i2 / j3 * p2);
					val.n   = j;
					val.p   = pp * sin;
					val.pi  = pp;
					val.tau = j * cos * pp - j1 * i1 / j2 * p1;
					yield return val;
					
					p2 = p1;
					p1 = pp;
				}
			}
		}
	}
}

