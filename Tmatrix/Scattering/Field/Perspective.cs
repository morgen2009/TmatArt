using System;
using TmatArt.Geometry;
using TmatArt.Numeric.Mathematics;

namespace TmatArt.Scattering.Field
{
	public abstract class Perspective: Field
	{
		public Coordinate perspective;
		public Field      field;
	}
}

