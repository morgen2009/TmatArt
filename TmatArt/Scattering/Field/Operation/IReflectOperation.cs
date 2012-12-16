using System;
using TmatArt.Geometry.Region;

namespace TmatArt.Scattering.Field.Operation
{
	public interface IReflectOperation
	{
		/// <summary>
		/// Reflection of the field on the specified boundary of the region
		/// </summary>
		/// <param name="region">Region.</param>
		Field Reflect(Halfspace region, Medium.Isotrop mediumExt);
		
		/// <summary>
		/// Transmission of the field on the specified boundary of the region
		/// </summary>
		/// <param name="region">Region.</param>
		Field Transmit(Halfspace region, Medium.Isotrop mediumExt);
	}
}

