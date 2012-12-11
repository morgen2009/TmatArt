using System;
using TmatArt.Geometry;
using TmatArt.Numeric.Mathematics;

namespace TmatArt.Scattering.Field
{
	public class PlaneWave: IField
	{
		public Complex Ex, Ey;

		public Vector3c Near(Vector3d r)
		{
			return new Vector3c(0, 0, 0);
		}
		
		public Vector3c Far(Euler e)
		{
			return new Vector3c(0, 0, 0);
		}

		public IFieldFactory factory()
		{
			return new PlaneWaveFactory();
		}

		public IExpansionCoefficients Expansion(Coordinate coordinate)
		{
			return null;
		}
	}
}

