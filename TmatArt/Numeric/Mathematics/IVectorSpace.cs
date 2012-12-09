using System;
using System.Collections.Generic;

namespace TmatArt.Numeric.Mathematics
{
	/// <summary>
	/// Interface for objects representing vector space
	/// </summary>
	/// <description>
	/// Mathematical definition of the vector space L for P
	/// 
	/// * Operations
	/// 	= addition       { +: L x L => L }
	/// 	= multiplication { *: P x L => L }
	/// 
	/// * Axioms
	/// 	= (L, +) is abelian (commutative) group
	/// 	= (L, P, *) holds
	/// 		* associativity    { for all a, b in P and c in L => (a*b)*c = a*(b*c) }
	/// 		* identity element { exists only one e in P, such that for all a in L => e*a = a }
	/// 		* inverse element  { for all a in G => exists b in G, such that a+b = b+a = o }
	/// 	= disctributivity
	/// 		* scalar { for all a, b in P and c in L => (a+b)*c = (a*c)+(b*c) }
	/// 		* vector { for all a in L and b, c in P => a*(b+c) = (a*b)+(a*c) }
	/// </description>		
	public interface IVectorSpace<T, TScalar> : IGroupOperations<T>
	{
		T Multiply(TScalar a);
		T Divide(TScalar a);
	}
}

