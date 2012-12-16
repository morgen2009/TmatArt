using System;
using TmatArt.Numeric.Mathematics;

namespace TmatArt.Geometry
{
	public interface IVector3x<T, Tscalar> : IHilbertSpace<T, Tscalar>, IRingOperations<T>
	{
		T RotateX (double angle);
		T RotateY (double angle);
		T RotateZ (double angle);
	}
}