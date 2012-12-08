using System;

namespace TmatArt.Geometry
{
	public class VectorR
	{
		public double r, theta, phi;
		
		public VectorR (double r, double theta, double phi)
		{
			this.r     = r;
			this.theta = theta;
			this.phi   = phi;
		}
		
		/* convert */
	    public static explicit operator Vector(VectorR a)
	    {
			// todo
	        return new Vector(0,0,0);
	    }
	}
}

