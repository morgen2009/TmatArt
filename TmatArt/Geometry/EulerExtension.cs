using System;

namespace TmatArt.Geometry
{
	public static class EulerExtension
	{
		public static Vector3d RotateX (this Vector3d vector, double angle)
		{
			double cos = System.Math.Cos(angle);
			double sin = System.Math.Sin(angle);
			
			return new Vector3d(
				vector.x,
				vector.y * cos + vector.z * sin,
				-vector.y * sin + vector.z * cos
			);
		}
		
		public static Vector3d RotateY (this Vector3d vector, double angle)
		{
			double cos = System.Math.Cos(angle);
			double sin = System.Math.Sin(angle);
			
			return new Vector3d(
				vector.x * cos - vector.z * sin,
				vector.y,
				vector.x * sin + vector.z * cos
			);
		}
		
		public static Vector3d RotateZ (this Vector3d vector, double angle)
		{
			double cos = System.Math.Cos(angle);
			double sin = System.Math.Sin(angle);
			
			return new Vector3d(
				vector.x * cos + vector.y * sin,
				-vector.x * sin + vector.y * cos,
				vector.z
			);
		}
		
		public static Vector3d Rotate (this Vector3d vector, Euler euler)
		{
			// TODO check rotation of Vector3d by Euler angles
			Vector3d r = vector;

			// rotate vector around Z axis by angle &alpha;
			if (euler.alpha != 0) {
				r = r.RotateZ(euler.alpha);
			}
			
			// rotate vector around Y' axis by angle &beta;
			if (euler.beta != 0) {
				r = r.RotateY(euler.beta);
			}

			// rotate vector around Z'' axis by angle &gamma;
			if (euler.gamma != 0) {
				r = r.RotateZ(euler.gamma);
			}
			
			return vector;
		}

		public static Vector3c Rotate (this Vector3c vector, Euler euler)
		{
			// TODO implement rotation of Vector3c by Euler angles
			return vector;
		}
	}
}

