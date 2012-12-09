using System;
using System.Collections.Generic;

namespace TmatArt.Numeric.Mathematics
{
	/// <summary>
	/// Interface for objects representing group
	/// </summary>
	/// <description>
	/// Mathematical definition of the group (G, +)
	/// * closure          { for all a, b in G => a+b in G }
	/// * associativity    { for all a, b, c in G => (a+b)+c = a+(b+c) }
	/// * identity element { exists only one o in G, such that for all a in G => o+a = a+o = a }
	/// * inverse element  { for all a in G => exists b in G, such that a+b = b+a = o }
	/// </description>		
	public interface IGroupOperations<T> : IEqualityComparer<T>
	{
		T Add(T a);
		T Subtract(T a);
		T Negate();
		T Zero();
	}
}

