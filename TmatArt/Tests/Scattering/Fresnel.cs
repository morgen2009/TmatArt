using System;
using NUnit.Framework;
using TmatArt.Scattering;

namespace TmatArt.Tests.Scattering
{
	[TestFixture()]
	public class TestFresnel
	{
		[Test()]
		public void ReflectanceTransmittance ()
		{
			// arrange
			double deg = System.Math.PI / 180;
			double m1 = 1.0;
			double m2 = 1.5;
			Fresnel.Coefficients c = Fresnel.Compute(m1, m2, 45*deg);

			// assert
			double reflectance   = c.rp*c.rp + c.rs*c.rs;
			double transmittance = c.tp*c.tp + c.ts*c.ts;
			double norm = Math.Cos(c.theta2) / Math.Cos(c.theta1) * m2 / m1;
			Assert.AreEqual(2, reflectance + norm*transmittance, 1E-4);
		}
	}
}

