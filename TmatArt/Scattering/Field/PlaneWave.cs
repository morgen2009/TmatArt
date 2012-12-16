using System;
using TmatArt.Geometry;
using TmatArt.Numeric.Mathematics;
using TmatArt.Geometry.Region;
using TmatArt.Scattering.Medium;
using TmatArt.Scattering.Field.Operation;

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
		private static ServiceContainer<PlaneWave> container;

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

		public override Vector3c NearE (Vector3d r)
		{
			throw new System.NotImplementedException ();
		}

		public override Vector3c FarE (Euler e)
		{
			throw new System.NotImplementedException ();
		}

		public override T Operation<T>()
		{
			// collect classes implementing fields operations
			if (PlaneWave.container == null) {
				PlaneWave.container = new ServiceContainer<PlaneWave>();
				PlaneWave.container.Register<IReflectOperation, Operation.PlaneWaveService>();
				PlaneWave.container.Register<IExpansionOperation, Operation.PlaneWaveService>();
			}

			// create object of the corresponding class for given operation
			return PlaneWave.container.Resolve<T>(this);
		}
	}
}

