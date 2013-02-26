using System;
using NUnit.Framework;
using TmatArt.Scattering.Field.Operation;
using TmatArt.Scattering.Medium;
using TmatArt.Numeric.Mathematics;
using TmatArt.Geometry.Region;
using TmatArt.Geometry;

namespace TmatArt.Scattering.Field
{
	[TestFixture()]
	public class WaveguideTest
	{
		double deg = System.Math.PI / 180;

		private double getRoot(Func<double, double> func, double a, double b)
		{
			const double epsRoot = 1E-10;
			double x=0, x1, fx;
			double fa = func(a), fb = func(b);
			
			do
			{
				x1 = x;
				//x  = a - (b-a) / (fb-fa) * fa;
				x  = (a + b) / 2;
				Console.WriteLine("Loop, x={0}", x/deg);
				fx = func(x);
				Console.WriteLine("Phase f={0}", fx/deg);
				Console.WriteLine("{0}, {1} => {2}, {3}, {4}", a/deg, b/deg, fa/deg, fb/deg, fx/deg);
				if (System.Math.Sign(fx)*System.Math.Sign(fb) < 0) { a = x; fa = fx; }
				else
				if (System.Math.Sign(fx)*System.Math.Sign(fa) < 0) { b = x; fb = fx; }
				else x1 = x;
			} while (System.Math.Abs(x-x1) >= epsRoot);
			
			return x;
		}

		[Test]
		public void correntModes ()
		{
			Isotrop mediumu = new Isotrop(1.33);
			Isotrop mediumc = new Isotrop(1.55);
			Isotrop mediumb = new Isotrop(1.5);
			WaveLength wave = new WaveLength(0.6283);
			double h = 1.0;
			//double thetaMinU = Complex.Math.Asin(mediumu.index / mediumc.index).re;
			//double thetaMinB = Complex.Math.Asin(mediumb.index / mediumc.index).re;

			double thetaIn = this.getRoot(delegate(double angle) {

				Fresnel.Coefficients cu = Fresnel.Compute(angle, mediumc.index, mediumu.index);
				Fresnel.Coefficients cb = Fresnel.Compute(angle, mediumc.index, mediumb.index);
				
				Complex te = Complex.Euler(1, 0);
				
				Complex wavenumber = (2*System.Math.PI / wave.length) * mediumc.index;
				Complex phaseTrav = wavenumber * h * Math.Cos(angle);
				
				Complex to = te * cu.rp * cb.rp * Complex.Math.Exp(-Complex.AIM * 2 * phaseTrav);

				double phase = Complex.Math.Argument(to);
				if (phase > System.Math.PI) {
					phase = phase - 2*System.Math.PI;
				}

				return phase;
			}, 81 * deg, 82 * deg);


			Console.WriteLine("{0}", thetaIn / deg);

			Assert.Fail();
		}
	}
}

