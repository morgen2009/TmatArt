using System;
using TmatArt.Geometry.Region;
using TmatArt.Numeric.Mathematics;

namespace TmatArt.Scattering.Field
{
	public class PlaneWaveFactory: IFieldFactory
	{
		public IField Reflect(Halfspace region)
		{
			return new PlaneWave();
		}

		public IField Transmit(Halfspace region)
		{
			return new PlaneWave();
		}
	}
}

