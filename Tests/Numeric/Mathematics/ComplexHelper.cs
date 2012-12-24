using System;
using NUnit.Framework;
using NUnit.Framework.Constraints;

namespace TmatArt.Numeric.Mathematics
{
	public static class ComplexHelper
	{
		public static String format = @"re: {0}, im: {1}";

		/// <summary>
		/// Output complex number
		/// </summary>
		/// <param name="v">Complex number</param>
		public static String info(this Complex v)
		{
			return String.Format(ComplexHelper.format, v.re, v.im);
		}
	}

	public class ComplexConstraint: Constraint
	{
		private Complex expected;
		private double  threshold;

		/// <summary>
		/// Initializes a new instance of the constraint class for complex numbers.
		/// </summary>
		/// <param name="expected">Expected value.</param>
		/// <param name="threshold">Threshold.</param>
		public ComplexConstraint (Complex expected, double threshold = 1E-7)
		{
			this.expected  = expected;
			this.threshold = threshold;
		}

		private Complex GetActual()
		{
			return (Complex)this.actual;
		}
		
		private Complex GetExpected()
		{
			return (Complex)this.expected;
		}

		/// <see cref="Constraint.Matches"/>
		public override bool Matches (object actual)
		{
			if (actual.GetType() != typeof(Complex)) {
				throw new System.FormatException(actual.GetType().FullName);
			}
			this.actual = actual;

			return System.Math.Abs(this.GetExpected().re - this.GetActual().re) < this.threshold
				&& System.Math.Abs(this.GetExpected().im - this.GetActual().im) < this.threshold;
		}

		/// <see cref="Constraint.WriteDescriptionTo"/>
		public override void WriteDescriptionTo (MessageWriter writer)
		{
			writer.WriteExpectedValue(this.GetExpected().info());
		}

		public override void WriteActualValueTo (MessageWriter writer)
		{
			writer.WriteValue(this.GetActual().info());
		}
	}
}

