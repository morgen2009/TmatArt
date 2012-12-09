using System;
using System.Collections.Generic;

namespace TmatArt.Numeric.Mathematics
{
	/// <summary>
	/// Interface for objects representing ring with multiplicative identity
	/// </summary>
	/// <description>
	/// Mathematical definition of the ring R
	/// 
	/// * Operations
	/// 	= addition       { +: R x R => R }
	/// 	= multiplication { *: R x R => R }
	/// 
	/// * Axioms
	/// 	= (R, +) is abelian (commutative) group
	/// 	= (R\{o}, *) is group
	/// 	= disctributivity { for all a, b, c in R => a*(b+c) = (a*b)+(a*c) and (a+b)*c = (a*c)+(b*c) }
	/// </description>		
	public interface IRingIdentityOperations<T> : IRingOperations<T>
	{
		T Divide(T a);
		T Inverse();
		T One();
	}
}

