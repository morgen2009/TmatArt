using System;
using NUnit.Framework;
using TmatArt.Numeric.Mathematics;

namespace TmatArt.Geometry
{
	[TestFixture()]
	public class RotationTest
	{
		[Test()]
		public void RotationAxis ()
		{
			Vector3d v = new Vector3d(1, 0, 0);
			double sqrt2 = System.Math.Sqrt(2) / 2;
			
			TestRotExtension.AreEqual(new Vector3d(1, 0, 0), v.Rotate(Axis3Name.X, System.Math.PI / 4).debug(), "RotateX");
			TestRotExtension.AreEqual(new Vector3d(sqrt2, 0, sqrt2), v.Rotate(Axis3Name.Y, System.Math.PI / 4).debug(), "RotateY");
			TestRotExtension.AreEqual(new Vector3d(sqrt2, -sqrt2, 0), v.Rotate(Axis3Name.Z, System.Math.PI / 4).debug(), "RotateZ");
		}

		[Test, TestCaseSource(typeof(RotationFixtures), "Negate")]
		public void EulerNegate (Vector3d v, Euler e)
		{
			Vector3d v1 = v.Rotate(e).Rotate(e.Negate()).debug();
			TestRotExtension.AreEqual(v, v1);

			v1 = v.Rotate(e+e.Negate()).debug();
			TestRotExtension.AreEqual(v, v1);
		}

		[Test()]
		public void EulerAdd ()
		{
			double rad = System.Math.PI / 180;
			Euler u1 = new Euler(330*rad, 12*rad, 350*rad);
			Euler u2 = new Euler(60*rad, 20*rad, 23*rad);
			Vector3d v = new Vector3d(1, 2, 3);

			TestRotExtension.AreEqual(v.Rotate(u1).Rotate(u2), v.Rotate(u1+u2));
		}

		[Test, TestCaseSource(typeof(RotationFixtures), "Rotations")]
		public void EulerRotation (Vector3d source, Euler e, Vector3d expected)
		{
			Vector3d actual = source.Rotate(e).debug();
			TestRotExtension.AreEqual(actual, expected);
			//Assert.Fail();
		}
	}

	public static class TestRotExtension
	{
		const double threshold = 1E-5;

		public static Vector3d debug(this Vector3d v)
		{
			Console.WriteLine(String.Format("x:{0}, y:{1}, z:{2}", v.x, v.y, v.z));
			return v;
		}
		
		public static Euler debug(this Euler u)
		{
			double deg = 180 / System.Math.PI;
			Console.WriteLine(String.Format("alpha:{0}, beta:{1}, gamma:{2}", u.alpha*deg, u.beta*deg, u.gamma*deg));
			return u;
		}
		
		public static void AreEqual(Vector3d expected, Vector3d actual, String message = "")
		{
			Assert.AreEqual(expected.x, actual.x, TestRotExtension.threshold, message + "[x]");
			Assert.AreEqual(expected.y, actual.y, TestRotExtension.threshold, message + "[y]");
			Assert.AreEqual(expected.z, actual.z, TestRotExtension.threshold, message + "[z]");
		}
	}
}