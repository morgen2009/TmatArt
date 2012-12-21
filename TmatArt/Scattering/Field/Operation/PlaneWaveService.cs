using System;
using TmatArt.Geometry.Region;
using TmatArt.Numeric.Mathematics;

namespace TmatArt.Scattering.Field.Operation
{
	public class PlaneWaveService: IReflectOperation, IExpansionOperation, IFieldOperation<PlaneWave>
	{
		public PlaneWaveService () { }

		PlaneWave field;

		/// <see cref="IFieldOperation"/> 
		public void SetField(PlaneWave field)
		{
			this.field = field;
		}

		/// <see cref="IReflectOperation.Reflect"/> 
		public Field Reflect(Halfspace region, Medium.Isotrop mediumExt)
		{
			Fresnel.Coefficients coef = Fresnel.Compute(this.field.medium.index.re, mediumExt.index.re, this.field.beta);
			PlaneWave res = new PlaneWave(System.Math.PI - field.beta, this.field.phi);
			double wavenumber = 2 * System.Math.PI / field.wave.value;
			Complex phase1 = Complex.AIM * System.Math.Cos(field.beta) * field.medium.index.re * region.z * wavenumber;
			Complex phase2 = phase1;
			res.ex     = coef.rp * Complex.Math.Exp(phase1) * Complex.Math.Exp(phase2) * this.field.ex;
			res.ey     = coef.rs * Complex.Math.Exp(phase1) * Complex.Math.Exp(phase2) * this.field.ey;
			res.norm   = 1;
			res.wave   = this.field.wave;
			res.medium = mediumExt;
			return res;
		}

		/// <see cref="IReflectOperation.Transmit"/> 
		public Field Transmit(Halfspace region, Medium.Isotrop mediumExt)
		{
			Fresnel.Coefficients coef = Fresnel.Compute(this.field.medium.index.re, mediumExt.index.re, this.field.beta);
			PlaneWave res = new PlaneWave(coef.theta2, this.field.phi);
			double wavenumber = 2 * System.Math.PI / field.wave.value;
			Complex phase1 = Complex.AIM * System.Math.Cos(field.beta) * field.medium.index.re * region.z * wavenumber;
			Complex phase2 = -Complex.AIM * System.Math.Cos(res.beta) * mediumExt.index.re * region.z * wavenumber;
			res.ex     = coef.tp * Complex.Math.Exp(phase1) * Complex.Math.Exp(phase2) * this.field.ex;
			res.ey     = coef.ts * Complex.Math.Exp(phase1) * Complex.Math.Exp(phase2) * this.field.ey;
			res.norm   = 1; //System.Math.Sqrt(coef.area);
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

