using System;
using TmatArt.Geometry;
using TmatArt.Numeric.Mathematics;
using TmatArt.Geometry.Region;
using TmatArt.Scattering.Medium;
using TmatArt.Scattering.Field.Operation;
using TmatArt.Scattering.Field.Impl;

namespace TmatArt.Scattering.Field
{
	/// <summary>
	/// Plane electromagnetic wave
	/// </summary>
	public class PlaneWave : Field
	{
		public Complex beta;
		public double phi;
		public Complex ex, ey;
		public double  norm;
		private static Container<PlaneWave> container;

		public enum Polarization {VERTICAL, HORIZONTAL, CIRCULAR_R, CIRCULAR_L};

		public PlaneWave(PlaneWave field)
		{
			this.beta = field.beta;
			this.phi  = field.phi;
			this.ex   = field.ex;
			this.ey   = field.ey;
			this.norm = field.norm;
			this.wave = field.wave;
			this.medium = field.medium;
		}

		public PlaneWave(Complex beta, double phi, Polarization polarization = Polarization.VERTICAL)
		{
			this.beta  = beta;
			this.phi   = phi;
			this.norm  = 1;
			this.wave  = WaveLength.Default;
			this.medium= Isotrop.Default;

			switch (polarization) {
			case Polarization.CIRCULAR_L: this.ex = new Complex(1); this.ey = new Complex(1) * Complex.Euler(1, -System.Math.PI / 2); break;
			case Polarization.CIRCULAR_R: this.ex = new Complex(1); this.ey = new Complex(1) * Complex.Euler(1, System.Math.PI / 2); break;
			case Polarization.VERTICAL  : this.ex = new Complex(1); this.ey = new Complex(0); break;
			case Polarization.HORIZONTAL: this.ex = new Complex(0); this.ey = new Complex(1); break;
			}
		}

		public override Vector3c NearE (Vector3d r)
		{
			Vector3c res = r.Rotate(Axis3Name.Z, phi).Rotate(Axis3Name.Y, beta); // conjugate?
			//Euler e = new Euler(phi, beta.re, 0);
			Complex wavenumber = (2*System.Math.PI / this.wave.length) * this.medium.index;
			Complex phase = Complex.AIM * wavenumber * res.z;
			if (this.beta.im != 0) {
				Console.WriteLine(String.Format("beta  {0} {1}", beta.re, beta.im));
				Console.WriteLine(String.Format("phase {0} {1}", phase.re, phase.im));
			}
			Vector3c v = new Vector3c(this.ex, this.ey, 0) * Complex.Math.Exp(phase) * this.norm;
			return v.Rotate(Axis3Name.Y, -beta).Rotate(Axis3Name.Z, -phi);
		}

		public override Vector3c FarE (Euler e)
		{
			throw new System.NotImplementedException ();
		}                             

		public override T Resolve<T>()
		{
			// collect classes implementing fields operations
			if (PlaneWave.container == null) {
				PlaneWave.container = new Container<PlaneWave>();
				PlaneWave.container.Register<IReflectOperation, PlaneWaveImpl>();
				PlaneWave.container.Register<IExpansionOperation, PlaneWaveImpl>();
			}

			// create object of the corresponding class for given operation
			return PlaneWave.container.Resolve<T>(this);
		}
	}
}

