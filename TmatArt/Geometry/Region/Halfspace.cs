using System;

namespace TmatArt.Geometry.Region
{
	public class Halfspace: IRegion
	{
		double z = 0;

		public Halfspace (double z)
		{
			this.z = z;
		}

		public bool inside(Vector3d point)
		{
			return point.z > this.z;
		}
	}
}

