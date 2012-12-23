using System;

namespace TmatArt.Scattering.Field.Operation
{
	public interface IFieldOperation<T>
	{
		/// <summary>
		/// Selection of the specified field.
		/// </summary>
		/// <param name="field">Field.</param>
		void SetField(T field);
	}
}

