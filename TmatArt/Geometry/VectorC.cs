using System;
using TmatArt.Numeric;

namespace TmatArt.Geometry
{
	public class VectorC
	{
		protected Complex x, y, z;
		
		public VectorC (Complex x, Complex y, Complex z)
		{
			this.x = x;
			this.y = y;
			this.z = z;
		}
		
		/* summation "+" */
		public static VectorC operator + (VectorC a, VectorC b)
		{
			return new VectorC(a.x + b.x, a.y + b.y, a.z + b.z);
		}

	    /* subtraction "-" */
		public static VectorC operator - (VectorC a, VectorC b)
		{
			return new VectorC(a.x - b.x, a.y - b.y, a.z - b.z);
		}
		
	    /* multiplication with number "*" */
		public static VectorC operator * (VectorC a, Complex b)
		{
			return new VectorC(a.x * b, a.y * b, a.z * b);
		}
		public static VectorC operator * (Complex a, VectorC b)
		{
			return new VectorC(a * b.x, a * b.y, a * b.z);
		}
		public static VectorC operator / (VectorC a, Complex b)
		{
			return new VectorC(a.x / b, a.y / b, a.z / b);
		}
		
	    /* scalar product "*" */
		public static Complex operator * (VectorC a, VectorC b)
		{
			return a.x * b.x.conjg() + a.y * b.y.conjg() + a.z * b.z.conjg();
		}

	    /* vector product "^" */
		public static VectorC operator ^ (VectorC a, VectorC b)
		{
			return new VectorC(a.y * b.z.conjg() - a.z * b.y.conjg(), - a.x * b.z.conjg() + a.z * b.x.conjg(), a.x * b.y.conjg() - a.y * b.x.conjg());	
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
	}
}

