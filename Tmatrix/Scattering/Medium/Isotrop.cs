using System;
using TmatArt.Numeric.Mathematics;
using TmatArt.Geometry;

namespace TmatArt.Scattering.Medium
{
	public class Isotrop
	{
		public readonly Complex index;
		
		public Isotrop (Complex index)
		{
			this.index = index;
		}
			
		public Isotrop (Complex eps, Complex mu)
		{
			this.index = Complex.Math.Sqrt(eps * mu);
		}

		/// <summary>
		/// Default medium
		/// </summary>
		public static Isotrop Default = new Isotrop(1.0);
	}
}

