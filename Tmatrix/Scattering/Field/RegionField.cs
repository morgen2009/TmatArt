using System;
using TmatArt.Geometry.Region;
using TmatArt.Numeric.Mathematics;

namespace TmatArt.Scattering.Field
{
	public abstract class RegionField : Field
	{
		public Field field;
		public IRegion region;
	}
}

