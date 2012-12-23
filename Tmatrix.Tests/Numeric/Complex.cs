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
			Assert.AreEqual(x.re, 1);
			Assert.AreEqual(x.im, 3);
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

			AssertComplexExtension.AreEqual(expected, res, ComplexTest.EqualityThreshold);
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

			AssertComplexExtension.AreEqual(expected, res, ComplexTest.EqualityThreshold);
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
			
			AssertComplexExtension.AreEqual(expected, res, ComplexTest.EqualityThreshold);
		}

		[Test()]
		public void Aim ()
		{
			/* degrees of imaginary identity */
			AssertComplexExtension.AreEqual(Complex.ONE, Complex.Math.Aim(4), ComplexTest.EqualityThreshold);
			AssertComplexExtension.AreEqual(Complex.AIM, Complex.Math.Aim(1), ComplexTest.EqualityThreshold);

			/* some relations for trigonometric functions */
/*			Complex r = new Complex(1,1);
			MathC math = MathC.Instance();
			Assert.IsTrue(math.Sin(r).Equals(math.Cos(r-System.Math.PI/2)));
			Assert.IsTrue(math.Tan(r).Equals(math.Sin(r) / math.Cos(r)));*/
		}

		[Test, TestCaseSource(typeof(ComplexFixtures), "SqrtCases")]
		public void Sqrt (double re, double im)
		{
			Complex r = new Complex(re, im);
			Complex s = Complex.Math.Sqrt(r);
			AssertComplexExtension.AreEqual(r, s*s, ComplexTest.EqualityThreshold, "Sqrt");
		}

		[Test, TestCaseSource(typeof(ComplexFixtures), "SqrtCases")]
		public void Pow (double re, double im)
		{
			Complex r = new Complex(re, im);
			Complex s = Complex.Math.Pow(r, 2);
			AssertComplexExtension.AreEqual(r*r, s, ComplexTest.EqualityThreshold, "Pow");
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

