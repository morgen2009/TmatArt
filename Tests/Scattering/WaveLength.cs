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
			Assert.AreEqual(wave.Length()/1000, wave.Length(WaveLength.Unit.MICRON), "NM->MICRON");

			wave = new WaveLength(532, WaveLength.Unit.MICRON);
			Assert.AreEqual(wave.Length()*1000, wave.Length(WaveLength.Unit.NM), "MICRON->NM");
		}

		[Test()]
		public void ScaleEV ()
		{
			WaveLength wave;

			wave = new WaveLength(532);
			Assert.AreEqual(2.33, wave.Length(WaveLength.Unit.EV), 1E-3, "NM->EV");
			
			wave = new WaveLength(1, WaveLength.Unit.EV);
			Assert.AreEqual(1240, wave.Length(WaveLength.Unit.NM), 1, "EV->NM");
		}
	}
}

