using System;
using TmatArt.Common.Attribute;

namespace TmatArt.Example
{
	[ExampleSet("Planewave field", path = "PlaneWave/")]
	public class PlaneWave
	{
		public PlaneWave ()
		{
		}

		[Example("Free space", description = "beta=45 deg, TM/TE")]
		void InFreeSpace()
		{
		}

		[Example("Reflection on the surface", description = "beta=45 deg, m_r = 1.5, TM/TE)")]
		void Reflection()
		{
		}
	}
}

