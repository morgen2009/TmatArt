using System;
using TmatArt.Geometry.Region;
using TmatArt.Geometry;
using TmatArt.Numeric.Mathematics;

namespace TmatArt.Scattering.Field
{
	/// <summary>
	/// Compute the electromagnetic field inside the given region (near field).
	/// The field outside this regions is assumed to be zero
	/// </summary>
	public class RegionField : Field
	{
		public readonly Field field;
		public readonly IRegion region;

		/// <summary>
		/// Initializes a new instance of the <see cref="TmatArt.Scattering.Field.RegionField"/> class.
		/// </summary>
		/// <param name="field">Electromagnetic field.</param>
		/// <param name="region">Region where the field is defined.</param>
		public RegionField (Field field, IRegion region)
		{
			this.field = field;
			this.region = region;
		}

		/// <see cref="TmatArt.Scattering.Field.NearE"/>
		public override Vector3c NearE (Vector3d r)
		{
			if (region.inside(r)) {
				return field.NearE(r);
			} else {
				return new Vector3c();
			}
		}

		/// <see cref="TmatArt.Scattering.Field.FarE"/>
		public override Vector3c FarE (Euler e)
		{
			return new Vector3c();
		}

		/// <see cref="TmatArt.Scattering.Operation<T>"/>
		public override T Resolve<T> ()
		{
			return field.Resolve<T>();
		}
	}
}

