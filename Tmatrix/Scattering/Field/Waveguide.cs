using System;
using System.Collections.Generic;
using System.Linq;
using TmatArt.Geometry;
using TmatArt.Numeric.Mathematics;
using TmatArt.Geometry.Region;
using TmatArt.Scattering.Medium;
using TmatArt.Scattering.Field.Operation;
using TmatArt.Scattering.Field.Impl;

namespace TmatArt.Scattering.Field
{
	public class Waveguide : Field
	{
		private List<Halfspace> regions;
		private List<RegionField> fields;
		private PlaneWave excitation;
		private bool computed;

		public Waveguide(PlaneWave excitation)
		{
			this.regions = new List<Halfspace>();
			this.fields = new List<RegionField>();
			this.excitation = excitation;
			this.computed = true;
		}

		public void addRegion(Halfspace region) {
			this.regions.Add(region);
			this.computed = false;
		}
		
		public void computeFields() {
			// sort regions
			this.regions.Sort(delegate (Halfspace x, Halfspace y) {
				return (x.z > y.z) ? 1 : -1;
			});

			// create list of regional fields
			this.fields.Clear();
			this.fields.Add(new RegionField(null, Expression.NOT(this.regions.First())));
			this.regions.ForEach(delegate (Halfspace region) {
				this.fields.Add(new RegionField(null, region));
			});

			// compute fresnel coefficients
		}
		
		public override Vector3c NearE (Vector3d r)
		{
			if (!this.computed) {

			}
			throw new System.NotImplementedException ();
		}

		public override Vector3c FarE (Euler e)
		{
			throw new System.NotImplementedException ();
		}                             

		public override T Resolve<T>()
		{
			throw new System.NotImplementedException ();
		}
	}
}

