using System;
using TmatArt.Numeric.Mathematics;
using TmatArt.Geometry;

namespace TmatArt.Scattering.Medium
{
	public class Isotrop
	{
		public readonly Complex mr;
		
		public Isotrop (Complex mr)
		{
			this.mr = mr;
		}
			
		public Isotrop (Complex eps, Complex mu)
		{
			this.mr = Complex.Math.Sqrt(eps * mu);
		}
	}
}

