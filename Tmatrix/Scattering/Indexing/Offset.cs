using System;
using TmatArt.Geometry;

namespace TmatArt.Scattering.Indexing
{
	/**
	 * Offset of the item in the sequence
	 * 
	 * @date 02 Oct 2012
	 */
	public struct Offset
	{
		/* position of item in the sequence */
		public readonly int position;
		
		/* number of block with item */
		public readonly int block;
		
		/* position of item in the block */
		public readonly int offset;
		
		/**
		 * Constructor
		 * 
		 * @param int position
		 * @param int block
		 * @param int offset
		 */
		internal Offset(int position, int block, int offset)
		{
			this.position = position;
			this.block    = block;
			this.offset   = offset;
		}
	}
}