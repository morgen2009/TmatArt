using System;
using TmatArt.Scattering.Medium;
using TmatArt.Geometry.Region;

namespace TmatArt.Scattering.Field
{
	/// <summary>
	/// The factory for the field object
	/// </summary>
	public abstract class FieldFactory
	{
		/// <summary>
		/// Reflection of the field on the specified boundary of the region
		/// </summary>
		/// <param name="region">Region.</param>
		public abstract Field Reflect(Halfspace region, Medium.Isotrop mediumExt);
		
		/// <summary>
		/// Transmission of the field on the specified boundary of the region
		/// </summary>
		/// <param name="region">Region.</param>
		public abstract Field Transmit(Halfspace region, Medium.Isotrop mediumExt);
	}
}

