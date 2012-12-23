using System;

namespace TmatArt.Geometry.Region
{
	/// <summary>
	/// Interface for the region object in the 3-dimentional space
	/// </summary>
	public interface IRegion
	{
		/// <summary>
		/// Check if the specified point is inside the region.
		/// </summary>
		/// <param name="point">Point.</param>
		bool inside(Vector3d point);
	}
}

