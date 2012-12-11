using System;
using TmatArt.Geometry;

namespace TmatArt.Scattering.Field
{
	/// <summary>
	/// Interface for the field object
	/// </summary>
	public interface IField
	{
		/// <summary>
		/// Compute the E component of the electromagnetic field in the near region (excepting factor e^(-j &omega; &phi;))
		/// </summary>
		/// <param name="r">Location of the point.</param>
		Vector3c NearE(Vector3d r);

		/// <summary>
		/// Compute the E component of the electromagnetic field in the far region (excepting factor e^(-j &omega; &phi;))
		/// </summary>
		/// <param name="e">Euler angle specifying the scattering direction.</param>
		Vector3c FarE(Euler e);

		/// <summary>
		/// Get expansion coefficients of the field (regular at the origin)
		/// </summary>
		IExpansionCoefficients Expansion(Coordinate coordinate);

		/// <summary>
		/// Get factory object of the object
		/// </summary>
		IFieldFactory factory();
		
	}
}

