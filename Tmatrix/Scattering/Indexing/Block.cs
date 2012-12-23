using System;
using TmatArt.Geometry;

namespace TmatArt.Scattering.Indexing
{
	/**
	 * Class describing the block in the sequence of indexes
	 * 
	 * @date 02 Oct 2012
	 */
	public class Block
	{
		/**
		 * Reference to the parent block
		 * @note For some symmetries of scattering objects all elements of
		 * one block through the generall symmetry relation
		 * can be projected onto elements of other block (on elements of itself).
		 * Thus, there is an equivalence relation between blocks. In each equivalence
		 * group (one or two blocks), the first one is called master, the second one slave.
		 */
		private Block _parent;
		public Block parent {
			get { return _parent; }
			set { _parent = value != null ? (value.map.Equals(this.map) ? value : null) : null; }
		}
		
		/* offset for the first item in the block */
		public readonly int offset;
		
		/* number of elements within the block */
		private int _length;
		public int length {
			get { return _length; }
			set { _length = value > 0 ? value : 0; }
		}
		
		/* mapping object that includes the block */
		private Mapper map;
		
		/**
		 * Consruct
		 * 
		 * @param int id
		 * @param int parent
		 * @param int offset
		 * @param int length
		 * @param Mapping map
		 */
		public Block(Mapper map, int offset)
		{
			this.parent = null;
			this.offset = offset;
			this.length = 0;
			this.map    = map;
		}
		
		/**
		 * Iterator over block's indexes
		 */
		public System.Collections.Generic.IEnumerable<Index> items()
		{
			for (int i = this.offset; i<this.offset+this.length; i++)
				yield return this.map.index(i);
		}
		
		/**
		 * Is current block master or slave?
		 * 
		 * @return bool
		 */
		public bool isMaster()
		{
			return parent == null || parent == this;
		}
	}

}

