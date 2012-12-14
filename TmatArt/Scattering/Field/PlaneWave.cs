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
		private static System.Collections.Generic.Dictionary<Type, IFieldOperation> methods;

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

		public override IFieldOperation method(Type type)
		{
			// collect classes implementing fields operations
			if (PlaneWave.methods == null) {
				// TODO implement injection using some DI framework
				PlaneWave.methods = new System.Collections.Generic.Dictionary<Type, IFieldOperation>();
				PlaneWave.methods.Add(typeof(IReflectOperation), Operation.PlaneWaveOperations.Instance());
				PlaneWave.methods.Add(typeof(IExpansionOperation), Operation.PlaneWaveOperations.Instance());
			}

			// create object of the corresponding class for given operation
			if (PlaneWave.methods.ContainsKey(type)) {
				return (PlaneWave.methods[type] as IFieldOperation).Select(this);
			} else {
				// TODO if method is not found, look into parent class
				throw new Operation.NotFoundMethod (type, this.GetType());
			}
		}
	}
}

