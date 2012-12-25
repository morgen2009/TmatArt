using System;
using NUnit.Framework;
using TmatArt.Scattering;

namespace TmatArt.Scattering
{
	[TestFixture()]
	public class WaveLengthTest
	{
		[Test()]
		public void ScaleNM ()
		{
			WaveLength wave;

			wave = new WaveLength(532);
			Assert.AreEqual(wave.length/1000, wave.In(WaveLength.Unit.MICRON).length, "NM->MICRON");

			wave = new WaveLength(532, WaveLength.Unit.MICRON);
			Assert.AreEqual(wave.length*1000, (double)wave.In(WaveLength.Unit.NM), "MICRON->NM");
		}

		[Test()]
		public void ScaleEV ()
		{
			WaveLength wave;

			wave = WaveLength.Value(532);
			Assert.AreEqual(2.33, wave.In(WaveLength.Unit.EV), 1E-3, "NM->EV");
			
			wave = WaveLength.Value(1, WaveLength.Unit.EV);
			Assert.AreEqual(1240, wave.In(WaveLength.Unit.NM), 1, "EV->NM");
		}
	}
}

