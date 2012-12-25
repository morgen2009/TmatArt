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
	public class PlaneWaveTest
	{
		double deg = System.Math.PI / 180;

		[Test]public void Initialization ()
		{
			PlaneWave field = new PlaneWave(45*deg, 0, PlaneWave.Polarization.CIRCULAR_R);
			field.wave   = new WaveLength(628.3);
			field.medium = new Isotrop(1.3);

			Assert.That(field.ex, new ComplexConstraint(new Complex(1, 0)));
			Assert.That(field.ey, new ComplexConstraint(new Complex(0, 1)));
		}

		[Test]public void ShouldBeSamePhase([Values(0,1,2,3)]PlaneWave.Polarization pol)
		{
			// arrange
			PlaneWave field = new PlaneWave(46*deg, 12*deg, pol);
			field.wave   = new WaveLength(1.0);
			field.medium = new Isotrop(1.3);
			
			// act
			Vector3d p0 = new Vector3d(0, 0, 0);
			Euler e = new Euler(field.phi, field.beta.re, 0);
			Vector3d p1 = new Vector3d(0, 0, 1).Rotate(-e) / field.medium.index.re;
			Vector3c e0 = field.NearE(p0);
			Vector3c e1 = field.NearE(p1);
			
			// assert
			Assert.That(e1, new Vector3dConstraint(e0));
		}
		
		[Test]public void SnellLaw ([Values(0, 1000)]double z, [Values(45, 60)]double beta)
		{
			// arrange
			PlaneWave field = new PlaneWave(beta*deg, 0, PlaneWave.Polarization.VERTICAL);
			field.wave   = new WaveLength(628.3);
			field.medium = new Isotrop(1.0);
			Isotrop medium2 = new Isotrop(1.5);
			double z0 = z;
			
			// act
			PlaneWave transmit = field.Operation<IReflectOperation>().Transmit(new Halfspace(z0), medium2) as PlaneWave;
			
			// assert
			Assert.That(System.Math.Sin(transmit.beta.re)*transmit.medium.index, new ComplexConstraint(System.Math.Sin(field.beta.re)*field.medium.index));
		}

		[Test]public void ContinousEnergy ([Values(0, 1000)]double z, [Values(45, 60)]double beta, [Values(0,1,2,3)]PlaneWave.Polarization pol)
		{
			// arrange
			PlaneWave field = new PlaneWave(beta*deg, 0, pol);
			field.wave   = new WaveLength(628.3);
			field.medium = new Isotrop(1.0);
			Isotrop medium2 = new Isotrop(1.5);
			double z0 = z;

			// act
			PlaneWave reflect  = field.Operation<IReflectOperation>().Reflect(new Halfspace(z0), medium2) as PlaneWave;
			PlaneWave transmit = field.Operation<IReflectOperation>().Transmit(new Halfspace(z0), medium2) as PlaneWave;

			Vector3d point = new Vector3d(0, 0, z0);
			Vector3c f1 = field.NearE(point);
			Vector3c fr = reflect.NearE(point);
			Vector3c ft = transmit.NearE(point);
			double norm = Math.Cos(transmit.beta.re) / Math.Cos(field.beta.re) * transmit.medium.index.re / field.medium.index.re;

			Complex before = f1*f1;
			Complex after  = fr*fr + ft*ft*norm;

			// assert
			Console.WriteLine(String.Format("{0}->{1} and {2}", field.beta/deg, reflect.beta/deg, transmit.beta/deg));
			Assert.That(before, new ComplexConstraint(after));
		}

		[Test]public void TangentialComponents (
			[Values(0, 1)] double mr_im,
			[Values(0, 1000)] double z,
			[Values(45, 60)] double beta,
			[Values(0,1,2,3)] PlaneWave.Polarization pol)
		{
			// TODO Further validations are needed (when angle is complex and evanescent wave and tow absorbing media)
			// arrange
			PlaneWave field = new PlaneWave(beta*deg, 0, pol);
			field.wave   = new WaveLength(628.3);
			field.medium = new Isotrop(1.0);
			Isotrop medium2 = new Isotrop(new Complex(1.5, 0.01*mr_im));
			double z0 = z;
			
			// act
			PlaneWave reflect  = field.Operation<IReflectOperation>().Reflect(new Halfspace(z0), medium2) as PlaneWave;
			PlaneWave transmit = field.Operation<IReflectOperation>().Transmit(new Halfspace(z0), medium2) as PlaneWave;
			
			// assert
			Vector3d point = new Vector3d(0, 0, z0);
			Vector3c n = new Vector3c(0, 0, 1);

			Vector3c f1 = (field.NearE(point) ^ n);
			Vector3c fr = (reflect.NearE(point) ^ n);
			Vector3c ft = (transmit.NearE(point) ^ n);

			Console.WriteLine((f1+fr).info());
			Console.WriteLine(ft.info());
			Console.WriteLine(((f1.y.im+fr.y.im)/ft.y.im));

			Console.WriteLine(String.Format("{0} : {1} : {2}", f1.Length().re, fr.Length().re, ft.Length().re));
			Assert.That(f1+fr, new Vector3cConstraint(ft, 1E-4));
		}
	}
}

