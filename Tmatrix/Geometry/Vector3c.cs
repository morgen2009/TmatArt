using System;
using TmatArt.Numeric.Mathematics;

namespace TmatArt.Geometry
{
	/// <summary>
	/// Vector with x, y, z complex coordinates
	/// </summary>
	public struct Vector3c :
		IHilbertSpace<Vector3c, Complex>,
		IRingOperations<Vector3c>,
		IRotatableAxis<Vector3c, Axis3Name, double>,
		IRotatableAxis<Vector3c, Axis3Name, Complex>
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

		/// <see cref="IEqualityComparer.Equals"/>
		public bool Equals(Vector3c a, Vector3c b)
		{
			return a.x.Equals(b.x) && a.y.Equals(b.y) && a.z.Equals(b.z);
		}

		/// <see cref="IEqualityComparer.GetHashCode"/>
		public int GetHashCode(Vector3c a)
		{
			return a.x.GetHashCode() + a.y.GetHashCode() + a.z.GetHashCode();
		}

		/// <see cref="IGroupOperations.Add"/>
		public Vector3c Add(Vector3c a)
		{
			return new Vector3c(this.x + a.x, this.y + a.y, this.z + a.z);
		}

		/// <see cref="IGroupOperations.Subtract"/>
		public Vector3c Subtract(Vector3c a)
		{
			return new Vector3c(this.x - a.x, this.y - a.y, this.z - a.z);
		}
		
		/// <see cref="IGroupOperations.Negate"/>
		public Vector3c Negate()
		{
			return new Vector3c(-this.x, -this.y, -this.z);
		}
		
		/// <see cref="IGroupOperations.Zero"/>
		public Vector3c Zero()
		{
			return new Vector3c();
		}

		/// <see cref="IVectorSpace.Negate"/>
		public Vector3c Multiply(Complex a)
		{
			return new Vector3c(a * this.x, a * this.y, a * this.z);
		}
		
		/// <see cref="IVectorSpace.Divide"/>
		public Vector3c Divide(Complex a)
		{
			Complex a1 = a.Inverse();
			return new Vector3c(a1 * this.x, a1 * this.y, a1 * this.z);
		}

		/// <see cref="IRingOperations.Multiply"/>
		public Vector3c Multiply(Vector3c a)
		{
			Vector3c a1 = new Vector3c(
				a.x.Conjugate(),
				a.y.Conjugate(),
				a.z.Conjugate()
			);

			return new Vector3c(
				this.y * a1.z - this.z * a1.y,
				this.z * a1.x - this.x * a1.z,
				this.x * a1.y - this.y * a1.x
			);
		}

		/// <see cref="IHilbertSpace.Scalar"/>
		public Complex Scalar(Vector3c a)
		{
			Vector3c a1 = new Vector3c(
				a.x.Conjugate(),
				a.y.Conjugate(),
				a.z.Conjugate()
			);
			return this.x * a1.x + this.y * a1.y + this.z * a1.z;
		}
		
		/// <see cref="IHilbertSpace.Length"/>
		public Complex Length()
		{
			return Complex.Math.Sqrt(this.Scalar(this));
		}
		
		/// <see cref="IRotatableAxis<Complex, Axis3Name, double>.Rotate"/>
		public Vector3c Rotate(Axis3Name axis, double angle)
		{
			double cos = System.Math.Cos(angle);
			double sin = System.Math.Sin(angle);

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
			default : return this;
			}
		}

		/// <see cref="IRotatableAxis<Complex, Axis3Name, Complex>.Rotate"/>
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
			default : return this;
			}
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

		/// <summary>
		/// Real part
		/// </summary>
		public Vector3d re
		{
			get
			{
				return new Vector3d(this.x.re, this.y.re, this.z.re);
			}
			set
			{
				this.x.re = value.x;
				this.y.re = value.y;
				this.z.re = value.z;
			}
		}

		/// <summary>
		/// Imaginary part
		/// </summary>
		public Vector3d im
		{
			get
			{
				return new Vector3d(this.x.im, this.y.im, this.z.im);
			}
			set
			{
				this.x.im = value.x;
				this.y.im = value.y;
				this.z.im = value.z;
			}
		}
	}
}

