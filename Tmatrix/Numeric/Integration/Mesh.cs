using System;
using System.Collections.Generic;

namespace TmatArt.Numeric.Integration
{
	/**
	 * Struct with point and weight of the current node
	 * 
	 * @author Vladimir Schmidt
	 * @date 28 Sep 2012
	 */
	public struct MeshNode
	{
		public double point;
		public double weight;
	}
	
	/**
	 * Abstract class to construct integration rule
	 * 
	 * @author Vladimir Schmidt
	 * @date 28 Sep 2012
	 */
	public abstract class Mesh
	{
		/**
		 * Left bound of the range
		 */
		protected double _left;
		public double left 
		{
			get
			{
				return this._left;
			}
		}

		/**
		 * Right bound of the range
		 */
		protected double _right; 
		public double right
		{
			get
			{
				return this._right;
			}
		}

		/**
		 * Normalization factor
		 */
		protected double _norm;
		public double norm
		{
			get
			{
				return this._norm;
			}
		}
		
		/**
		 * Iteration over nodes
		 */
		public abstract IEnumerable<MeshNode> nodes();
		
		/**
		 * Constructor
		 */
		public Mesh(double _left, double _right, double _norm = 1E0)
		{
			this._left  = _left;
			this._right = _right;
			this._norm  = _norm;
		}
	}
}

