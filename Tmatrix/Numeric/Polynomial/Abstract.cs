using System;
using System.Linq;

namespace TmatArt.Numeric.Polynomial
{
	/**
	 * Abstract class for orthogonal polynomials
	 * 
	 * @author Vladimir Schmidt
	 * @date 28 Sep 2012
	 */
	public class Abstract
	{
		/**
		 * Left and right bounds of orthogonality range
		 */
		public virtual double left 
		{
			get
			{
				return double.NegativeInfinity;
			}
		}
		public virtual double right
		{
			get
			{
				return double.PositiveInfinity;
			}
		}

		/**
		 * Accuracy of polynomials roots
		 */
		public const double epsRoot = 1E-12;
		
		/**
		 * Value returned by compute method 
		 */
		public struct Value
		{
			/* degree of polynomial */
			public int n;
			/* value */
			public double p;
			/* derivation */
			public double dp;
		}
		
		/**
		 * Compute Legendre polynomial and its derivation for degrees 0..n
		 * 
		 * @param x  double argument
		 * @param n  int degree
		 * @return IEnumerable<Value>
		 */
		public virtual System.Collections.Generic.IEnumerable<Value> compute (double x, int n)
		{
			yield break;
		}

		/**
		 * Compute Legendre polynomial and its derivation for degree n
		 * 
		 * @param x  double argument
		 * @param n  int degree
		 * @return Value
		 */
		public Value computeOne (double x, int n)
		{
			Value val = new Value {
				n  = n,
				p  = 0,
				dp = 0
			};
			return this.compute(x, n).Where(v => v.n == n).DefaultIfEmpty(val).First();
		}

		/**
		 * Square of the polynomials norm
		 * 		\int_{left}^{right} [ F_n^m(x) ]^2 * w(x) dx
		 * 
		 * @param n int degree
		 * @return double
		 */
		public virtual double norm(int n)
		{
			return 1E0;
		}

		/**
		 * Cristoffell numbers of the polynomial
		 * 
		 * @param x double root of polynomial
		 * @param n int degree
		 * @return double
		 * 
		 * @see http://mathworld.wolfram.com/ChristoffelNumber.html
		 * @note
		 * 	\lambda_i = \frac{k_{n}}{k_{n-1}} \frac{ ||p_{n-1}||^2 }{p_{n-1}(x_i)p_n'(x_i)}
		 */
		public virtual double cristoffel(double x, int n)
		{
			return 0E0;
		}

		/**
		 * Roots of the polynomial
		 * 
		 * @param n int  degree
		 * @return IEnumerable<Value>
		 */
		public virtual System.Collections.Generic.IEnumerable<double> roots (int n)
		{
			yield break;
		}
		
		/**
		 * Compute roots of orthogonal polynomials sequentionally from the lower degree to the higher
		 * 
		 * @param xmin double left bound of the orthogonality range
		 * @param xmax double right bound of the orthogonality range
		 * @param n    int degree
		 * @return IEnumerable<Value>
		 */
		protected virtual System.Collections.Generic.IEnumerable<double> roots_interlacing(double xmin, double xmax, int n)
		{
			// no roots for zero-degree polynomial
			if (n <= 0) yield break;
			
			double x1  = xmin;
			double fx1 = this.computeOne(x1, n).p;
			
			foreach (double x in this.roots_interlacing(xmin, xmax, n-1).OrderBy(v => v))
			{
				double fx = this.computeOne(x, n).p;
				yield return this.root_secant(x1, x, fx1, fx, n);
				x1 = x;
				fx1 = fx;
			}
			
			double fx2 = this.computeOne(xmax, n).p;
			yield return this.root_secant(x1, xmax, fx1, fx2, n);
		}
		
		/**
		 * Compute root within the range [a,b] using secant method
		 * 
		 * @param a  double left bound of the range
		 * @param b  double right bound of the range
		 * @param fa double value of polynomial on the left bound of the range
		 * @param fb double value of polynomial on the right bound of the range
		 * @param n  int order
		 * @return double
		 * 
		 * @note The parameters fa and fb have to have different signs f(a) * f(b) < 0.
		 * Because of this the root has to exist. The accuracy of root estimation is defined
		 * by the this.epsRoot constant.
		 */
		private double root_secant(double a, double b, double fa, double fb, int n)
		{
			double x=a, x1, fx;
			
			// search one root of polynomial of degree n in the range [a,b]
			do
			{
				x1 = x;
				x  = a - (b-a) / (fb-fa) * fa;
				fx = this.compute(x, n).Where(v => v.n == n).First().p;
				if (System.Math.Sign(fx)*System.Math.Sign(fb) < 0) { a = x; fa = fx; }
				else
				if (System.Math.Sign(fx)*System.Math.Sign(fa) < 0) { b = x; fb = fx; }
				else x1 = x;
			} while (System.Math.Abs(x-x1) >= Abstract.epsRoot);
			
			return x;
		}
	}

}

