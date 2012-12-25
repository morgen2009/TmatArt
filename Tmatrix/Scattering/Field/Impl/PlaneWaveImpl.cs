using System;
using TmatArt.Geometry.Region;
using TmatArt.Numeric.Mathematics;
using TmatArt.Scattering.Field.Operation;

namespace TmatArt.Scattering.Field.Impl
{
	public struct PlaneWaveImpl: IReflectOperation, IExpansionOperation, IFieldOperation<PlaneWave>
	{
		PlaneWave field;

		/// <see cref="IFieldOperation.SetField"/> 
		public void SetField(PlaneWave field)
		{
			this.field = field;
		}

		/// <see cref="IReflectOperation.Reflect"/> 
		public Field Reflect(Halfspace region, Medium.Isotrop mediumExt)
		{
			Fresnel.Coefficients coef = Fresnel.Compute(this.field.beta, this.field.medium.index, mediumExt.index);
			// TODO theta_reflected = pi - theta seems be wrong for absorbing media
			PlaneWave res = new PlaneWave(System.Math.PI - field.beta, this.field.phi);
			double wavenumber = 2 * System.Math.PI / field.wave.length;
			Complex phase1 = Complex.AIM * Complex.Math.Cos(field.beta) * field.medium.index * region.z * wavenumber;
			Complex phase2 = phase1;
			res.ex     = Complex.Math.Exp(phase2) * coef.rp * Complex.Math.Exp(phase1) * this.field.ex;
			res.ey     = Complex.Math.Exp(phase2) * coef.rs * Complex.Math.Exp(phase1) * this.field.ey;
			res.norm   = 1;
			res.wave   = this.field.wave;
			res.medium = this.field.medium;
			return res;
		}

		/// <see cref="IReflectOperation.Transmit"/> 
		public Field Transmit(Halfspace region, Medium.Isotrop mediumExt)
		{
			Fresnel.Coefficients coef = Fresnel.Compute(this.field.beta, this.field.medium.index, mediumExt.index);
			PlaneWave res = new PlaneWave(coef.thetaOut, this.field.phi, PlaneWave.Polarization.HORIZONTAL);
			double wavenumber = 2 * System.Math.PI / field.wave.length;
			Complex phase1 = Complex.AIM * Complex.Math.Cos(field.beta) * field.medium.index * region.z * wavenumber;
			Complex phase2 = -Complex.AIM * Complex.Math.Cos(res.beta) * mediumExt.index * region.z * wavenumber;
			res.ex     = Complex.Math.Exp(phase2) * coef.tp * Complex.Math.Exp(phase1) * this.field.ex;
			res.ey     = Complex.Math.Exp(phase2) * coef.ts * Complex.Math.Exp(phase1) * this.field.ey;
			res.norm   = 1;
			res.wave   = this.field.wave;
			res.medium = mediumExt;
			return res;
		}

		public Expansion Expansion(int nrank, int mrank)
		{
			throw new System.NotImplementedException ();
		}
	}
}

