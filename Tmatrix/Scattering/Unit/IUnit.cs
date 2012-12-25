using System;

namespace TmatArt.Scattering
{
	public interface IUnit<T, TEnum>
	{
		T In(TEnum unit);
	}
}

