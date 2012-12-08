using System;
using TmatArt.Scattering.Index;
using TmatArt.Geometry;
using System.Collections.Generic;

namespace TmatArt.Scattering.Basis
{
	public abstract class SVWF
	{
		public abstract VectorC f (int n, int m, Type l, Vector r);
		public abstract VectorC f (Index.Index index, Vector r);
		public abstract IEnumerable<KeyValuePair<Index.Index, VectorC>> f (System.Collections.Generic.IEnumerable<Index.Index> index, Vector r);
	}
}

