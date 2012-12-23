using System;
using System.Collections.Generic;

namespace TmatArt.Common
{
	public static class Limit
	{
		public static IEnumerable<double> Range(double left, double right, double step = 1)
		{
			if (left >= right || step <= 0) {
				yield break;
			}

			double current = left;
			while (current <= right) {
				yield return current;
				current += step;
			}
		}
	}
}

