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
			public double theta1, theta2;
			public double tp, ts, rp, rs;
			public double area;
		}

		public static Coefficients Compute(double index1, double index2, double theta1)
		{
			var res = new Coefficients();
			res.theta1 = theta1;
			res.theta2 = Math.Asin(Math.Sin(theta1)*index1/index2);
			double cos1 = Math.Cos(res.theta1);
			double cos2 = Math.Cos(res.theta2);
			res.rp = (index2 * cos1 - index1 * cos2) / (index1 * cos2 + index2 * cos1);
			res.rs = (index1 * cos1 - index2 * cos2) / (index1 * cos1 + index2 * cos2);
			res.tp = 2 * index1 * cos1 / (index1 * cos2 + index2 * cos1);
			res.ts = 2 * index1 * cos1 / (index1 * cos1 + index2 * cos2);
			res.area = cos2 / cos1;
			return res;
		}
	}
}

