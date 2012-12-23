using System;
using System.Collections.Generic;

namespace TmatArt.Numeric
{
	/**
	 * Compare double with given threshold (for LINQ)
	 * 
	 * @author Vladimir Schmidt
	 * @date 29.09.2012
	 */
	public class DoubleEqualityComparer: IEqualityComparer<double>
	{
        private readonly double epsilon;

        public DoubleEqualityComparer(double epsilon)
        {
            if (epsilon < 0)
                throw new ArgumentException("epsilon can't be negative", "epsilon");

            this.epsilon = epsilon;
        }

        public bool Equals(double x, double y)
        {
			//Console.WriteLine(String.Format("{0}=={1} ? {2}", x, y, System.Math.Abs(x - y) < this.epsilon));
            return System.Math.Abs(x - y) < this.epsilon;
        }

        public int GetHashCode(double obj)
        {
            return 0;
        }
	}
	
	public static class DoubleExtension
	{
		/**
		 * Indian algorithm to compute x^y, where y is integer
		 */
		public static double Pow(this double obj, int degree)
		{
			double x = obj;
			double res = (degree % 2 > 0) ? x : 1;
			while (true)
			{
				degree = (int)(degree / 2);
				x *= x;
				if (degree % 2 > 0) res *= x;
				if (degree == 0) return res;
			}
		}
	}
}

