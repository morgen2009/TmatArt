using System;
using NUnit.Framework;
using TmatArt.Numeric;

namespace TmatArt.Test
{
	[TestFixture()]
	public class TestDouble
	{
		[Test()]
		public void TestPow ()
		{
			double x = 0.75, y = 1;
			for (int i = 0; i < 12; i++)
			{
				Console.WriteLine(String.Format("{0} {1} {2}", i, y, x.Pow(i)));
				Assert.AreEqual(y, x.Pow(i));
				y *= x;
			}
			//Assert.Fail();
		}
	}
}

