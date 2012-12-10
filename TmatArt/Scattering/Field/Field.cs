using System;
using TmatArt.Geometry;

namespace TmatArt.Scattering.Field
{
	public abstract class Field
	{
		public Coordinate coordinate;

		public Vector3c near(double x, double y, double z)
		{
			return this.near(new Vector3d(x, y, z));
		}

		/* abstract methods to be implemented */
		public abstract Vector3c near(Vector3d r);
		public abstract Vector3c far(double phi, double theta);
	}
}

