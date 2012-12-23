using System;
using System.Collections.Generic;

namespace TmatArt.Numeric.Mathematics
{
	/// <summary>
	/// Interface for objects representing algebra
	/// </summary>
	public interface IAlgebraOperations<T, TScalar> : IRingIdentityOperations<T>, IVectorSpace<T, TScalar>
	{
	}
}

