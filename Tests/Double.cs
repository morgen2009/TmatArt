using System;
using NUnit.Framework;

namespace TmatArt.Numeric
{
	[TestFixture()]
	public class DoubleTest
	{
		[Test()]
		public void Pow ()
		{
			double x = 0.75, y = 1;
			for (int i = 0; i < 12; i++)
			{
				Console.WriteLine(String.Format("{0} {1} {2}", i, y, x.Pow(i)));
				Assert.AreEqual(y, x.Pow(i));
				y *= x;
			}
		}
	}
}

