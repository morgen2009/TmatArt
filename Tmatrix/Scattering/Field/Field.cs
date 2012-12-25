using System;
using TmatArt.Scattering.Medium;
using TmatArt.Numeric.Mathematics;
using TmatArt.Geometry;
using TmatArt.Scattering.Field.Operation;

namespace TmatArt.Scattering.Field
{
	/// <summary>
	/// Electromagnetical field
	/// </summary>
	public abstract class Field
	{
		/// <summary>
		/// The wavelength of the electromagnetical oscillation
		/// </summary>
		public WaveLength wave;

		/// <summary>
		/// The ambient medium
		/// </summary>
		public Isotrop medium;

		/// <summary>
		/// Compute the E component of the electromagnetic field in the near region (excepting factor e^(-j &omega; &phi;))
		/// </summary>
		/// <param name="r">Location of the point.</param>
		public abstract Vector3c NearE(Vector3d r);
		
		/// <summary>
		/// Compute the E component of the electromagnetic field in the far region (excepting factor e^(-j &omega; &phi;))
		/// </summary>
		/// <param name="e">Euler angle specifying the scattering direction.</param>
		public abstract Vector3c FarE(Euler e);

		/// <summary>
		/// Get instance implementing the given operation for the field
		/// </summary>
		/// <param name="T">IFieldOperation interface.</param>
		public abstract T Resolve<T>();
	}
}

