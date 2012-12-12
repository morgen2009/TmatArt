using System;
using TmatArt.Geometry;
using TmatArt.Numeric.Mathematics;

namespace TmatArt.Scattering.Field
{
	public abstract class Superposition : Field
	{
		public Field   [] fields;
		public Complex [] weights;
	}
}

