using System;

namespace TmatArt.Geometry.Region
{
	/// <summary>
	/// Making computation (intersection, union, negation) with regions
	/// </summary>
	public struct Expression: IRegion
	{
		public delegate bool Closure (Vector3d point);

		///<summary>
		/// Closure computing if point is inside the region
		/// </summary>
		private Closure exp;

		/// <summary>
		/// Initializes a new instance of the <see cref="TmatArt.Geometry.Region.Expression"/> struct.
		/// </summary>
		/// <param name="exp">Closure</param>
		public Expression (Closure exp)
		{
			this.exp = exp;
		}

		/// <see cref="TmatArt.Geometry.Region.IRegion.inside"/>
		public bool inside(Vector3d point)
		{
			return this.exp(point);
		}

		/// <summary>
		/// Intersection of two regions
		/// </summary>
		public static Expression AND(IRegion a, IRegion b)
		{
			return new Expression(delegate (Vector3d point) {
				return a.inside(point) && b.inside(point);
			});
		}
		
		/// <summary>
		/// Union of two regions
		/// </summary>
		public static Expression OR(IRegion a, IRegion b)
		{
			return new Expression(delegate (Vector3d point) {
				return a.inside(point) || b.inside(point);
			});
		}
		
		/// <summary>
		/// Exclusive disjunction of two regions
		/// </summary>
		public static Expression XOR(IRegion a, IRegion b)
		{
			return new Expression(delegate (Vector3d point) {
				return a.inside(point) ^ b.inside(point);
			});
		}
		
		/// <summary>
		/// Negation of the region
		/// </summary>
		public static Expression NOT(IRegion a)
		{
			return new Expression(delegate (Vector3d point) {
				return !a.inside(point);
			});
		}
	}
}