using System;
using NUnit.Framework.Constraints;
using TmatArt.Numeric.Mathematics;

namespace TmatArt.Geometry
{
	public static class Vector3cHelper
	{
		public static String formatHeader = @"Vector";
		public static String formatItem   = Environment.NewLine + @"   {0} = ({1})";

		/// <summary>
		/// Output Vector3c
		/// </summary>
		/// <param name="v">vector</param>
		public static String info(this Vector3c v)
		{
			String res = String.Format(Vector3cHelper.formatHeader);
			res = res + String.Format(Vector3cHelper.formatItem, "x", v.x.info());
			res = res + String.Format(Vector3cHelper.formatItem, "y", v.y.info());
			res = res + String.Format(Vector3cHelper.formatItem, "z", v.z.info());

			return res;
		}
	}

	public class Vector3cConstraint: Constraint
	{
		private Vector3c expected;
		private double  threshold;
		
		/// <summary>
		/// Initializes a new instance of the constraint class for Vector3c.
		/// </summary>
		/// <param name="expected">Expected value.</param>
		/// <param name="threshold">Threshold.</param>
		public Vector3cConstraint (Vector3c expected, double threshold = 1E-7)
		{
			this.expected  = expected;
			this.threshold = threshold;
		}
		
		private Vector3c GetActual()
		{
			return (Vector3c)this.actual;
		}
		
		private Vector3c GetExpected()
		{
			return (Vector3c)this.expected;
		}
		
		/// <see cref="Constraint.Matches"/>
		public override bool Matches (object actual)
		{
			if (actual.GetType() != typeof(Vector3c)) {
				throw new System.FormatException(actual.GetType().FullName);
			}
			this.actual = actual;

			Vector3c vres = this.GetExpected() - this.GetActual();
			Complex res = vres.Length();

			return System.Math.Abs(res.re) < this.threshold;
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

