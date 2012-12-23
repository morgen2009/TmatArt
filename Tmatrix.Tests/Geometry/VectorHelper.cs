using System;

namespace TmatArt.Geometry
{
	public static class VectorTestHelper
	{
		/// <summary>
		/// Output Vector3c
		/// </summary>
		/// <param name="v">vector</param>
		public static Vector3c info(this Vector3c v)
		{
			Console.WriteLine(String.Format("Elements of vector {0}", v.GetHashCode()));
			Console.WriteLine(String.Format("\t v.x = {0}", v.x.info()));
			Console.WriteLine(String.Format("\t v.y = {0}", v.y.info()));
			Console.WriteLine(String.Format("\t v.z = {0}", v.z.info()));
		}
	}
}

