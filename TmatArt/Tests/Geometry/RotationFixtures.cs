using System;
using NUnit.Framework;
using TmatArt.Geometry;
using System.Collections;

namespace TmatArt.Tests.Geometry
{
	public class RotationFixtures
	{
		public static IEnumerable Negate
		{
			get {
				Vector3d e1 = new Vector3d(1,2,3);
				double rad = System.Math.PI / 180;
				
				yield return new TestCaseData(e1, new Euler(30*rad, 0*rad, 0*rad)).SetName("30/0/0");
				yield return new TestCaseData(e1, new Euler(0*rad, 30*rad, 0*rad)).SetName("0/30/0");
				yield return new TestCaseData(e1, new Euler(0*rad, 0*rad, 30*rad)).SetName("0/0/30");
				yield return new TestCaseData(e1, new Euler(12*rad, 36*rad, 52*rad)).SetName("12/36/52");
			}
		}

		public static IEnumerable Rotations
		{
			get {
				Vector3d e1 = new Vector3d(1,0,0);
				Vector3d e2 = new Vector3d(0,1,0);
				Vector3d e3 = new Vector3d(0,0,1);
				double deg = System.Math.PI / 180;
				
				yield return new TestCaseData(e1, new Euler(90*deg, 0*deg, 0*deg), -e2).SetName("e1==-e2");
				yield return new TestCaseData(e1, new Euler(180*deg, 0*deg, 0*deg), -e1).SetName("e1==-e1");
				yield return new TestCaseData(e1, new Euler(0*deg, 90*deg, 0*deg), e3).SetName("e1==e3");
				yield return new TestCaseData(e1, new Euler(90*deg, 90*deg, 90*deg), -e1).SetName("e1==-e1(a)");
			}
		}
	}
}