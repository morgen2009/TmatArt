using System;
using NUnit.Framework;
using TmatArt.Numeric.Polynomial;
using TmatArt.Numeric;

namespace TmatArt.Scattering.Basis
{
	[TestFixture()]
	[Ignore("Class Scattering.Basis.Angular is not implemented")]
	public class BasisTest
	{
		[Test()]
		public void testLegendreNorm ()
		{
			double theta = 0.45;
			int nrank = 5;
			int mrank = 2;
			Legendre pol = new Legendre(mrank);				
			foreach (Angular.Value<double> val in Angular.compute(theta, nrank, mrank))
			{
				Legendre.Value val1 = pol.computeOne(System.Math.Cos(theta), val.n);
				val1.p *= 1.0 / pol.norm(val.n) * (-1.0).Pow(mrank);
				Console.WriteLine(String.Format("{0} {1} {2}", val.n, val.p, val1.p));
				Assert.AreEqual(val1.p, val.p, 1E-10);
			}
			//Assert.Fail();
		}
		[Test()]
		public void testAngular ()
		{
			double theta = 0.45;
			int nrank = 5;
			int mrank = 1;
			Legendre pol = new Legendre(mrank);				
			foreach (Angular.Value<double> val in Angular.compute(theta, nrank, mrank))
			{
				Legendre.Value val1 = pol.computeOne(System.Math.Cos(theta), val.n);
				val1.dp *= -1.0 / pol.norm(val.n) * (-1.0).Pow(mrank) * System.Math.Sin(theta);
				Console.WriteLine(String.Format("{0} {1} {2}", val.n, val.tau, val1.dp));
				Assert.AreEqual(val1.dp, val.tau, 1E-10);
			}
			//Assert.Fail();
		}
	}
}

