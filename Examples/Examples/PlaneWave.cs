using System;
using System.Linq;
using System.IO;
using System.Collections.Generic;
using TmatArt.Common.Attribute;
using TmatArt.Scattering.Field;
using TmatArt.Scattering.Medium;
using TmatArt.Common;
using TmatArt.Geometry;
using TmatArt.Geometry.Region;
using TmatArt.Scattering;
using TmatArt.Scattering.Field.Operation;
using TmatArt.Examples.Plot;
using TmatArt.Numeric.Mathematics;

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
			// arrage field
			Field field = this.field;
			Isotrop mediumExt = new Isotrop(1.9);
			Halfspace surface = new Halfspace(0);
			Field reflect = field.Resolve<IReflectOperation>().Reflect(surface, mediumExt);
			Field trans   = field.Resolve<IReflectOperation>().Transmit(surface, mediumExt);
			
			Field totalUppon = new RegionField( new Superposition(new Field[] { field, reflect }), surface);
			Field totalBelow = new RegionField( trans, Expression.NOT(surface));
			Field total = new Superposition(new Field[] { totalBelow, totalUppon });
			
			// plot field
			StreamWriter file = new System.IO.StreamWriter("data_reflect.dat");
			
			GnuplotFieldSaver saver = new GnuplotFieldSaver(total);
			saver.setMessage("x z ex ez");
			saver.setPoints(this.getRectangleMeshInXZPlane(1000, 10, 1000, 10));
			saver.saveField(file, delegate(Vector3d p, Vector3c e) {
				return String.Format("{0} {1} {2} {3}", p.x, p.z, e.x.re, e.z.re).Replace(",", ".");
			});
			file.Close();
		}
		
		[Example("Reflection on the surface", text = "beta=45 deg, m_r = 1.5, TM/TE)")]
		public void ReflectionEvanescent()
		{
			// arrage field
			Field field = this.field;
			Isotrop mediumExt = new Isotrop(0.666);
			Halfspace surface = new Halfspace(200);
			Field reflect = field.Resolve<IReflectOperation>().Reflect(surface, mediumExt);
			Field trans   = field.Resolve<IReflectOperation>().Transmit(surface, mediumExt);
			//(trans as PlaneWave).beta = (trans as PlaneWave).beta.Conjugate();

			Field totalUppon = new RegionField( new Superposition(new Field[] { field, reflect }), surface);
			Field totalBelow = new RegionField( trans, Expression.NOT(surface));
			Field total = new Superposition(new Field[] { totalBelow, totalUppon });
			
			// plot field
			StreamWriter file = new System.IO.StreamWriter("data_evan.dat");
			
			GnuplotFieldSaver saver = new GnuplotFieldSaver(total);
			saver.setMessage("x z ex ez");
			saver.setPoints(this.getRectangleMeshInXZPlane(1000, 10, 1000, 10));
			saver.saveField(file, delegate(Vector3d p, Vector3c e) {
				return String.Format("{0} {1} {2} {3}", p.x, p.z, e.x.re, e.z.re).Replace(",", ".");
			});
			file.Close();
		}
		
		private IEnumerable<Vector3d> getRectangleMeshInXZPlane(double xmax, int xstep, double zmax, int zstep)
		{
			return
				from x in Limit.Range(-xmax, xmax, xstep)
				from z in Limit.Range(-zmax, zmax, zstep)
				select new Vector3d(x, 0, z);
		}
	}
}

