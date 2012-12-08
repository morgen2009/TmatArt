using System;
using TmatArt.Geometry;

namespace TmatArt.Scattering.Indexing.Mappers
{
	/**
	 * Mapper for spherical symmetry
	 * 
	 * @see inherited
	 * @date 07 Oct 2012
	 */
	public class Sphere: Mapper
	{
		/**
		 * Constructor
		 * 
		 * @param Symmetry symmetry
		 * @param int nrank
		 * @param int mrank
		 */
		public Sphere (Symmetry symmetry, int nrank, int mrank) : base (symmetry, nrank, mrank)
		{
			// blocks
			this._blocks = new Block[2*this.nmax];
			
			// positions
			int position = 0;
			for (int il = 0; il <= 1; il++)
			{
				Index.Type l = (Index.Type)il;
				for (int m = 0; m <= mrank; m++)
					for (int ml = m; ml >= -m; ml -= Math.Max(2*m, 1))
						for (int n = Math.Max(Math.Abs(m), 1); n <= nrank; n++)
						{
							this._blocks[position] = new Block(this, position);
							this.bind(position, n, ml, l);
						}
			}
		}
	}
	
	/**
	 * Factory class
	 * 
	 * @date 07 Oct 2012
	 */
	public class SphereFactory: MapperFactory
	{
		/**
		 * Singleton pattern
		 */
		protected static SphereFactory instance;
		protected SphereFactory() {}
		public static MapperFactory getInstance()
		{
			if (SphereFactory.instance == null)
				SphereFactory.instance = new SphereFactory();
			return SphereFactory.instance;
		}
		
		/**
		 * @see inherited
		 */
		public Mapper createMapper(Symmetry symmetry, int nrank, int mrank)
		{
			return new Sphere(symmetry, nrank, mrank);
		}
		
		/**
		 * @see inherited
		 */
		public int weighSymmetry(Symmetry symmetry)
		{
			return 1;
		}
	}
}
