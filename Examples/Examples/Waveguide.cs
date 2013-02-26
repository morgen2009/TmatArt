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
	[ExampleGroup("Waveguide field")]
	public class WaveguideExample
	{
		double deg = System.Math.PI / 180;

		public WaveguideExample ()
		{
		}

		[Example()]
		public void monoMode()
		{
			Isotrop mediumu = new Isotrop(1.33);
			Isotrop mediumc = new Isotrop(1.55);
			Isotrop mediumb = new Isotrop(1.5);
			WaveLength wave = new WaveLength(628.3);
			double h = 1000;
			double theta = 81.4030808806419;
//			double theta = 81.6768636703491;

			PlaneWave in1 = new PlaneWave(theta*deg, 0*deg, PlaneWave.Polarization.VERTICAL);
			in1.medium = mediumc;
			in1.wave   = wave;

			PlaneWave in1u = in1.Resolve<IReflectOperation>().Transmit(new Halfspace(h), mediumu) as PlaneWave;
			in1u.beta = in1u.beta.Conjugate();
			Console.WriteLine("Beta={0}, {1}", in1u.beta.re, in1u.beta.im);
			Console.WriteLine("N={0}", in1u.norm);
			Field in2  = in1.Resolve<IReflectOperation>().Reflect(new Halfspace(h), mediumu);
			Field in2b = in2.Resolve<IReflectOperation>().Transmit(new Halfspace(0), mediumb);

			Field total = new Superposition(new Field[] {
				new RegionField(in1, new Layer(0, h)),
				new RegionField(in1u, new Halfspace(h)),
				new RegionField(in2b, Expression.NOT(new Halfspace(0))),
				new RegionField(in2, new Layer(0, h))
			});

			// plot field
			StreamWriter file = new System.IO.StreamWriter("data_waveguide.dat");
			
			GnuplotFieldSaver saver = new GnuplotFieldSaver(total);
			saver.setMessage("x z ex ez");
			saver.setPoints(this.getRectangleMeshInXZPlane(1000, 5, 1200, 5));
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

