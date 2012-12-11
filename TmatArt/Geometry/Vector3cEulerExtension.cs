using System;

namespace TmatArt.Geometry
{
	public static class Vector3cEulerExtension
	{
		/// <summary>
		/// Rotation of coordinate system around X axis by given angle
		/// </summary>
		/// <returns>The original vector in the rotated coordinate system</returns>
		/// <param name="vector">Vector.</param>
		/// <param name="angle">Angle (positive value corresponds clockwise rotation).</param>
		public static Vector3c RotateX (this Vector3c vector, double angle)
		{
			double cos = System.Math.Cos(angle);
			double sin = System.Math.Sin(angle);
			
			return new Vector3c(
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
		public static Vector3c RotateY (this Vector3c vector, double angle)
		{
			double cos = System.Math.Cos(angle);
			double sin = System.Math.Sin(angle);
			
			return new Vector3c(
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
		public static Vector3c RotateZ (this Vector3c vector, double angle)
		{
			double cos = System.Math.Cos(angle);
			double sin = System.Math.Sin(angle);
			
			return new Vector3c(
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
		public static Vector3c Rotate (this Vector3c vector, Euler euler)
		{
			Vector3c r = vector;

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

