using System;
using TmatArt.Geometry;

namespace TmatArt.Scattering.Indexing.Mappers
{
	/**
	 * Mapper for general symmetry using items indexing like A. Doicu
	 * 
	 * @see inherited
	 * @date 02 Oct 2012
	 */
	public class GeneralDoicu: Mapper
	{
		/**
		 * Constructor
		 * 
		 * @param Symmetry symmetry
		 * @param int nrank
		 * @param int mrank
		 */
		public GeneralDoicu (Symmetry symmetry, int nrank, int mrank) : base (symmetry, nrank, mrank)
		{
			// blocks
			this._blocks = new Block[] { new Block(this, 0) };
			
			// positions
			for (int il = 0; il <= 1; il++)
			{
				Index.Type l = (Index.Type)il;
				for (int m = 0; m <= mrank; m++)
					for (int ml = m; ml >= -m; ml -= Math.Max(2*m, 1))
						for (int n = Math.Max(Math.Abs(m), 1); n <= nrank; n++)
							this.bind(0, n, ml, l);
			}
		}
	}
	
	/**
	 * Factory class
	 * 
	 * @date 05 Oct 2012
	 */
	public class GeneralDoicuFactory: MapperFactory
	{
		/**
		 * Singleton pattern
		 */
		protected static GeneralDoicuFactory instance;
		protected GeneralDoicuFactory() {}
		public static MapperFactory getInstance()
		{
			if (GeneralDoicuFactory.instance == null)
				GeneralDoicuFactory.instance = new GeneralDoicuFactory();
			return GeneralDoicuFactory.instance;
		}
		
		/**
		 * @see inherited
		 */
		public Mapper createMapper(Symmetry symmetry, int nrank, int mrank)
		{
			return new GeneralDoicu(symmetry, nrank, mrank);
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
