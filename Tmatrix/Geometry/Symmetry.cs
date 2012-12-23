using System;

namespace TmatArt.Geometry
{
	/**
	 * Class describing types of geometrical symmetries
	 * 
	 * @author Vladimir Schmidt
	 * @date 01 Oct 2012
	 */
	public class Symmetry
	{
		/* Reflection across X-plane yield the same object */
		public bool reflection_x = false;

		/* Reflection across Y-plane yield the same object */
		public bool reflection_y = false;
		
		/* Reflection across Z-plane yield the same object */
		public bool reflection_z = false;
		
		/**
		 * Rotation along Z-axis by the angle (2*pi/rotate_phi) yield the same object
		 * If rotate_phi = 0, then object is axial-symmetric
		 */
		public int  rotate_phi   = 1;

		/**
		 * If rotate_theta = 0, then object is spherical, otherwise rotate_theta = 1
		 */
		public int  rotate_theta = 1;
		
		/**
		 * Constructor
		 */
		public Symmetry ()
		{
		}
		
		/**
		 * Constructor
		 */
		public static Symmetry getInstance(String description = null)
		{
			/**
			 * @todo use text describtion of symmetry properties (optionally)
			 */
			return new Symmetry();
		}
		
		/**
		 * Define reflection symmetry
		 * 
		 * @param type RectectType plane for the reflection
		 * @return Symmetry self
		 */
		public enum RectectType { PlaneX, PlaneY, PlaneZ };
		public Symmetry Reflect(RectectType type)
		{
			switch (type)
			{
			case RectectType.PlaneX: this.reflection_x = true; break;
			case RectectType.PlaneY: this.reflection_y = true; break;
			case RectectType.PlaneZ: this.reflection_z = true; break;
			};
			return this;
		}
		
		/**
		 * Define rotation symmetry
		 * 
		 * @param type RotationType axis for the rotation
		 * @param times int rotation by the angle (2pi/times)
		 * @return Symmetry self
		 */
		public enum RotateType { All, AxisZ };
		public Symmetry Rotate(RotateType type, int times = 0)
		{
			switch (type)
			{
			case RotateType.All:
				this.rotate_phi   = 0;
				this.rotate_theta = 0;
				break;
			case RotateType.AxisZ:
				this.rotate_phi = times;
				break;
			}
			return this;
		}
		
		/**
		 * Check if symmetry is spherical
		 */
		public bool isSpherical()
		{
			return this.rotate_theta == 0 && this.rotate_phi == 0;
		}
		
		/**
		 * Check if symmetry is axial-symemtrical
		 */
		public bool isAxial()
		{
			return this.rotate_theta != 0 && this.rotate_phi == 0;
		}
	}
}

