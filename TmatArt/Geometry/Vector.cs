using System;

namespace TmatArt.Geometry
{
	public class Vector
	{
		public double x, y, z;
		
		public Vector (double x, double y, double z)
		{
			this.x = x;
			this.y = y;
			this.z = z;
		}
		
		/* summation "+" */
		public static Vector operator + (Vector a, Vector b)
		{
			return new Vector(a.x + b.x, a.y + b.y, a.z + b.z);
		}

	    /* subtraction "-" */
		public static Vector operator - (Vector a, Vector b)
		{
			return new Vector(a.x - b.x, a.y - b.y, a.z - b.z);
		}
		
	    /* multiplication with number "*" */
		public static Vector operator * (Vector a, double b)
		{
			return new Vector(a.x * b, a.y * b, a.z * b);
		}
		public static Vector operator * (double a, Vector b)
		{
			return new Vector(a * b.x, a * b.y, a * b.z);
		}
		public static Vector operator / (Vector a, double b)
		{
			return new Vector(a.x / b, a.y / b, a.z / b);
		}
		
	    /* scalar product "*" */
		public static double operator * (Vector a, Vector b)
		{
			return a.x * b.x + a.y * b.y + a.z * b.z;
		}

	    /* vector product "^" */
		public static Vector operator ^ (Vector a, Vector b)
		{
			return new Vector(a.y * b.z - a.z * b.y, - a.x * b.z + a.z * b.x, a.x * b.y - a.y * b.x);	
		}
		
		/**
		 * rotate vector by Euler angles
		 * 
		 * @note it transforms to spherical basis, particulary
		 * 		e_x -(theta, phi)-> e_theta
		 * 		e_y -(theta, phi)-> e_phi
		 * 		e_z -(theta, phi)-> e_r
		 */
		public void rotate(Euler angle)
		{
			// todo
		}
		
		/* convert */
	    public static explicit operator VectorC(Vector a)
	    {
	        return new VectorC(a.x, a.y, a.z);
	    }
	}
}

