using System;
using TmatArt.Numeric.Mathematics;

namespace TmatArt.Scattering
{
	/// <summary>
	/// Class to compute Fresnell coefficients
	/// </summary>
	public static class Fresnel
	{
		public struct Coefficients
		{
			public Complex thetaIn, thetaOut;
			public Complex tp, ts, rp, rs;
		}

		private static Complex ThetaOut(Complex thetaIn, Complex indexIn, Complex indexOut)
		{
			return Complex.Math.Asin(Complex.Math.Sin(thetaIn)*indexIn/indexOut);
		}
		
		public static Coefficients Compute(Complex thetaIn, Complex indexIn, Complex indexOut)
		{
			var res = new Coefficients();

			// scattering angle for transmitted wave
			res.thetaIn  = thetaIn;
			res.thetaOut = Fresnel.ThetaOut(thetaIn, indexIn, indexOut);
			Complex cosIn  = Complex.Math.Cos(res.thetaIn);
			Complex cosOut = Complex.Math.Cos(res.thetaOut);

			Console.WriteLine("{0}  :  {1}", res.thetaOut.re, res.thetaOut.im);
			Console.WriteLine("{0}  :  {1}", cosOut.re, cosOut.im);

			// coefficients for reflected wave
			res.rp = (indexOut * cosIn - indexIn * cosOut) / (indexIn * cosOut + indexOut * cosIn);
			res.rs = (indexIn * cosIn - indexOut * cosOut) / (indexIn * cosIn + indexOut * cosOut);

			// coefficients for transmitted wave
			res.tp = 2 * indexIn * cosIn / (indexIn * cosOut + indexOut * cosIn);
			res.ts = 2 * indexIn * cosIn / (indexIn * cosIn + indexOut * cosOut);

			return res;
		}
	}
}

