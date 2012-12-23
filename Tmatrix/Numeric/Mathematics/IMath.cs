using System;
using System.Collections;

namespace TmatArt.Numeric.Mathematics
{
	public interface IMath<T, TComparer>
	{
		T Cos(T arg);
		T Sin(T arg);
		T Tan(T arg);
		T Log(T arg);
		T Exp(T arg);
		T Sqrt(T arg);
		T Pow(T arg, int deg);
		T Pow(T arg, TComparer deg);
		T Pow(T arg, T deg);
		TComparer Abs(T arg);
	}
}

