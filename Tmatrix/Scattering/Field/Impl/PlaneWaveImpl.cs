using System;
using TmatArt.Geometry.Region;
using TmatArt.Numeric.Mathematics;
using TmatArt.Scattering.Field.Operation;

namespace TmatArt.Scattering.Field.Impl
{
	public struct PlaneWaveImpl: IReflectOperation, IExpansionOperation
	{
		public readonly PlaneWave field;

		/// <summary>
		/// Initializes a new instance of the <see cref="TmatArt.Scattering.Field.Impl.PlaneWaveImpl"/> struct.
		/// </summary>
		/// <param name="field">Field.</param>
		public PlaneWaveImpl(PlaneWave field)
		{
			this.field = field;
		}

		/// <see cref="IReflectOperation.Reflect"/> 
		public Field Reflect(Halfspace region, Medium.Isotrop mediumExt)
		{
			// TODO theta_reflected = pi - theta seems be wrong for absorbing media
			Fresnel.Coefficients coef = Fresnel.Compute(this.field.beta, this.field.medium.index, mediumExt.index);
			double wavenumber = 2 * System.Math.PI / field.wave.length;
			Complex phase = 2 * Complex.Math.Cos(field.beta) * field.medium.index;
			phase = Complex.AIM * phase * region.z * wavenumber;

			PlaneWave res = new PlaneWave(field);
			res.beta   = System.Math.PI - field.beta;
			res.ex     = coef.rp * Complex.Math.Exp(phase) * this.field.ex;
			res.ey     = coef.rs * Complex.Math.Exp(phase) * this.field.ey;

			return res;
		}

		/// <see cref="IReflectOperation.Transmit"/> 
		public Field Transmit(Halfspace region, Medium.Isotrop mediumExt)
		{
			Fresnel.Coefficients coef = Fresnel.Compute(this.field.beta, this.field.medium.index, mediumExt.index);
			double wavenumber = 2 * System.Math.PI / field.wave.length;
			Complex phase = Complex.Math.Cos(field.beta) * field.medium.index - Complex.Math.Cos(coef.thetaOut) * mediumExt.index;
			phase = Complex.AIM * phase * region.z * wavenumber;

			PlaneWave res = new PlaneWave(field);
			res.beta   = coef.thetaOut;
			res.ex     = coef.tp * Complex.Math.Exp(phase) * this.field.ex;
			res.ey     = coef.ts * Complex.Math.Exp(phase) * this.field.ey;
			res.medium = mediumExt;

			return res;
		}

		public Expansion Expansion(int nrank, int mrank)
		{
			throw new System.NotImplementedException ();
		}
	}
}

