using System;

namespace TmatArt.Scattering.Field.Operation
{
	public class NotFoundException: System.Exception
	{
		public NotFoundException (Type operation, Type field) : base(GetMessage(operation, field))
		{
		}

		private static String GetMessage(Type operation, Type field)
		{
			return String.Format("Operation {0} for the class {1} is not found", operation.Name, field.FullName);
		}
	}
}

