using System;
using TmatArt.Geometry;
using TmatArt.Numeric.Mathematics;

namespace TmatArt.Scattering.Field
{
	public class Container: Field
	{
		public Field field;

		public override Vector3c near(Vector3d r)
		{
			return this.field.near(r);
		}
		
		public override Vector3c far(double phi, double theta)
		{
			return this.field.far(phi, theta);
		}		
	}
}

