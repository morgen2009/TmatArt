using System;
using TmatArt.Numeric.Mathematics;

namespace TmatArt.Geometry
{
	/// <summary>
	/// Vector with x, y, z complex coordinates
	/// </summary>
	public struct Vector3c : IHilbertSpace<Vector3c, Complex>, IRingOperations<Vector3c>
	{
		public Complex x, y, z;

		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="x">The x coordinate</param>
		/// <param name="y">The y coordinate</param>
		/// <param name="z">The z coordinate</param>
		public Vector3c (Complex x, Complex y, Complex z)
		{
			this.x = x;
			this.y = y;
			this.z = z;
		}

		/* implementation of IEqualityComparer */
		public bool Equals(Vector3c a, Vector3c b)
		{
			return a.x.Equals(b.x) && a.y.Equals(b.y) && a.z.Equals(b.z);
		}

		public int GetHashCode(Vector3c a)
		{
			return a.x.GetHashCode() + a.y.GetHashCode() + a.z.GetHashCode();
		}

		/* implementation of IGroupOperations */
		public Vector3c Add(Vector3c a)
		{
			return new Vector3c(this.x + a.x, this.y + a.y, this.z + a.z);
		}

		public Vector3c Subtract(Vector3c a)
		{
			return new Vector3c(this.x - a.x, this.y - a.y, this.z - a.z);
		}
		
		public Vector3c Negate()
		{
			return new Vector3c(-this.x, -this.y, -this.z);
		}
		
		public Vector3c Zero()
		{
			return new Vector3c();
		}

		/* implementation of IVectorSpace */
		public Vector3c Multiply(Complex a)
		{
			return new Vector3c(a * this.x, a * this.y, a * this.z);
		}
		
		public Vector3c Divide(Complex a)
		{
			Complex a1 = a.Inverse();
			return new Vector3c(a1 * this.x, a1 * this.y, a1 * this.z);
		}

		/* implementation of IRingOperations */
		public Vector3c Multiply(Vector3c a)
		{
			Vector3c a1 = new Vector3c(
				Complex.Math.Conjugate(a.x),
				Complex.Math.Conjugate(a.y),
				Complex.Math.Conjugate(a.z)
			);

			return new Vector3c(
				this.y * a1.z - this.z * a1.y,
				this.z * a1.x - this.x * a1.z,
				this.x * a1.y - this.y * a1.x
			);
		}

		/* implementation of IHilbertSpace */
		public Complex Scalar(Vector3c a)
		{
			Vector3c a1 = new Vector3c(
				Complex.Math.Conjugate(a.x),
				Complex.Math.Conjugate(a.y),
				Complex.Math.Conjugate(a.z)
			);
			return this.x * a1.x + this.y * a1.y + this.z * a1.z;
		}
		
		public Complex Length()
		{
			return Complex.Math.Sqrt(this.Scalar(this));
		}
		
		/* Static operations */
		public static Vector3c operator + (Vector3c a, Vector3c b) { return a.Add(b); }
		public static Vector3c operator - (Vector3c a, Vector3c b) { return a.Subtract(b); }
		public static Vector3c operator - (Vector3c a) { return a.Negate(); }
		public static Vector3c operator * (Vector3c a, Complex b) { return a.Multiply(b);	}
		public static Vector3c operator * (Complex a, Vector3c b) { return b.Multiply(a); }
		public static Vector3c operator / (Vector3c a, Complex b) { return a.Divide(b); }
		public static Complex operator * (Vector3c a, Vector3c b) { return a.Scalar(b);	}
		public static Vector3c operator ^ (Vector3c a, Vector3c b) { return a.Multiply(b); }
	}
}

