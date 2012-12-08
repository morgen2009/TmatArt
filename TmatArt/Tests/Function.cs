using System;
using NUnit.Framework;
using TmatArt.Numeric;
using TmatArt.Numeric.Function;
using System.Linq;

namespace TmatArt.Test
{
	[TestFixture()]
	public class TestFunction
	{
		[Test()]
		public void testSphBesselJ ()
		{
			double[] x = {1E-10, 1, 2, 3};
			int n = 1;

			Func<double, double> bessel_j = SphBesselExtension.exact_dj(n);
			
			Assert.IsTrue(this.compareFunctions(x, delegate(double y) {
//				return SphBessel.j(y, n).Where(v => v.n == n).First().dp;
				double[] p  = new double[n+1];
				double[] dp = new double[n+1];
				SphBessel.j1(n, y, ref p, ref dp);
				return dp[n];
			}, delegate(double y) {
				return bessel_j(y);
			}));
		}
		
		private bool compareFunctions(double [] arg, Func<double,double>func1, Func<double,double>func2)
		{
			Console.WriteLine(String.Format("Argument\t Func1 \t Func2"));
			bool isEqual = true;
			foreach (double x in arg)
			{
				double f1 = func1(x);
				double f2 = func2(x);
				Console.WriteLine(String.Format("{0}\t {1}\t {2}", x, f1, f2));
				isEqual = isEqual && System.Math.Abs(f1-f2) < 1E-14;
			}
			isEqual = false; // DEBUG
			return isEqual;
		}
	}
	
	public static class SphBesselExtension
	{
		public static Func<double, double> exact_j(int n)
		{
			switch (n)
			{
			case 0  : return y => System.Math.Sin(y) / y; break;
			case 1  : return y => System.Math.Sin(y)/y * ( 1/y ) + System.Math.Cos(y)/y * ( -1 ); break;
			case 2  : return y => System.Math.Sin(y)/y * ( 3/y/y-1 ) + System.Math.Cos(y)/y * ( -3/y ); break;
			case 3  : return y => System.Math.Sin(y)/y * ( 15/y/y/y - 6/y ) + System.Math.Cos(y)/y * ( -15/y/y + 1 ); break;
			default : return y => 0; break;
			}
		}
		
		public static Func<double, double> exact_dj(int n)
		{
			switch (n)
			{
			case 0  : return y => System.Math.Cos(y)/y * ( 1 ) + System.Math.Sin(y)/y * ( -1/y ); break;
			case 1  : return y => System.Math.Cos(y)/y * ( 2/y ) + System.Math.Sin(y)/y * ( 1-2/y/y ); break;
			default : return y => 0; break;
			}
		}
	}
}

