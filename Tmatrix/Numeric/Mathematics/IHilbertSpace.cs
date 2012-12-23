using System;

namespace TmatArt.Numeric.Mathematics
{
	/// <summary>
	/// Interface for objects representing vector space
	/// </summary>
	/// <description>
	/// According to mathematical definition, Hilbert space is the vector space
	/// with defined scalar product
	/// </description>		
	public interface IHilbertSpace<T, Tscalar> : IVectorSpace<T, Tscalar>
	{
		Tscalar Scalar(T a);
		Tscalar Length();
	}
}

