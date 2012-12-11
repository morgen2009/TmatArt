using System;
using TmatArt.Geometry;
using TmatArt.Numeric.Mathematics;

namespace TmatArt.Scattering.Field
{
	public class PlaneWave: Field
	{
		public Complex Ax, Ay;

		public override Vector3c near(Vector3d r)
		{
			return new Vector3c(0, 0, 0);
		}
		
		public override Vector3c far(double phi, double theta)
		{
			return new Vector3c(0, 0, 0);
		}		
	}
}

