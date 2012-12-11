using System;
using TmatArt.Geometry.Region;
using TmatArt.Scattering.Medium;

namespace TmatArt.Scattering.Field
{
	/// <summary>
	/// Interface for the field factory object
	/// </summary>
	public interface IFieldFactory
	{
		/// <summary>
		/// Reflection of the field on the specified boundary of the region
		/// </summary>
		/// <param name="region">Region.</param>
		IField Reflect(Halfspace region, IMedium mediumExt);

		/// <summary>
		/// Transmission of the field on the specified boundary of the region
		/// </summary>
		/// <param name="region">Region.</param>
		IField Transmit(Halfspace region, IMedium mediumExt);
	}
}

