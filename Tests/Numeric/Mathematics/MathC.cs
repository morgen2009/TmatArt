using System;
using NUnit.Framework;
using System.Collections.Generic;

namespace TmatArt.Numeric.Mathematics
{
	[TestFixture()]
	public class MathCTest
	{
		const double EqualityThreshold = 1E-10;
		const double deg2rad = System.Math.PI / 180;
		const double rad2deg = 180 / System.Math.PI;

		public enum Function { Sin, Cos, Tan, Exp, Log, Acos, Asin, Atan, Sqrt, Pow };
		private Dictionary<Function, Func<Complex, Complex>> cmplxFunc;
		private Dictionary<Function, Func<double, double>> realFunc;
		private Dictionary<Function, Function> inverseFunc;

		public MathCTest()
		{
			cmplxFunc = new Dictionary<Function, Func<Complex, Complex>>();
			cmplxFunc.Add(Function.Cos, Complex.Math.Cos);
			cmplxFunc.Add(Function.Sin, Complex.Math.Sin);
			cmplxFunc.Add(Function.Tan, Complex.Math.Tan);
			cmplxFunc.Add(Function.Acos,Complex.Math.Acos);
			cmplxFunc.Add(Function.Asin,Complex.Math.Asin);
			cmplxFunc.Add(Function.Atan,Complex.Math.Atan);
			cmplxFunc.Add(Function.Exp, Complex.Math.Exp);
			cmplxFunc.Add(Function.Log, Complex.Math.Log);
			cmplxFunc.Add(Function.Sqrt,Complex.Math.Sqrt);

			realFunc  = new Dictionary<Function, Func<double, double>>();
			realFunc.Add(Function.Cos, System.Math.Cos);
			realFunc.Add(Function.Sin, System.Math.Sin);
			realFunc.Add(Function.Tan, System.Math.Tan);
			realFunc.Add(Function.Acos,System.Math.Acos);
			realFunc.Add(Function.Asin,System.Math.Asin);
			realFunc.Add(Function.Atan,System.Math.Atan);
			realFunc.Add(Function.Exp, System.Math.Exp);
			realFunc.Add(Function.Log, System.Math.Log);
			realFunc.Add(Function.Sqrt,System.Math.Sqrt);

			inverseFunc = new Dictionary<Function, Function>();
			inverseFunc.Add(Function.Cos, Function.Acos);
			inverseFunc.Add(Function.Sin, Function.Asin);
			inverseFunc.Add(Function.Tan, Function.Atan);
			inverseFunc.Add(Function.Exp, Function.Log);
			inverseFunc.Add(Function.Pow, Function.Sqrt);
		}

		[Test]
		public void Sin_Complex ()
		{
			Complex expected = new Complex() {
				re = System.Math.Sin(1E0) * (System.Math.E + 1E0/System.Math.E) / 2E0,
				im = System.Math.Cos(1E0) * (System.Math.E - 1E0/System.Math.E) / 2E0
			};
			Assert.That(Complex.Math.Sin(new Complex(1,1)), new ComplexConstraint(expected));
		}
		
		[Test]
		public void Cos_Complex ()
		{
			Complex expected = new Complex() {
				re =  System.Math.Cos(1E0) * (System.Math.E + 1E0/System.Math.E) / 2E0,
				im = -System.Math.Sin(1E0) * (System.Math.E - 1E0/System.Math.E) / 2E0
			};
			Assert.That(Complex.Math.Cos(new Complex(1,1)), new ComplexConstraint(expected));
		}

		[Test]
		public void Trigonom_Real ([Values(-15, 0, 15, 135, 220)]double arg, [Values(Function.Cos, Function.Sin, Function.Tan)] Function func)
		{
			Complex actual   = this.cmplxFunc[func](arg * System.Math.PI / 180);
			Complex expected = this.realFunc[func](arg * System.Math.PI / 180);
			Assert.That(actual, new ComplexConstraint(expected));
		}
		
		[Test]
		public void Trigonom_Real_Inverse (
			[Values(-15, 15, 135, 220)] double arg,
			[Values(Function.Cos, Function.Sin, Function.Tan)] Function func)
		{
			Complex argument = arg * deg2rad;
			Complex direct   = this.cmplxFunc[func](argument);
			Complex inverse  = this.cmplxFunc[func](this.cmplxFunc[this.inverseFunc[func]](direct));
			Assert.That(direct, new ComplexConstraint(inverse));
		}
		
		[Test]
		public void Trigonom_Complex_Inverse (
			[Values(0.5, 1, 2)] double norm,
			[Values(-15, 15, 135, 220)] double arg,
			[Values(Function.Cos, Function.Sin, Function.Tan)] Function func)
		{
			Complex argument = Complex.Euler(norm, arg * deg2rad);
			Complex direct   = this.cmplxFunc[func](argument);
			Complex inverse  = this.cmplxFunc[func](this.cmplxFunc[this.inverseFunc[func]](direct));
			Assert.That(direct, new ComplexConstraint(inverse));
		}

		[Test]
		public void Log_Real ([Values(1, 2)]double arg, [Values(Function.Log, Function.Exp)] Function func)
		{
			Assume.That( arg > 0 );
			Complex actual   = this.cmplxFunc[func](arg);
			Complex expected = this.realFunc[func](arg);
			Assert.That(actual, new ComplexConstraint(expected));
		}
		
		[Test]
		public void Log_Real_Inverse (
			[Values(0.66, 1, 2)] double norm,
			[Values(-15, 15, 135, 220)] double arg,
			[Values(Function.Exp)] Function func)
		{
			Complex argument = Complex.Euler(norm, arg * deg2rad);
			Complex direct   = this.cmplxFunc[func](argument);
			Complex inverse  = this.cmplxFunc[func](this.cmplxFunc[this.inverseFunc[func]](direct));
			Assert.That(direct, new ComplexConstraint(inverse));
		}
		
		[Test]
		public void Pow_Complex (
			[Values(0.66, 1, 2)] double norm,
			[Values(-15, 15, 135, 220)] double arg)
		{
			Complex argument = Complex.Euler(norm, arg * deg2rad);
			Complex expected = argument * argument;
			Complex actual   = Complex.Math.Pow(argument, 2);;
			Assert.That(actual, new ComplexConstraint(expected));
		}
		
		[Test]
		public void Sqrt_Complex (
			[Values(0.66, 1, 2)] double norm,
			[Values(-15, 15, 135, 220)] double arg)
		{
			Complex argument = Complex.Euler(norm, arg * deg2rad);
			Complex expected = argument;
			Complex actual   = Complex.Math.Sqrt(argument);
			Assert.That(actual*actual, new ComplexConstraint(expected));
		}
		
		[Test]
		public void Abs ()
		{
			Complex argument = new Complex(1.1, 2.3);
			Complex expected = System.Math.Sqrt(6.5);
			Complex actual   = Complex.Math.Abs(argument);
			Assert.That(actual, new ComplexConstraint(expected));
		}
	}
}
