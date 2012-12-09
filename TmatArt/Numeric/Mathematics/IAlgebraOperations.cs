using System;
using System.Collections.Generic;

namespace TmatArt.Numeric.Mathematics
{
	/// <summary>
	/// Interface for objects representing algebra
	/// </summary>
	public interface IAlgebraOperations<T, TScalar> : IRingOperations<T>, IVectorOperations<T, TScalar>
	{
	}
}

