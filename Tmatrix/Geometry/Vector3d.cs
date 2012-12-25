using System;
using TmatArt.Numeric.Mathematics;

namespace TmatArt.Geometry
{
	/// <summary>
	/// Vector with x, y, z real coordinates
	/// </summary>
	public struct Vector3d :
		IHilbertSpace<Vector3d, double>,
		IRingOperations<Vector3d>,
		IRotatableAxis<Vector3d, Axis3Name, double>,
		IRotatableAxis<Vector3c, Axis3Name, Complex>
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

		/// <see cref="IEqualityComparer.Equals"/>
		public bool Equals(Vector3d a, Vector3d b)
		{
			return a.x.Equals(b.x) && a.y.Equals(b.y) && a.z.Equals(b.z);
		}

		/// <see cref="IEqualityComparer.GetHashCode"/>
		public int GetHashCode(Vector3d a)
		{
			return a.x.GetHashCode() + a.y.GetHashCode() + a.z.GetHashCode();
		}

		/// <see cref="IGroupOperations.Add"/>
		public Vector3d Add(Vector3d a)
		{
			return new Vector3d(this.x + a.x, this.y + a.y, this.z + a.z);
		}

		/// <see cref="IGroupOperations.Subtract"/>
		public Vector3d Subtract(Vector3d a)
		{
			return new Vector3d(this.x - a.x, this.y - a.y, this.z - a.z);
		}
		
		/// <see cref="IGroupOperations.Negate"/>
		public Vector3d Negate()
		{
			return new Vector3d(-this.x, -this.y, -this.z);
		}
		
		/// <see cref="IGroupOperations.Zero"/>
		public Vector3d Zero()
		{
			return new Vector3d();
		}

		/// <see cref="IVectorSpace.Negate"/>
		public Vector3d Multiply(double a)
		{
			return new Vector3d(a * this.x, a * this.y, a * this.z);
		}
		
		/// <see cref="IVectorSpace.Divide"/>
		public Vector3d Divide(double a)
		{
			return new Vector3d(this.x / a, this.y / a, this.z / a);
		}

		/// <see cref="IRingOperations.Multiply"/>
		public Vector3d Multiply(Vector3d a)
		{
			return new Vector3d(
				this.y * a.z - this.z * a.y,
				this.z * a.x - this.x * a.z,
				this.x * a.y - this.y * a.x
				);
		}

		/// <see cref="IHilbertSpace.Scalar"/>
		public double Scalar(Vector3d a)
		{
			return this.x * a.x + this.y * a.y + this.z * a.z;
		}
		
		/// <see cref="IHilbertSpace.Length"/>
		public double Length()
		{
			return System.Math.Sqrt(this.Scalar(this));
		}
		
		/// <see cref="IRotatableAxis<Vector3d, Axis3Name, double>.Rotate"/>
		public Vector3d Rotate(Axis3Name axis, double angle)
		{
			double cos = System.Math.Cos(angle);
			double sin = System.Math.Sin(angle);
			
			switch (axis)
			{
			case Axis3Name.X :
				return new Vector3d(
					this.x,
					this.y * cos + this.z * sin,
					-this.y * sin + this.z * cos
					);
			case Axis3Name.Y :
				return new Vector3d(
					this.x * cos - this.z * sin,
					this.y,
					this.x * sin + this.z * cos
					);
			case Axis3Name.Z :
				return new Vector3d(
					this.x * cos + this.y * sin,
					-this.x * sin + this.y * cos,
					this.z
					);
			default : return this;
			}
		}
		
		/// <see cref="IRotatableAxis<Vector3c, Axis3Name, Complex>.Rotate"/>
		public Vector3c Rotate(Axis3Name axis, Complex angle)
		{
			Complex cos = Complex.Math.Cos(angle);
			Complex sin = Complex.Math.Sin(angle);
			
			switch (axis)
			{
			case Axis3Name.X :
				return new Vector3c(
					this.x,
					this.y * cos + this.z * sin,
					-this.y * sin + this.z * cos
					);
			case Axis3Name.Y :
				return new Vector3c(
					this.x * cos - this.z * sin,
					this.y,
					this.x * sin + this.z * cos
					);
			case Axis3Name.Z :
				return new Vector3c(
					this.x * cos + this.y * sin,
					-this.x * sin + this.y * cos,
					this.z
					);
			default :
				return new Vector3c(x, y, z);
			}
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

