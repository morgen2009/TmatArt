using System;
using TmatArt.Geometry;

namespace TmatArt.Scattering.Indexing
{
	/**
	 * Index of the item in the sequence
	 * 
	 * @date 02 Oct 2012
	 */
	public struct Index
	{
		/* position of item in the sequence */
		public readonly int   position;
		
		/* index m of item */
		public readonly short n;
		
		/* index n of item */
		public readonly short m;
		
		/* index l of item */
		public enum Type {
			M, // item delates to M_{mn}^{k}(r)
			N  // item delates to N_{mn}^{k}(r)
		};
		public readonly Type l;
		
		/**
		 * Constructor
		 */
		internal Index(int position, int n, int m, Type l)
		{
			if (Math.Abs(m) > n)
				throw new ArgumentOutOfRangeException(String.Format("Index n must be not less than |m| (n={0}, m={1})", n, m));
			if (n < 0)
				throw new ArgumentOutOfRangeException(String.Format("Index n must be positive (n={0})", n));
			
			this.position = position;
			this.n = (short)n;
			this.m = (short)m;
			this.l = l;
		}
	}
}
