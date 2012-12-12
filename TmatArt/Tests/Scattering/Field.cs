using System;
using NUnit.Framework;
using TmatArt.Scattering.Field;
using TmatArt.Scattering.Medium;
using TmatArt.Scattering;
using TmatArt.Numeric.Mathematics;
using TmatArt.Tests.Numeric;
using TmatArt.Geometry.Region;
using TmatArt.Geometry;

namespace TmatArt.Tests.Scattering
{
	[TestFixture()]
	public class TestField
	{
		[Test()]
		public void TestCase ()
		{
			double deg = System.Math.PI / 180;

			// constructor
			PlaneWave field = new PlaneWave(45*deg, 0, PlaneWave.Polarization.CIRCULAR_R);
			field.wave   = new WaveLength(628.3);
			field.medium = new Isotrop(1.3);

			AssertComplexExtension.AreEqual(new Complex(1E0, 0E0), field.ex, 1E-6, "Ex");
			AssertComplexExtension.AreEqual(new Complex(0E0, 1E0), field.ey, 1E-6, "Ey");

			// Snell's law
			PlaneWave reflect = (PlaneWave)field.factory().Reflect(new Halfspace(1), new Isotrop(1.5));
			Assert.AreEqual(System.Math.Sin(field.beta)*field.medium.index, System.Math.Sin(reflect.beta)*reflect.medium.index);

			// Brewster's angle

			// Check continous energy by transmission
			PlaneWave transmit = (PlaneWave)field.factory().Transmit(new Halfspace(1), new Isotrop(1.5));
			Vector3d point = new Vector3d(0, 0, 1);
			double before = field.NearE(point).Length().re;
			double after  = (reflect.NearE(point) + transmit.NearE(point)).Length().re;
			Assert.AreEqual(before, after);

			// Total internal reflection (special class) ???
		}
	}
}

