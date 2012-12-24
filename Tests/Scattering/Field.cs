using System;
using NUnit.Framework;
using TmatArt.Scattering.Field.Operation;
using TmatArt.Scattering.Medium;
using TmatArt.Numeric.Mathematics;
using TmatArt.Geometry.Region;
using TmatArt.Geometry;

namespace TmatArt.Scattering.Field
{
	[TestFixture()]
	public class FieldTest
	{
		[Test()]
		public void Initialization ()
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
		public void ContinousEnergy ([Values(0, -1000, 1000)]double z, [Values(45, 60)]double beta, [Values(0,1,2,3)]PlaneWave.Polarization pol)
		{
			// arrange
			double deg = System.Math.PI / 180;
			PlaneWave field = new PlaneWave(beta*deg, 0, pol);
			field.wave   = new WaveLength(628.3);
			field.medium = new Isotrop(1.0);
			Isotrop medium2 = new Isotrop(1.5);
			double z0 = z;

			// act
			PlaneWave reflect  = field.Operation<IReflectOperation>().Reflect(new Halfspace(z0), medium2) as PlaneWave;
			PlaneWave transmit = field.Operation<IReflectOperation>().Transmit(new Halfspace(z0), medium2) as PlaneWave;

			// assert
				// Snell's law
			AssertComplexExtension.AreEqual(System.Math.Sin(field.beta)*field.medium.index, System.Math.Sin(transmit.beta)*transmit.medium.index, 1E-3, "Snell");
			
				// Continous energy transmission
			Vector3d point = new Vector3d(0, 0, z0);
			Vector3c f1 = field.NearE(point);
			Vector3c fr = reflect.NearE(point);
			Vector3c ft = transmit.NearE(point);
			double f = Math.Sqrt(Math.Cos(field.beta) / Math.Cos(transmit.beta));

			Console.WriteLine(String.Format("{0}->{1} and {2}", field.beta/deg, reflect.beta/deg, transmit.beta/deg));

			Complex before = f1*f1;
			Complex after  = fr*fr + ft*ft*medium2.index.re/field.medium.index.re/f/f;
			AssertComplexExtension.AreEqual(before, after, 1E-3, "Continuos");
		}

		[Test()]
		public void ContinousTangential ([Values(0, -1000, 1000)]double z, [Values(45, 60)]double beta, [Values(0,1,2,3)]PlaneWave.Polarization pol)
		{
			// arrange
			double deg = System.Math.PI / 180;
			PlaneWave field = new PlaneWave(beta*deg, 0, pol);
			field.wave   = new WaveLength(628.3);
			field.medium = new Isotrop(1.0);
			Isotrop medium2 = new Isotrop(1.5);
			double z0 = z;
			
			// act
			PlaneWave reflect  = field.Operation<IReflectOperation>().Reflect(new Halfspace(z0), medium2) as PlaneWave;
			PlaneWave transmit = field.Operation<IReflectOperation>().Transmit(new Halfspace(z0), medium2) as PlaneWave;
			
			// assert
			Vector3d point = new Vector3d(0, 0, z0);
			Vector3c n = new Vector3c(0, 0, 1);

			//double f = Math.Sqrt(Math.Cos(field.beta) / Math.Cos(transmit.beta));
			Vector3c f1 = (field.NearE(point) ^ n);
			Vector3c fr = (reflect.NearE(point) ^ n);
			Vector3c ft = (transmit.NearE(point) ^ n);

			Console.WriteLine(String.Format("{0} : {1} : {2}", f1.Length().re, fr.Length().re, ft.Length().re));
			Assert.AreEqual((f1 + fr - ft).Length().re, 0, 1E-4);
			//Assert.Fail ();

			/*
			Vector3c f1 = field.NearE(point) ^ n;
			Vector3c fr = reflect.NearE(point) ^ n;
			Vector3c ft = transmit.NearE(point) ^ n;

			Console.WriteLine(String.Format("{0}:{1}", f1.y.re, f1.y.im));
			Console.WriteLine(String.Format("{0}:{1}", fr.y.re, fr.y.im));
			Console.WriteLine(String.Format("{0}:{1}", ft.y.re, ft.y.im));

			Console.WriteLine(String.Format("f={0}", f));
			Vector3c diff = f1 + fr - ft * f;
			AssertComplexExtension.AreEqual(diff.x, Complex.ZERO, 1E-3, "diff_x");
			AssertComplexExtension.AreEqual(diff.y, Complex.ZERO, 1E-3, "diff_y");
			AssertComplexExtension.AreEqual(diff.z, Complex.ZERO, 1E-3, "diff_z");*/
		}
	}
}

