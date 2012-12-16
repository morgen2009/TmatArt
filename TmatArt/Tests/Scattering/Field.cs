using System;
using NUnit.Framework;
using TmatArt.Scattering.Field;
using TmatArt.Scattering.Field.Operation;
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
		}

		[Test()]
		public void PlaneWaveE_H()
		{
			// arrange
			double deg = System.Math.PI / 180;
			PlaneWave field = new PlaneWave(46*deg, 12*deg, PlaneWave.Polarization.HORIZONTAL);
			field.wave   = new WaveLength(1.0);
			field.medium = new Isotrop(1.3);
			
			// act
			Vector3d p0 = new Vector3d(0, 0, 0);
			Euler e = new Euler(field.phi, field.beta, 0);
			Vector3d p1 = new Vector3d(0, 0, 1).Rotate(-e) / field.medium.index.re;
			Vector3c e0 = field.NearE(p0);
			Vector3c e1 = field.NearE(p1);
			
			// assert
			AssertComplexExtension.AreEqual(e0.x, e1.x, 1E-7, "Ex");
			AssertComplexExtension.AreEqual(e0.y, e1.y, 1E-7, "Ey");
			AssertComplexExtension.AreEqual(e0.z, e1.z, 1E-7, "Ez");
		}
		
		[Test()]
		public void PlaneWaveE_V()
		{
			// arrange
			double deg = System.Math.PI / 180;
			PlaneWave field = new PlaneWave(46*deg, 12*deg, PlaneWave.Polarization.VERTICAL);
			field.wave   = new WaveLength(1.0);
			field.medium = new Isotrop(1.0);
			
			// act
			Vector3d p0 = new Vector3d(0, 0, 0);
			Euler e = new Euler(field.phi, field.beta, 0);
			Vector3d p1 = new Vector3d(0, 0, 1).Rotate(-e) / field.medium.index.re;
			Vector3c e0 = field.NearE(p0);
			Vector3c e1 = field.NearE(p1);
			
			// assert
			AssertComplexExtension.AreEqual(e0.x, e1.x, 1E-7, "Ex");
			AssertComplexExtension.AreEqual(e0.y, e1.y, 1E-7, "Ey");
			AssertComplexExtension.AreEqual(e0.z, e1.z, 1E-7, "Ez");
		}
		
		[Test()]
		public void TestReflect ()
		{
			double deg = System.Math.PI / 180;
			PlaneWave field = new PlaneWave(45*deg, 0, PlaneWave.Polarization.CIRCULAR_R);
			field.wave   = new WaveLength(628.3);
			field.medium = new Isotrop(1.3);

			// test
			PlaneWave reflect = field.Operation<IReflectOperation>().Reflect(new Halfspace(1), new Isotrop(1.5)) as PlaneWave;

			// Snell's law
			Assert.AreEqual(System.Math.Sin(field.beta)*field.medium.index, System.Math.Sin(reflect.beta)*reflect.medium.index);
			
			// Brewster's angle
			
			// Check continous energy by transmission
			PlaneWave transmit = field.Operation<IReflectOperation>().Transmit(new Halfspace(1), new Isotrop(1.5)) as PlaneWave;
			Vector3d point = new Vector3d(0, 0, 1);
			double before = field.NearE(point).Length().re;
			double after  = (reflect.NearE(point) + transmit.NearE(point)).Length().re;
			Assert.AreEqual(before, after);
			
			// Total internal reflection (special class) ???
		}
	}
}

