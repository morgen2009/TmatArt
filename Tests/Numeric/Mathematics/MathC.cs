using System;
using NUnit.Framework;

namespace TmatArt.Numeric.Mathematics
{
	[TestFixture()]
	public class ComplexTest
	{
		const double EqualityThreshold = 1E-10;

		[Test()]
		public void Construct ()
		{
			Complex x = new Complex(1,3);
			Assert.That(x, new ComplexConstraint(new Complex(1, 3)));
		}

		[Test, TestCaseSource(typeof(ComplexFixtures), "BinaryOperations")]
		public void Binary (Complex a, Complex b, ComplexFixtures.Operation op, Complex expected)
		{
			Complex res = Complex.ZERO;

			switch (op) {
			case ComplexFixtures.Operation.Add:       res = a+b; break;
			case ComplexFixtures.Operation.Substract: res = a-b; break;
			case ComplexFixtures.Operation.Multiply:  res = a*b; break;
			case ComplexFixtures.Operation.Divide:    res = a/b; break;
			}

			Assert.That(res, new ComplexConstraint(expected, ComplexTest.EqualityThreshold));
		}

		[Test, TestCaseSource(typeof(ComplexFixtures), "UnaryOperations")]
		public void Unary (Complex a, ComplexFixtures.Operation op, Complex expected)
		{
			Complex res = Complex.ZERO;

			switch (op) {
			case ComplexFixtures.Operation.Negate:    res = -a; break;
			case ComplexFixtures.Operation.Inverse:   res = a.Inverse(); break;
			case ComplexFixtures.Operation.Conjugate: res = Complex.Math.Conjugate(a); break;
			case ComplexFixtures.Operation.Abs:       res = Complex.Math.Abs(a); break;
			}

			Assert.That(res, new ComplexConstraint(expected, ComplexTest.EqualityThreshold));
		}

		[Test, TestCaseSource(typeof(ComplexFixtures), "MathFunctions")]
		public void Math (Complex a, ComplexFixtures.Function op, Complex expected)
		{
			Complex res = Complex.ZERO;
			
			switch (op) {
			case ComplexFixtures.Function.Sin: res = Complex.Math.Sin(a); break;
			case ComplexFixtures.Function.Cos: res = Complex.Math.Cos(a); break;
			case ComplexFixtures.Function.Tan: res = Complex.Math.Tan(a); break;
			case ComplexFixtures.Function.Exp: res = Complex.Math.Exp(a); break;
			case ComplexFixtures.Function.Log: res = Complex.Math.Log(a); break;
			}
			
			Assert.That(res, new ComplexConstraint(expected, ComplexTest.EqualityThreshold));
		}

		[Test()]
		public void Aim ()
		{
			/* degrees of imaginary identity */
			Assert.That(Complex.Math.Aim(4), new ComplexConstraint(Complex.ONE, ComplexTest.EqualityThreshold));
			Assert.That(Complex.Math.Aim(1), new ComplexConstraint(Complex.AIM, ComplexTest.EqualityThreshold));
		}

		[Test, TestCaseSource(typeof(ComplexFixtures), "SqrtCases")]
		public void Sqrt (double re, double im)
		{
			Complex r = new Complex(re, im);
			Complex s = Complex.Math.Sqrt(r);
			Assert.That(s*s, new ComplexConstraint(r, ComplexTest.EqualityThreshold));
		}

		[Test, TestCaseSource(typeof(ComplexFixtures), "SqrtCases")]
		public void Pow (double re, double im)
		{
			Complex r = new Complex(re, im);
			Complex s = Complex.Math.Pow(r, 2);
			Assert.That(s, new ComplexConstraint(r*r, ComplexTest.EqualityThreshold));
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

