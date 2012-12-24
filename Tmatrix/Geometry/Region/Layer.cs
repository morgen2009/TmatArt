using System;

namespace TmatArt.Geometry.Region
{
	public class Layer: IRegion
	{
		public readonly double zmin = 0;
		public readonly double zmax = 0;

		public Layer (double zmin, double zmax)
		{
			this.zmin = Math.Min(zmin, zmax);
			this.zmax = Math.Max(zmin, zmax);
		}

		public bool inside(Vector3d point)
		{
			return (point.z > this.zmin) && (point.z <= this.zmax);
		}
	}
}

