using System;

namespace TmatArt.Numeric
{
	/**
	 *  Complex number
	 */
	public struct Complex
	{
		const double threshold = 1E-14;
		public double re, im;
		
	    /* Constructor */
		public Complex (double re = 0, double im = 0)
		{
			this.re = re;
			this.im = im;
		}
		public Complex (Complex x)
		{
			this.re = x.re;
			this.im = x.im;
		}
		public static Complex c (double re = 0, double im = 0)
		{
			return new Complex(re, im);
		}
		public static Complex c (Complex x)
		{
			return new Complex(x.re, x.im);
		}

		/* Arithmetic operations: summation */
		public static Complex operator + (Complex a, double b)
		{
			return new Complex(a.re + b, a.im);
		}
		public static Complex operator + (Complex a, Complex b)
		{
			return new Complex(a.re + b.re, a.im + b.im);
		}
		public static Complex operator + (double a, Complex b)
		{
			return new Complex(a + b.re, b.im);
		}

	    /* Arithmetic operations: subtraction */
		public static Complex operator - (Complex a, double b)
		{
			return new Complex(a.re - b, a.im);
		}
		public static Complex operator - (Complex a, Complex b)
		{
			return new Complex(a.re - b.re, a.im - b.im);
		}
		public static Complex operator - (double a, Complex b)
		{
			return new Complex(a - b.re, -b.im);
		}
		public static Complex operator - (Complex a)
		{
			return new Complex(-a.re, -a.im);
		}
		
	    /* Arithmetic operations: multiplication */
		public static Complex operator * (Complex a, double b)
		{
			return new Complex(a.re * b, a.im * b);
		}
		public static Complex operator * (Complex a, Complex b)
		{
			return new Complex(a.re * b.re - a.im * b.im, a.re * b.im + a.im * b.re);
		}
		public static Complex operator * (double a, Complex b)
		{
			return new Complex(a * b.re, a * b.im);
		}

	    /* Arithmetic operations: division */
		public static Complex operator / (Complex a, double b)
		{
			if (System.Math.Abs(b) < Complex.threshold) throw new DivideByZeroException(String.Format("Operand is less than threshold ({0} < {1})", b, Complex.threshold));
			return new Complex(a.re / b, a.im / b);
		}		
		public static Complex operator / (Complex a, Complex b)
		{
			double norm = b.re * b.re + b.im * b.im;
			if (norm < Complex.threshold) throw new DivideByZeroException(String.Format("Operand is less than threshold (|({0}, {1})| < {2})", b.re, b.im, Complex.threshold));
			return new Complex(( a.re * b.re + a.im * b.im ) / norm, ( - a.re * b.im + a.im * b.re ) / norm);
		}
		public static Complex operator / (double a, Complex b)
		{
			double norm = b.re * b.re + b.im * b.im;
			if (norm < Complex.threshold) throw new DivideByZeroException(String.Format("Operand is less than threshold (|({0}, {1})| < {2})", b.re, b.im, Complex.threshold));
			return new Complex( a * b.re / norm, - a * b.im / norm);
		}
		
	    /* Other functions like absolute value, conjugation */
		public static double abs(Complex a)
		{
			return System.Math.Sqrt(a.re * a.re + a.im * a.im);
		}
		public double abs()
		{
			return Complex.abs(this);
		}
		public Complex conjg()
		{
			return new Complex(this.re, -this.im);
		}
		
		/* Complex constants */
	    public static Complex zero()
	    {
	        return new Complex(0,0);
	    }
	    public static Complex one()
	    {
	        return new Complex(1,0);
	    }
	    public static Complex aim()
	    {
	        return new Complex(0,1);
	    }
	    public static Complex aim(int degree)
	    {
			switch (degree % 4)
			{
			case 3:  return new Complex( 0,-1);
			case 2:  return new Complex(-1, 0);
			case 1:  return new Complex( 0, 1);
			default: return new Complex( 1, 0);
			}
	    }
		
	    /* Comparision with complex number */
	    public bool Equals(Complex b)
	    {
	        return Complex.abs(this-b) <= Complex.threshold;
	    }
	}
}