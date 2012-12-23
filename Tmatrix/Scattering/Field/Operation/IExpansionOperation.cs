using System;

namespace TmatArt.Scattering.Field.Operation
{
	public interface IExpansionOperation
	{
		/// <summary>
		/// Expansion of the field in the regular SVWF
		/// </summary>
		Expansion Expansion(int nrank, int mrank);
	}
}

