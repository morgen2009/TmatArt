using System;
using TmatArt.Geometry.Region;
using TmatArt.Numeric.Mathematics;
using TmatArt.Scattering.Basis;

namespace TmatArt.Scattering.Field
{
	public abstract class Expansion : Field
	{
		public double nrank, mrank, nmax;
		public IExpansionCoefficients coef;
		public IBasisFunctions basis;
	}
}

