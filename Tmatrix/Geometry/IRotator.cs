using System;
using TmatArt.Numeric.Mathematics;

namespace TmatArt.Geometry
{
	/// <summary>
	/// Interface for rotating an other object 
	/// </summary>
	public interface IRotator<TAxisName, Tangle>
	{
		T Rotate<T>(T obj) where T: IRotatableAxis<T, Axis3Name, double>;
	}

	/// <summary>
	/// Interface for rotatable object about axes
	/// </summary>
	public interface IRotatableAxis<T, TAxisName, Tangle>
	{
		T Rotate(TAxisName axis, Tangle angle);
	}

	/// <summary>
	/// Name of three axis in 3D space
	/// </summary>
	public enum Axis3Name {X, Y, Z};
}