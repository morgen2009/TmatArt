using System;
using NUnit.Framework;

namespace TmatArt.Numeric.Mathematics
{
	[TestFixture()]
	public class ComplexTest
	{
		const double EqualityThreshold = 1E-10;

		[Test]public void Initialization ()
		{
			Complex x = new Complex(1,3);
			Assert.That(x, new ComplexConstraint(new Complex(1, 3)));
		}

		[Test]public void Operators()
		{
			Complex a = new Complex(3.0, 4.0);
			Complex b = new Complex(1.1, 2.3);
			
			Assert.That(a+b, new ComplexConstraint(new Complex(4.1, 6.3)), "a+b");
			Assert.That(a-b, new ComplexConstraint(new Complex(1.9, 1.7)), "a-b");
			Assert.That(a*b, new ComplexConstraint(new Complex(-5.9, 11.3)), "a*b");
			Assert.That(a/b, new ComplexConstraint(new Complex(12.5/6.5, -2.5/6.5)), "a/b");
			Assert.That(-a, new ComplexConstraint(new Complex(-3.0, -4.0)), "-a");
			Assert.That(b.Inverse(), new ComplexConstraint(new Complex(1.1/6.5, -2.3/6.5)), "b^(-1)");
			Assert.That(a.Conjugate(), new ComplexConstraint(new Complex(3.0, -4.0)), "a^*");
		}
		
		[Test()]public void DegreesOfJ ()
		{
			Assert.That(Complex.Math.Aim(4), new ComplexConstraint(Complex.ONE, ComplexTest.EqualityThreshold));
			Assert.That(Complex.Math.Aim(1), new ComplexConstraint(Complex.AIM, ComplexTest.EqualityThreshold));
		}
	}

	public static class AssertComplexExtension
	{
		public static void AreEqual (Complex expected, Complex actual, double delta, String message)
		{
			Assert.AreEqual(expected.re, actual.re, delta, message + "[real]");
			Assert.AreEqual(expected.im, actual.im, delta, message + "[imaginary]");
		}

		public static void AreEqual (Complex expected, Complex actual, double delta)
		{
			Assert.AreEqual(expected.re, actual.re, delta, "real");
			Assert.AreEqual(expected.im, actual.im, delta, "imaginary");
		}

		public static void print(this Complex number)
		{
			Console.WriteLine(String.Format("Complex number: {0}+j{1}", number.re, number.im));
		}
	}
}

