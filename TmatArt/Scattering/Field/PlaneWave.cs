using System;
using TmatArt.Geometry;
using TmatArt.Numeric.Mathematics;
using TmatArt.Geometry.Region;
using TmatArt.Scattering.Medium;

namespace TmatArt.Scattering.Field
{
	/// <summary>
	/// Plane electromagnetic wave
	/// </summary>
	public class PlaneWave : Field
	{
		public double beta, phi;
		public Complex ex, ey;
		public double  norm;

		public enum Polarization {VERTICAL, HORIZONTAL, CIRCULAR_R, CIRCULAR_L};

		public PlaneWave(double beta, double phi, Polarization polarization = Polarization.VERTICAL)
		{
			this.beta  = beta;
			this.phi   = phi;
			this.norm  = 1;

			switch (polarization) {
			case Polarization.CIRCULAR_L: this.ex = new Complex(1); this.ey = new Complex(1) * Complex.Euler(1, -System.Math.PI / 2); break;
			case Polarization.CIRCULAR_R: this.ex = new Complex(1); this.ey = new Complex(1) * Complex.Euler(1, System.Math.PI / 2); break;
			case Polarization.VERTICAL  : this.ex = new Complex(1); this.ey = new Complex(0); break;
			case Polarization.HORIZONTAL: this.ex = new Complex(0); this.ey = new Complex(1); break;
			}
		}

		public FieldFactory factory()
		{
			return PlaneWaveFactory.Instance();
		}

		public override Vector3c NearE (Vector3d r)
		{
			throw new System.NotImplementedException ();
		}

		public override Vector3c FarE (Euler e)
		{
			throw new System.NotImplementedException ();
		}
	}

	/// <summary>
	/// The factory for @PlaneWave class
	/// </summary>
	public class PlaneWaveFactory: FieldFactory
	{
		private static PlaneWaveFactory instance;
		private PlaneWaveFactory() { }

		public static PlaneWaveFactory Instance()
		{
			if (PlaneWaveFactory.instance != null) {
				PlaneWaveFactory.instance = new PlaneWaveFactory();
			}

			return PlaneWaveFactory.instance;
		}

		public override Field Reflect(Halfspace region, Isotrop mediumExt)
		{
			throw new System.NotImplementedException ();
		}

		public override Field Transmit(Halfspace region, Isotrop mediumExt)
		{
			throw new System.NotImplementedException ();
		}
	}
}

