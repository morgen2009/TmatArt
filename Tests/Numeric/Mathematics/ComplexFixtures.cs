using System;
using NUnit.Framework;
using System.Collections;

namespace TmatArt.Numeric.Mathematics
{
	public class ComplexFixtures
	{
		public enum Operation { Add, Substract, Multiply, Divide, Negate, Inverse, Conjugate, Abs };

		public static IEnumerable BinaryOperations
		{
			get {
				yield return new TestCaseData(new Complex(1.1,1.2), new Complex(2.1, 3.5), Operation.Add, new Complex(3.2, 4.7)).SetName("add");
				yield return new TestCaseData(new Complex(3.0, 4.0), new Complex(1.1, 2.3), Operation.Substract, new Complex(1.9, 1.7)).SetName("sub");
				yield return new TestCaseData(new Complex(3.0, 4.0), new Complex(1.1, 2.3), Operation.Multiply, new Complex(-5.9, 11.3)).SetName("mult");
				yield return new TestCaseData(new Complex(3.0, 4.0), new Complex(1.1, 2.3), Operation.Divide, new Complex(12.5/6.5, -2.5/6.5)).SetName("div");
			}
		}

		public static IEnumerable UnaryOperations
		{
			get {
				yield return new TestCaseData(new Complex(1.1, 1.2), Operation.Negate, new Complex(-1.1, -1.2)).SetName("neg");
				yield return new TestCaseData(new Complex(1.1, 2.3), Operation.Inverse, new Complex(1.1/6.5, -2.3/6.5)).SetName("inv");
				yield return new TestCaseData(new Complex(1.1, 2.3), Operation.Conjugate, new Complex(1.1, -2.3)).SetName("conjg");
				yield return new TestCaseData(new Complex(1.1, 2.3), Operation.Abs, new Complex(System.Math.Sqrt(6.5))).SetName("abs");
			}
		}

		public enum Function { Sin, Cos, Tan, Exp, Log };

		public static IEnumerable MathFunctions
		{
			get {
				yield return new TestCaseData(new Complex(System.Math.PI / 6), Function.Sin, Complex.ONE / 2).SetName("sin, real");
				yield return new TestCaseData(new Complex(System.Math.PI / 3), Function.Cos, Complex.ONE / 2).SetName("cos, real");
				yield return new TestCaseData(new Complex(System.Math.PI / 4), Function.Tan, Complex.ONE).SetName("tan, real");

				Complex r = new Complex();
				r.re = System.Math.Sin(1E0) * (System.Math.E + 1E0/System.Math.E) / 2E0;
				r.im = System.Math.Cos(1E0) * (System.Math.E - 1E0/System.Math.E) / 2E0;
				yield return new TestCaseData(new Complex(1, 1), Function.Sin, r).SetName("sin, complex");

				r.re = System.Math.Cos(1E0) * (System.Math.E + 1E0/System.Math.E) / 2E0;
				r.im = -System.Math.Sin(1E0) * (System.Math.E - 1E0/System.Math.E) / 2E0;
				yield return new TestCaseData(new Complex(1, 1), Function.Cos, r).SetName("cos, complex");

				yield return new TestCaseData(new Complex(System.Math.E), Function.Log, Complex.ONE).SetName("log, real");
				yield return new TestCaseData(new Complex(1), Function.Exp, new Complex(System.Math.E)).SetName("exp, real");

				r = new Complex(2*System.Math.Cos(1.1), 2*System.Math.Sin(1.1));
				yield return new TestCaseData(r, Function.Log, new Complex(System.Math.Log(2), 1.1)).SetName("log, complex");

				r = new Complex(2*System.Math.Cos(1.1), 2*System.Math.Sin(1.1));
				yield return new TestCaseData(new Complex(System.Math.Log(2), 1.1), Function.Exp, r).SetName("exp, complex");
			}
		}

		public static IEnumerable SqrtCases
		{
			get {
				yield return new TestCaseData( 0,  0);
				yield return new TestCaseData( 1,  0);
				yield return new TestCaseData( 1,  1);
				yield return new TestCaseData( 0,  1);
				yield return new TestCaseData(-1,  1);
				yield return new TestCaseData(-1,  0);
				yield return new TestCaseData(-1, -1);
				yield return new TestCaseData( 0, -1);
				yield return new TestCaseData( 1, -1);
			}
		}
	}
}

