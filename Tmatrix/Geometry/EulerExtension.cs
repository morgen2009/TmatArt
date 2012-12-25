using System;
using TmatArt.Numeric.Mathematics;

namespace TmatArt.Geometry
{
	public static class EulerExtension
	{
		public static Vector3d Rotate(this Vector3d vector, Euler euler)
		{
			return euler.Rotate(vector);
		}

		public static Vector3c Rotate(this Vector3c vector, Euler euler)
		{
			return euler.Rotate(vector);
		}
	}
}

