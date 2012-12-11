using System;

namespace TmatArt.Geometry
{
	public static class Vector3dEulerExtension
	{
		/// <summary>
		/// Rotation of coordinate system around X axis by given angle
		/// </summary>
		/// <returns>The original vector in the rotated coordinate system</returns>
		/// <param name="vector">Vector.</param>
		/// <param name="angle">Angle (positive value corresponds clockwise rotation).</param>
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
		
		/// <summary>
		/// Rotation of coordinate system around Y axis by given angle
		/// </summary>
		/// <returns>The original vector in the rotated coordinate system</returns>
		/// <param name="vector">Vector.</param>
		/// <param name="angle">Angle (positive value corresponds clockwise rotation).</param>
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
		
		/// <summary>
		/// Rotation of coordinate system around Z axis by given angle
		/// </summary>
		/// <returns>The original vector in the rotated coordinate system</returns>
		/// <param name="vector">Vector.</param>
		/// <param name="angle">Angle (positive value corresponds clockwise rotation).</param>
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
		
		/// <summary>
		/// Rotation of coordinate system by Euler angles
		/// </summary>
		/// <returns>The original vector in the rotated coordinate system</returns>
		/// <param name="vector">Vector.</param>
		/// <param name="euler">Euler angles.</param>
		public static Vector3d Rotate (this Vector3d vector, Euler euler)
		{
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
			
			return r;
		}
	}
}

