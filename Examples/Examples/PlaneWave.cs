using System;
using System.IO;
using TmatArt.Common.Attribute;
using TmatArt.Scattering.Field;
using TmatArt.Scattering.Medium;
using TmatArt.Common;
using TmatArt.Geometry;
using TmatArt.Scattering;

namespace TmatArt.Examples
{
	[ExampleGroup("Planewave field")]
	public class PlaneWaveExample
	{
		PlaneWave field;

		public PlaneWaveExample ()
		{
			double deg = System.Math.PI / 180;
			field = new PlaneWave(45 * deg, 0 * deg, PlaneWave.Polarization.VERTICAL);
			field.wave = new WaveLength(628.3);
			field.medium = new Isotrop(1.0);
		}

		[Example()]
		public void InFreeSpace()
		{
			string res = "";
			double zmax = 1000;
			double xmax = 1000;

			StreamWriter file = new System.IO.StreamWriter("data.dat");
			foreach (double x in Limit.Range(-xmax, xmax, 10)) {
				foreach (double z in Limit.Range(-zmax, zmax, 10)) {
					Vector3d p = new Vector3d(x, 0, z);
					Vector3d e = field.NearE(p).re;
					res = String.Format("{0} {1} {2} {3}", x, z, e.x, e.z).Replace(",", ".");
					file.WriteLine(res);
				}
			}
			file.Close();
		}

		[Example("Reflection on the surface", text = "beta=45 deg, m_r = 1.5, TM/TE)")]
		public void Reflection()
		{
		}
	}
}

