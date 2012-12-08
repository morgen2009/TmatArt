using System;
using TmatArt.Scattering.Indexing;
using TmatArt.Geometry;
using System.Collections.Generic;
using TmatArt.Numeric;
using TmatArt.Numeric.Function;
using System.Linq;

namespace TmatArt.Scattering.Basis
{
	/**
	 * Spherical Vector Wave Functions
	 */
	public class SVWF
	{
		public IEnumerable<KeyValuePair<Index, VectorC>> f (int nrank, int mrank, double r, double theta, double phi, Medium.Isotrop medium)
		{
			// compute bessel functions
			var re =
				from bes in SphBessel.j(r)
				where bes.n <= nrank
				select bes;
			
			for (int ma = 0; ma < mrank; ma++)
			{
				Complex fact = Complex.n(1, ma * phi);
				for (int m = -ma; m <= ma; m+=Math.Max(2*ma,1))
				{
					Angular.compute(theta, nrank, m);
						// compute m_{mn}(theta, phi)
					for (int n = 0; n < nrank; n++)
					{
						
					}
					fact = fact.conjg();
				}
			}
			yield break;
		}
		
		
		public VectorC m(int m, int n, double theta, Angular.Value<double> val)
		{
/*			VectorC res = new VectorC();
			res.x = m * Complex.aim() * val.pi;
			res.y = - val.tau;
			res.z = 0;
			return res;*/
			return null;
		}
		
		public VectorC n(int m, int n, double theta, double p, double dp)
		{
			return new VectorC(p, m * Complex.aim() * dp, 0);
		}
	}
/*	public abstract class SVWF
	{
		public abstract VectorC f (int n, int m, Index.Type l, Vector r, Medium.Base medium);
		public abstract VectorC f (Index index, Vector r, Medium.Base medium);
	}
	
	public class IncommingSVWF : SVWF
	{
		public override VectorC f (int n, int m, Index.Type l, Vector r, Medium.Base medium)
		{
			VectorR rad = (VectorR)r;
			Medium.Isotrop med = (Medium.Isotrop)medium;
				
			Complex bes = Numeric.Function.SphBessel.j(rad.r*med.mr, n).Where(v => v.n == n).First().p;
				
			if (l == Index.Type.M)
			{
			}
			else
			{
			}
		}
	}
	
	public class OutcommingSVWF : SVWF
	{
	}*/
}

