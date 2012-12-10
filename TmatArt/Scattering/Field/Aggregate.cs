using System;
using TmatArt.Geometry;
using TmatArt.Numeric.Mathematics;

namespace TmatArt.Scattering.Field
{
	public class Aggregate: Field
	{
		public Field [] fields;

		public override Vector3c near(Vector3d r)
		{
			Vector3c res = new Vector3c(0, 0, 0);

			foreach (Field field in this.fields) {
				res += field.near(r);
			}

			return res;
		}
		
		public override Vector3c far(double phi, double theta)
		{
			Vector3c res = new Vector3c(0, 0, 0);
			
			foreach (Field field in this.fields) {
				res += field.far(phi, theta);
			}
			
			return res;
		}		
	}
}

