using System;

namespace TmatArt.Scattering.Field.Operation
{
	public interface IFieldOperation
	{
		/// <summary>
		/// Selection of the specified field.
		/// </summary>
		/// <param name="field">Field.</param>
		IFieldOperation Select(Field field);
	}
}

