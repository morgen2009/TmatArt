using System;
using TmatArt.Geometry;

namespace TmatArt.Scattering.Indexing.Mappers
{
	/**
	 * Mapper for general symmetry
	 * 
	 * @see inherited
	 * @date 02 Oct 2012
	 */
	public class General: Mapper
	{
		private int hashBlock(int n, int m, Index.Type l)
		{
			// rotational symmetry
			int c_n = symmetry.rotate_phi;
			if (c_n == 0) c_n = this.mrank * 2 + 2;
			int c_n_key = (m) % c_n;
			if (c_n_key < 0) c_n_key = (c_n_key + c_n) % c_n;
			
			// reflection, z-plane
			int s_h_key = symmetry.reflection_z ? (n+m+(int)l) % 2 : 0;
			if (s_h_key < 0) s_h_key = (s_h_key + 2) % 2;
				
			// reflection, y-plane
			int s_v_key = symmetry.reflection_y ? (m+(int)l) % 2 : 0;
			if (s_v_key < 0) s_v_key = (s_v_key + 2) % 2;
			
			// combine total key
			return (s_h_key*1+s_v_key*10+c_n_key*100);
		}
			
		/**
		 * Constructor
		 * 
		 * @param Symmetry symmetry
		 * @param int nrank
		 * @param int mrank
		 */
		public General (Symmetry symmetry, int nrank, int mrank) : base (symmetry, nrank, mrank)
		{
			// collect hashes of blocks
			System.Collections.Generic.Dictionary<int, int> keys = new System.Collections.Generic.Dictionary<int, int>();
			
			for (int il = 0; il <= 1; il++)
			{
				Index.Type l = (Index.Type)il;
				for (int n = 1; n <= nrank; n++)
				{
					int nm = Math.Min(n, mrank);
					for (int m = -nm; m <= nm; m++)
					{
						int key = this.hashBlock(n, m, l);
						if (!keys.ContainsKey(key)) keys.Add(key, 0);
						keys[key]++;
					}
				}
			}
			
			// create blocks
			System.Collections.Generic.Dictionary<int, int> map_block = new System.Collections.Generic.Dictionary<int, int>();
			this._blocks = new Block[keys.Count];
			int position = 0;
			int block_id = 0;
			foreach (int key in keys.Keys)
			{
				this._blocks[block_id] = new Block(this, position);
				position += keys[key];
				map_block[key] = block_id++;
			}
			
			// fill map
			for (int il = 0; il <= 1; il++)
			{
				Index.Type l = (Index.Type)il;
				for (int n = 1; n <= nrank; n++)
				{
					int nm = Math.Min(n, mrank);
					for (int m = -nm; m <= nm; m++)
					{
						// append index (n, m, l) into block
						block_id = map_block[this.hashBlock(n, m, l)];
						this.bind(block_id, n, m, l);
						
						// set parent block
						if (m < 0 && this._blocks[block_id].parent == null)
						{
							int block_id2 = map_block[this.hashBlock(n, -m, l)];
							this._blocks[block_id2].parent = this._blocks[block_id];
						}
					}
				}
			}
		}
		
		private void compress()
		{
		}
	}
	
	/**
	 * Factory class
	 * 
	 * @date 05 Oct 2012
	 */
	public class GeneralFactory: MapperFactory
	{
		/**
		 * Singleton pattern
		 */
		protected static GeneralFactory instance;
		protected GeneralFactory() {}
		public static MapperFactory getInstance()
		{
			if (GeneralFactory.instance == null)
				GeneralFactory.instance = new GeneralFactory();
			return GeneralFactory.instance;
		}
		
		/**
		 * @see inherited
		 */
		public Mapper createMapper(Symmetry symmetry, int nrank, int mrank)
		{
			return new General(symmetry, nrank, mrank);
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
