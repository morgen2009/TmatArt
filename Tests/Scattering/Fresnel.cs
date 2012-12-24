using System;
using NUnit.Framework;
using TmatArt.Numeric.Mathematics;

namespace TmatArt.Scattering
{
	[TestFixture()]
	public class FresnelTest
	{
		[TestCase(1.0, 1.5, 45, TestName = "m_r=1,5; b=45")]
		[TestCase(1.5, 1.0, 45, TestName = "m_r=1/1,5; b=45")]
		public void RAndT_ShouldBeEqual (double mediumIn, double mediumOut, double betaIn)
		{
			// arrange
			double deg = System.Math.PI / 180;
			Fresnel.Coefficients c = Fresnel.Compute(betaIn*deg, mediumIn, mediumOut);
			Assume.That(c.thetaOut.im == 0, "Total internal reflection");

			// assert
			double reflectance   = System.Math.Pow(Complex.Math.Abs(c.rp),2) + System.Math.Pow(Complex.Math.Abs(c.rs),2);
			double transmittance = System.Math.Pow(Complex.Math.Abs(c.tp),2) + System.Math.Pow(Complex.Math.Abs(c.ts),2);
			double norm = Complex.Math.Abs(Complex.Math.Cos(c.thetaOut) / Complex.Math.Cos(c.thetaIn) * mediumOut / mediumIn);
			Assert.AreEqual(2, reflectance + norm*transmittance, 1E-4);
		}
	}
}

