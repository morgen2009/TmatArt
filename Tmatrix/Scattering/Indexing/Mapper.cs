using System;
using TmatArt.Geometry;
using System.Collections.Generic;

namespace TmatArt.Scattering.Indexing
{
	/**
	 * Class describing the relation between indexes (n,m,l) and
	 * position of element in the sequence (e.q. vector)
	 * 
	 * @date 02 Oct 2012
	 */
	public abstract class Mapper
	{
		/* Paramenter Nrank (1 <= n <= Nrank) */
		public readonly int nrank;
		
		/* Paramenter Mrank (-Mrank <= m <= Mrank) */
		public readonly int mrank;
		
		/**
		 * Total number of different sets (n,m)
		 * where 1 <= n <= Nrank, -Mrank <= m <= Mrank, -n <= m <= n
		 */
		public readonly int nmax;
		
		/* Sysmmetry class determining mapping */
		public readonly Symmetry symmetry;
		
		/* Blocks */
		protected Block[] _blocks;
		
		/**
		 * Iterate over blocks
		 */
		public System.Collections.Generic.IEnumerable<Block> blocks()
		{
			foreach (Block block in this._blocks)
				yield return block;
		}
		public Block blocks(int id)
		{
			return this._blocks[id];
		}
				
		/* Mapping position => hash of index(n,m,l)*/
		protected int[] map_inv;
		
		/* Mapping hash of index(n,m,n) => index(n,m,l) */
		protected Index[] map;
		
		/**
		 * Constructor
		 * 
		 * @param Symmetry symmetry
		 * @param int nrank
		 * @param int mrank
		 */
		protected Mapper (Symmetry symmetry, int nrank, int mrank)
		{
			this.nrank = nrank;
			this.mrank = mrank;
			this.symmetry = symmetry;
			this.nmax  = 2*nrank*mrank - mrank * (mrank - 1) + nrank;
			this.map_inv = new int[2*this.nmax];
			this.map     = new Index[2*this.nmax];
		}
		
		/**
		 * Return number of elements in the sequence
		 * 
		 * @return int
		 */
		public int count()
		{
			return 2*this.nmax;
		}
		
		/**
		 * Return integer uniquelly determing the index (n,m,l)
		 *
		 * @param int n
		 * @param int m
		 * @param Index.Type l
		 * @return int
		 */
		protected int hashIndex(int n, int m, Index.Type l)
		{
			// check input
			int ml = Math.Abs(m);
			if (n <= 0)  throw new ArgumentOutOfRangeException("n <= 0");
			if (n > this.nrank)  throw new ArgumentOutOfRangeException("n > Nrank");
			if (ml > this.mrank) throw new ArgumentOutOfRangeException("|m| > Mrank");
			if (ml > n) throw new ArgumentOutOfRangeException("|m| > n");
			
			// applying
			int res = 0;			
			if (m == 0)
				res = n - 1;
			else 
			{
				Func<int, int> count = delegate(int _m) {
					return this.nrank * _m - _m*(_m-1) / 2;
				};
				
				if  (m > 0)
					res = this.nrank + count(m-1) + n - m;
				else
					res = this.nrank + count(this.mrank) + count(-m-1) + n + m;
			}
			switch (l)
			{
			case Index.Type.M: break;
			case Index.Type.N: res += this.nmax; break;
			}
			
			return res;
		}

		/**
		 * Return integer uniquelly determing the (block, offset)
		 *
		 * @param int block
		 * @param int offset
		 * @return int
		 */
		protected int hashOffset(int block, int offset)
		{
			// check input
			if (block < 0) throw new ArgumentOutOfRangeException("Wrong: block < 0");
			if (block >= this._blocks.Length) throw new ArgumentOutOfRangeException("Wrong: block > max_block");
			if (offset < 0) throw new ArgumentOutOfRangeException("Wrong: offset < 0");
			
			Block b = this._blocks[block];
			if (offset >= b.length) throw new ArgumentOutOfRangeException("Wrong: offset > max_offset_in_block 0");
			
			// return
			return b.offset + offset;
		}
		
		/**
		 * Convert (n,m,l) => (block,offset)
		 * 
		 * @param int n
		 * @param int m
		 * @param Index.Type l
		 * @return Offset
		 */
		public Offset offset(int n, int m, Index.Type l)
		{
			int i = this.hashIndex(n, m, l);
			int position = this.map[i].position;
			
			return this.offset(position);
		}
		
		/**
		 * Get offset (block, offset) by its position
		 * 
		 * @param int position
		 * @return Offset
		 */
		public Offset offset(int position)
		{
			// check input
			if (position < 0) throw new ArgumentOutOfRangeException("Wrong: position < 0");
			if (position >= 2*this.nmax) throw new ArgumentOutOfRangeException("Wrong: position > max_position");
			
			// return
			for (int i = 0; i < this._blocks.Length; i++)
			{
				Block block = this._blocks[i];
				if (block.offset <= position && position < block.offset + block.length)
					return new Offset(position, i, position - block.offset);
			}
			throw new ArgumentOutOfRangeException("No suitable block is found [internal]");
		}
		
		/**
		 * Convert (block,offset) => (n,m,l)
		 * 
		 * @param int block
		 * @param int offset
		 * @return Index
		 */
		public Index index(int block, int offset)
		{
			int position = this.hashOffset(block, offset);
			return this.index(position);
		}

		/**
		 * Get index (n,m,l) by its position
		 * 
		 * @param int position
		 * @return Index
		 */
		public Index index(int position)
		{
			// check input
			if (position < 0) throw new ArgumentOutOfRangeException("Wrong: position < 0");
			if (position >= 2*this.nmax) throw new ArgumentOutOfRangeException("Wrong: position > max_position");
			
			// return
			int i = this.map_inv[position];
			return this.map[i];
		}
		
		/**
		 */
		protected void bind(int block_id, int n, int m, Index.Type l)
		{
			int position = this._blocks[block_id].offset + this._blocks[block_id].length++;
			int key = this.hashIndex(n, m, l);
			this.map_inv[position] = key;
			this.map[key] = new Index(position, n, m, l);
		}
	}
	
	
	/**
	 * Factory class for Mapper class
	 * 
	 * @date 02 Oct 2012
	 */
	public interface MapperFactory {
		/**
		 * Create Mapper instance
		 */
		Mapper createMapper(Symmetry symmetry, int nrank, int mrank);
		
		/**
		 * Check if the mapper class suites to the given symmetry properties
		 * The mapper factory with highest value is used to create mapper instace
		 * 
		 * @param Symmetry symmetry
		 * @return int 
		 */
		int weighSymmetry(Symmetry symmetry);
	}
	
	/**
	 * Collection of Mapper Factory instances
	 * 
	 * @date 02 Oct 2012
	 */
	public class MapperAggregator
	{
		/**
		 * Collection of registered MapperFactory instances
		 */
		protected List<MapperFactory> factories;
		
		/**
		 * Collection of created Mapper instances
		 */
		protected List<Mapper> mappers;
		
		/**
		 * Singleton pattern
		 */
		protected static MapperAggregator instance;
		protected MapperAggregator()
		{
			this.mappers   = new List<Mapper>();
			this.factories = new List<MapperFactory>();
		}
		public static MapperAggregator getInstance()
		{
			if (MapperAggregator.instance == null)
				MapperAggregator.instance = new MapperAggregator();
			return MapperAggregator.instance;
		}
		
		/**
		 * Register Mapper Factory#
		 * 
		 * @param MapperFactory factory
		 *
		 */
		public void registerFactory(MapperFactory factory)
		{
			int index = this.factories.FindIndex(delegate (MapperFactory f) {
				return f.Equals(factory);
			});
			
			if (index < 0)
				this.factories.Add(factory);
		}
		
		/**
		 * Get (or create if needed) Mapper instance for given symmetry
		 */
		public Mapper getMapper(Symmetry symmetry, int nrank, int mrank)
		{
			// search in the collection of already created mapper's
			int i = this.mappers.FindIndex(delegate (Mapper mapper) {
				return (mapper.symmetry == symmetry && mapper.nrank == nrank && mapper.mrank == mrank);
			});
			if (i >= 0) return this.mappers[i];
			
			// search for suitable MapperFactory
			int weight_suit = 0;
			MapperFactory factory_suit = null;
			foreach (MapperFactory factory in this.factories)
			{
				int weight = factory.weighSymmetry(symmetry);
				if (weight > weight_suit)
				{
					weight_suit  = weight;
					factory_suit = factory;
				}
			}
			if (factory_suit == null)
				throw new Exception("No suitable mapper factory could be found for given symmetry parameters [internal]");

			// create instance
			Mapper map = factory_suit.createMapper(symmetry, nrank, mrank);
			this.mappers.Add(map);
			return map;
		}
	}
}

