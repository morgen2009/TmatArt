using System;
using TmatArt.Numeric;
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
			this.mr = MathCmplx.Sqrt(eps * mu);
		}
	}
}

