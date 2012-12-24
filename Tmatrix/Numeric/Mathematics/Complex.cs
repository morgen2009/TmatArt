using System;

namespace TmatArt.Numeric.Mathematics
{
	/// <summary>
	/// Complex numbers
	/// </summary>
	public struct Complex: IAlgebraOperations<Complex, double>
	{
		/// <summary>
		/// Real and imaginary parts
		/// </summary>
		public double re, im;

		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="re">real part of complex number</param>
		/// <param name="im">imaginary part of complex number</param>
		public Complex (double re = 0, double im = 0)
		{
			this.re = re;
			this.im = im;
		}

		/// <summary>
		/// Create complex number from its Euler representation
		/// </summary>
		/// <param name="norm">Absolute value (modulus, magnitude) of the complex number</param>
		/// <param name="arg">Argument (phase) of the complex number</param>
		public static Complex Euler(double norm, double arg)
		{
			return new Complex(norm*System.Math.Cos(arg), norm*System.Math.Sin(arg));
		}

		/// <summary>Implicit convert double to complex</summary>
		/// <param name="a">Real part of complex number</param>
		public static implicit operator Complex(double a)
		{
			return new Complex(a, 0);
		}

		/// <summary>
		/// Return IMath object for complex number
		/// </summary>
		public static MathC Math
		{
			get
			{
				return MathC.Instance();
			}
		}

		/* implementation of IEqualityComparer */
		public bool Equals (Complex x, Complex y)
		{
			return x.re.Equals(y.re) && x.im.Equals(y.im);
		}
		
		public int GetHashCode (Complex obj)
		{
			return obj.re.GetHashCode() + obj.im.GetHashCode();
		}

		/* implementation of IGroupOperations */
		public Complex Add(Complex a)
		{
			return new Complex(this.re + a.re, this.im + a.im);
		}

		public Complex Subtract(Complex a)
		{
			return new Complex(this.re - a.re, this.im - a.im);
		}

		public Complex Negate()
		{
			return new Complex(-this.re, -this.im);
		}

		public Complex Zero()
		{
			return Complex.ZERO;
		}

		/* implementation of IRingOperations */
		public Complex Multiply(Complex a)
		{
			return new Complex(this.re * a.re - this.im * a.im, this.re * a.im + this.im * a.re);
		}

		public Complex Divide(Complex a)
		{
			double norm = a.re * a.re + a.im * a.im;
			return new Complex(this.re * a.re + this.im * a.im, - this.re * a.im + this.im * a.re).Divide(norm);
		}

		public Complex Inverse()
		{
			double norm = this.re * this.re + this.im * this.im;
			return new Complex(this.re, - this.im).Divide(norm);
		}

		public Complex One()
		{
			return Complex.ONE;
		}

		/* implementation of IVectorSpace */
		public Complex Multiply (double a)
		{
			return new Complex(this.re * a, this.im * a);
		}

		public Complex Divide (double a)
		{
			return new Complex(this.re / a, this.im / a);
		}

		/* Static operations */
		public static Complex operator + (Complex a, Complex b) { return a.Add(b); }
		public static Complex operator - (Complex a, Complex b) { return a.Subtract(b); }
		public static Complex operator - (Complex a) { return a.Negate(); }
		public static Complex operator * (Complex a, Complex b) { return a.Multiply(b);	}
		public static Complex operator * (Complex a, double b) { return a.Multiply(b);	}
		public static Complex operator * (double a, Complex b) { return b.Multiply(a); }
		public static Complex operator / (Complex a, Complex b) { return a.Divide(b); }
		public static Complex operator / (Complex a, double b) { return a.Divide(b); }
		public static Complex operator / (double a, Complex b) { return a*b.Inverse(); }

		/* Constants */
		public static Complex ZERO = new Complex(0, 0);
		public static Complex ONE  = new Complex(1, 0);
		public static Complex AIM  = new Complex(0, 1);

		/* addional methods */
		public Complex Conjugate()
		{
			return new Complex(re, -im);
		}
	}
}