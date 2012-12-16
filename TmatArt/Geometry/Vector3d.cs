using System;
using TmatArt.Numeric.Mathematics;

namespace TmatArt.Geometry
{
	/// <summary>
	/// Vector with x, y, z real coordinates
	/// </summary>
	public struct Vector3d : IVector3x<Vector3d, double>
	{
		public double x, y, z;

		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="x">The x coordinate</param>
		/// <param name="y">The y coordinate</param>
		/// <param name="z">The z coordinate</param>
		public Vector3d (double x = 0, double y = 0, double z = 0)
		{
			this.x = x;
			this.y = y;
			this.z = z;
		}

		/* implementation of IEqualityComparer */
		public bool Equals(Vector3d a, Vector3d b)
		{
			return a.x.Equals(b.x) && a.y.Equals(b.y) && a.z.Equals(b.z);
		}

		public int GetHashCode(Vector3d a)
		{
			return a.x.GetHashCode() + a.y.GetHashCode() + a.z.GetHashCode();
		}

		/* implementation of IGroupOperations */
		public Vector3d Add(Vector3d a)
		{
			return new Vector3d(this.x + a.x, this.y + a.y, this.z + a.z);
		}

		public Vector3d Subtract(Vector3d a)
		{
			return new Vector3d(this.x - a.x, this.y - a.y, this.z - a.z);
		}
		
		public Vector3d Negate()
		{
			return new Vector3d(-this.x, -this.y, -this.z);
		}
		
		public Vector3d Zero()
		{
			return new Vector3d();
		}

		/* implementation of IVectorSpace */
		public Vector3d Multiply(double a)
		{
			return new Vector3d(a * this.x, a * this.y, a * this.z);
		}
		
		public Vector3d Divide(double a)
		{
			return new Vector3d(this.x / a, this.y / a, this.z / a);
		}

		/* implementation of IRingOperations */
		public Vector3d Multiply(Vector3d a)
		{
			return new Vector3d(
				this.y * a.z - this.z * a.y,
				this.z * a.x - this.x * a.z,
				this.x * a.y - this.y * a.x
				);
		}

		/* implementation of IHilbertSpace */
		public double Scalar(Vector3d a)
		{
			return this.x * a.x + this.y * a.y + this.z * a.z;
		}
		
		public double Length()
		{
			return System.Math.Sqrt(this.Scalar(this));
		}
		
		/* implementation of IVector3x */
		public Vector3d RotateX (double angle)
		{
			double cos = System.Math.Cos(angle);
			double sin = System.Math.Sin(angle);
			
			return new Vector3d(
				this.x,
				this.y * cos + this.z * sin,
				-this.y * sin + this.z * cos
				);
		}
		
		public Vector3d RotateY (double angle)
		{
			double cos = System.Math.Cos(angle);
			double sin = System.Math.Sin(angle);
			
			return new Vector3d(
				this.x * cos - this.z * sin,
				this.y,
				this.x * sin + this.z * cos
				);
		}
		
		public Vector3d RotateZ (double angle)
		{
			double cos = System.Math.Cos(angle);
			double sin = System.Math.Sin(angle);
			
			return new Vector3d(
				this.x * cos + this.y * sin,
				-this.x * sin + this.y * cos,
				this.z
				);
		}

		/* Static operations */
		public static Vector3d operator + (Vector3d a, Vector3d b) { return a.Add(b); }
		public static Vector3d operator - (Vector3d a, Vector3d b) { return a.Subtract(b); }
		public static Vector3d operator - (Vector3d a) { return a.Negate(); }
		public static Vector3d operator * (Vector3d a, double b) { return a.Multiply(b);	}
		public static Vector3d operator * (double a, Vector3d b) { return b.Multiply(a); }
		public static Vector3d operator / (Vector3d a, double b) { return a.Divide(b); }
		public static double operator * (Vector3d a, Vector3d b) { return a.Scalar(b);	}
		public static Vector3d operator ^ (Vector3d a, Vector3d b) { return a.Multiply(b); }
	}
}

