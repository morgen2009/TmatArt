using System;
using NUnit.Framework;
using TmatArt.Numeric;

namespace TmatArt.Test
{
	[TestFixture()]
	public class TestComplex
	{
		[Test()]
		public void testConstruct ()
		{
			Complex x = new Complex(1,3);
			Assert.AreEqual(x.re, 1);
			Assert.AreEqual(x.im, 3);

			Complex y = Complex.c(1,3);
			Assert.AreEqual(y.re, 1);
			Assert.AreEqual(y.im, 3);
		}
		
		[Test()]
		public void testArithmetic ()
		{
			// arithmetic +
			Assert.AreEqual(Complex.c(1,1) + Complex.c(1.1, 2.3), Complex.c(2.1, 3.3));
			
			// arithmetic -
			Assert.IsTrue(Complex.c(1.9, 1.7).Equals(Complex.c(3,4) - Complex.c(1.1, 2.3)));
			
			// arithmetic *
			Assert.IsTrue(Complex.c(-5.9, 11.3).Equals(Complex.c(3,4) * Complex.c(1.1, 2.3)));

			// arithmetic /
			Assert.IsTrue(Complex.c(12.5 / 6.5, -2.5 / 6.5).Equals(Complex.c(3,4) / Complex.c(1.1, 2.3)));
			Assert.IsTrue(Complex.c(1.1 / 6.5, -2.3 / 6.5).Equals(1E0 / Complex.c(1.1, 2.3)));

			// arithmetic conjg
			Assert.IsTrue(Complex.c(1.5, -2.5).conjg().Equals(Complex.c(1.5, 2.5)));

			// arithmetic aim
			Assert.IsTrue(Complex.aim(4).Equals(Complex.one()));
			Assert.IsTrue(Complex.aim(1).Equals(Complex.aim()));
		}
		
		[Test()]
		public void testTrigonometric ()
		{
			// real argument
			Assert.IsTrue(Complex.c(Numeric.Math.Sin(System.Math.PI / 6)).Equals(Complex.one() / 2));
			Assert.IsTrue(Complex.c(Numeric.Math.Cos(System.Math.PI / 3)).Equals(Complex.one() / 2));
			Assert.IsTrue(Complex.c(Numeric.Math.Tan(System.Math.PI / 4)).Equals(Complex.one()));
			
			// complex argument
			Complex r = new Complex();
			r.re = System.Math.Sin(1E0) * (System.Math.E + 1E0/System.Math.E) / 2E0;
			r.im = System.Math.Cos(1E0) * (System.Math.E - 1E0/System.Math.E) / 2E0;
			Assert.IsTrue(Numeric.Math.Sin(Complex.c(1,1)).Equals(r));
			
			r = Complex.c(1,1);
			Assert.IsTrue(Numeric.Math.Sin(r).Equals(Numeric.Math.Cos(r-System.Math.PI/2)));
			Assert.IsTrue(Numeric.Math.Tan(r).Equals(Numeric.Math.Sin(r) / Numeric.Math.Cos(r)));
		}
		
		[Test()]
		public void testExponent ()
		{
			// real argument
			Assert.IsTrue(Complex.c(Numeric.Math.Log(System.Math.E)).Equals(Complex.one()));
			Assert.IsTrue(Complex.c(Numeric.Math.Exp(1)).Equals(Complex.c(System.Math.E)));
			
			// complex argument
			Complex r = new Complex(2*System.Math.Cos(1.1), 2*System.Math.Sin(1.1));
			Assert.IsTrue(Numeric.Math.Log(r).Equals(Complex.c(System.Math.Log(2), 1.1)));
			
			r = new Complex(2*System.Math.Cos(1.1), 2*System.Math.Sin(1.1));
			Assert.IsTrue(Numeric.Math.Exp(Complex.c(System.Math.Log(2),1.1)).Equals(r));
		}
	}
	
	public static class ComplexTestExtension
	{
		public static void print(this Complex number)
		{
			Console.WriteLine(String.Format("Complex number: {0}+j{1}", number.re, number.im));
		}
	}
}

