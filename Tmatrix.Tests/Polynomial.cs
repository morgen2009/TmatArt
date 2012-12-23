using System;
using NUnit.Framework;
using System.Linq;
using System.Collections.Generic;

namespace TmatArt.Numeric
{
	[TestFixture()]
	public class PolynomialTest
	{
		[Test()]
		public void Legendre ()
		{
			double[] x = {-0.5, -0.25, 0, 0.25, 0.5, 0.75, 1};
			int n = 4;
			int m = -1;

			Numeric.Polynomial.Legendre pol = new Numeric.Polynomial.Legendre(m);
			Func<double, double> exact = pol.exact(n);
			
			Assert.IsTrue(this.compareFunctions(x, delegate(double y) {
				return pol.computeOne(y, n).p;
			}, delegate(double y) {
				return exact(y);
			}));
		}
		
		[Test()]
		public void LegendreD ()
		{
			double[] x = {-1+1E-7, -0.5, 0, 0.25, 0.5, 0.75, 1-1E-7};
			int n = 4;
			int m = 1;

			// function
			Numeric.Polynomial.Legendre pol = new Numeric.Polynomial.Legendre(m);
			Func<double, double> exact = pol.exact_d(n);
			
			Assert.IsTrue(this.compareFunctions(x, delegate(double y) {
				return pol.computeOne(y, n).dp;
			}, delegate(double y) {
				return exact(y);
			}));
			//Assert.Fail();
		}
		
		[Test()]
		public void LegendreRoots ()
		{
			Numeric.Polynomial.Legendre pol = new Numeric.Polynomial.Legendre();
			
			double[] r = {
				0,
				-0.577350269189626, 0.577350269189626,
				-0.774596669241483, 0, 0.774596669241483,
				-0.861136311594053, -0.339981043584856, 0.339981043584856, 0.86113631159405,
				-0.906179845938664, -0.538469310105683, 0, 0.538469310105683, 0.90617984593866,
				-0.932469514203152, -0.661209386466265, -0.238619186083197, 0.238619186083197, 0.661209386466265, 0.932469514203152
			};
			
			int n = 1;
			for (int i=0; i<r.Length; i+=n, n++)
			{
				Console.WriteLine(String.Format("Roots of polynom #{0}", n));
				Assert.IsTrue(pol.roots(n).OrderBy(x=>x).Intersect(r.Skip(i).Take(n), new DoubleEqualityComparer(1E-8)).Count() == n);
			}
		}
		
		[Test()]
		public void Laguerre ()
		{
			double[] x = {-0.5, -0.25, 0, 0.25, 0.5, 0.75, 1};
			int n = 3;
			double alpha = 0.5;
			
			Numeric.Polynomial.Laguerre pol = new Numeric.Polynomial.Laguerre(alpha);
			Func<double, double> exact = pol.exact(n);
			
			Assert.IsTrue(this.compareFunctions(x, delegate(double y) {
				return pol.computeOne(y, n).p;
			}, delegate(double y) {
				return exact(y);
			}));
		}
		
		[Test()]
		public void LaguerreD ()
		{
			double[] x = {-0.5, -0.25, 0, 0.25, 0.5, 0.75, 1};
			int n = 3;
			double alpha = 0.5;
			
			Numeric.Polynomial.Laguerre pol = new Numeric.Polynomial.Laguerre(alpha);
			Func<double, double> exact = pol.exact_d(n);
			
			Assert.IsTrue(this.compareFunctions(x, delegate(double y) {
				return pol.computeOne(y, n).dp;
			}, delegate(double y) {
				return exact(y);
			}));
		}
		
		[Test()]
		public void LeguerreRoots ()
		{
			double alpha = 0.5;
			int[] ncase = { 1, 2, 3, 4 };
			
			Numeric.Polynomial.Laguerre pol = new Numeric.Polynomial.Laguerre(alpha);
			foreach (int n in ncase)
			{
				int cnt = 0;
				Console.WriteLine(String.Format("Roots of polynom #{0}", n));
				foreach (double x in pol.roots(n).Distinct(new DoubleEqualityComparer(1E-7)))
				{
					Console.WriteLine(x);
					Assert.AreEqual(pol.computeOne(x, n).p, 0, 1E-7, "Root value is wrong");
					cnt++;
				}
				Assert.AreEqual(cnt, n, "Wrong number of roots");
			}
		}
		
		[Test()]
		public void ArbitraryLegendre ()
		{
			int n = 5;
			Numeric.Polynomial.Arbitrary pol1 = new Numeric.Polynomial.Arbitrary(-1, 1, n, x=>1, 100);
			Numeric.Polynomial.Legendre pol2 = new Numeric.Polynomial.Legendre(0);
			
			Console.WriteLine(String.Format("Legendgre polynomial roots, n={0}", n));
			foreach (double x in pol2.roots(n).OrderBy(x=>x))
				Console.WriteLine(x);
			
			Console.WriteLine(String.Format("Arbitrary polynomial roots, n={0}", n));
			foreach (double x in pol1.roots(n).OrderBy(x=>x))
				Console.WriteLine(x);
			
			Assert.AreEqual(pol1.roots(n).Intersect(pol2.roots(n), new DoubleEqualityComparer(1E-7)).Count(), n);
		}
		
		[Test()]
		public void ArbitraryLaguerre ()
		{
			int n = 4;
			double a = 0.25;
			Numeric.Polynomial.Arbitrary pol1 = new Numeric.Polynomial.Arbitrary(0, 100, n, x=>System.Math.Pow(x, a)*System.Math.Exp(-x), 1000);
			Numeric.Polynomial.Laguerre pol2 = new Numeric.Polynomial.Laguerre(a);
			
			Console.WriteLine(String.Format("Laguerre polynomial roots, n={0}", n));
			foreach (double x in pol2.roots(n).OrderBy(x=>x))
				Console.WriteLine(x);
			
			Console.WriteLine(String.Format("Arbitrary polynomial roots, n={0}", n));
			foreach (double x in pol1.roots(n).OrderBy(x=>x))
				Console.WriteLine(x);
			
			Assert.AreEqual(pol1.roots(n).Intersect(pol2.roots(n), new DoubleEqualityComparer(1E-4)).Count(), n);
		}
		
		[Test()]
		public void ArbitrarySelf ()
		{
			int[] ncase = { 1, 2, 3, 4 };
			
			foreach (int n in ncase)
			{
				Numeric.Polynomial.Arbitrary pol = new Numeric.Polynomial.Arbitrary(0, 1, n, x=>x*x, 100);
				int cnt = 0;
				Console.WriteLine(String.Format("Roots of polynom #{0}", n));
				foreach (double x in pol.roots(n).Distinct(new DoubleEqualityComparer(1E-7)))
				{
					Console.WriteLine(x);
					Assert.AreEqual(pol.computeOne(x, n).p, 0, 1E-7, "Root value is wrong");
					cnt++;
				}
				Assert.AreEqual(cnt, n, "Wrong number of roots");
			}
			//Assert.Fail();
		}
		
		private bool compareFunctions(double [] arg, Func<double,double>func1, Func<double,double>func2)
		{
			Console.WriteLine(String.Format("Argument\t Func1 \t Func2"));
			bool isEqual = true;
			foreach (double x in arg)
			{
				double f1 = func1(x);
				double f2 = func2(x);
				Console.WriteLine(String.Format("{0}\t {1}\t {2}\t {3}", x, f1, f2, f1/f2));
				double max = System.Math.Max(System.Math.Abs(f1), System.Math.Abs(f2));
				double res = System.Math.Abs(f1-f2);
				isEqual = isEqual && (res < 1E-5 || res / max < 1E-5);
			}
			return isEqual;
		}	
	}
	
	public static class PolynomialLegendreExtension
	{
		public static Func<double, double> exact(this Numeric.Polynomial.Legendre obj, int n)
		{
			int m = obj.m;
			switch (m)
			{
			case 0 : switch (n)
				{
				case 0  : return x => 1; break;
				case 1  : return x => x; break;
				case 2  : return x => (3*x*x-1)/2; break;
				case 3  : return x => (5*x*x*x-3*x)/2; break;
				case 4  : return x => (35*x*x*x*x-30*x*x+3)/8; break;
				default : return x => 0; break;
				};
				break;
			case 1 : switch (n)
				{
				case 1  : return x => -System.Math.Sqrt(1-x*x); break;
				case 2  : return x => -3*x*System.Math.Sqrt(1-x*x); break;
				case 3  : return x => -3.0/2*(5.0*x*x-1)*System.Math.Sqrt(1-x*x); break;
				case 4  : return x => -5.0/2*(7*x*x*x-3*x)*System.Math.Sqrt(1-x*x); break;
				default : return x => 0; break;
				};
				break;
			case -1 : switch (n)
				{
				case 1  : return x => 1.0/2*System.Math.Sqrt(1-x*x); break;
				case 2  : return x => 1.0/2*x*System.Math.Sqrt(1-x*x); break;
				case 3  : return x => 1.0/8*(5.0*x*x-1)*System.Math.Sqrt(1-x*x); break;
				case 4  : return x => 1.0/8*(7*x*x*x-3*x)*System.Math.Sqrt(1-x*x); break;
				default : return x => 0; break;
				};
				break;
			default: return x => 0;
			}
		}
		
		public static Func<double, double> exact_d(this Numeric.Polynomial.Legendre obj, int n)
		{
			int m = obj.m;
			switch (m)
			{
			case 0 : switch (n)
				{
				case 0  : return x => 0; break;
				case 1  : return x => 1; break;
				case 2  : return x => 3*x; break;
				case 3  : return x => (15*x*x-3)/2; break;
				case 4  : return x => (35*4*x*x*x-30*2*x)/8; break;
				default : return x => 0; break;
				};
				break;
			case 1 : switch (n)
				{
				case 1  : return x => x/System.Math.Sqrt(1-x*x); break;
				case 2  : return x => (6*x*x-3)/System.Math.Sqrt(1-x*x); break;
				case 3  : return x => 3.0/2*(15.0*x*x*x-11*x)/System.Math.Sqrt(1-x*x); break;
				case 4  : return x => 5.0/2*(28*x*x*x*x-27*x*x+3)/System.Math.Sqrt(1-x*x); break;
				default : return x => 0; break;
				};
				break;
			default: return x => 0;
			}
		}
	}

	public static class PolynomialLaguerreExtension
	{
		public static Func<double, double> exact(this Numeric.Polynomial.Laguerre obj, int n)
		{
			double a = obj.alpha;
			switch (n)
			{
			case 0: return x=>1; break;
			case 1: return x=>-x+a+1; break;
			case 2: return x=>x*x/2-(a+2)*x+(a+2)*(a+1)/2; break;
			case 3: return x=>-x*x*x/6+(a+3)*x*x/2-(a+3)*(a+2)*x/2+(a+3)*(a+2)*(a+1)/6; break;
			default: return x=>0;
			}
		}
		
		public static Func<double, double> exact_d(this Numeric.Polynomial.Laguerre obj, int n)
		{
			double a = obj.alpha;
			switch (n)
			{
			case 0: return x=>0; break;
			case 1: return x=>-1; break;
			case 2: return x=>x-(a+2); break;
			case 3: return x=>-x*x/2+(a+3)*x-(a+3)*(a+2)/2; break;
			default: return x=>0;
			}
		}
	}

}

