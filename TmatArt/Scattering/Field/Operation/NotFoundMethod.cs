using System;

namespace TmatArt.Scattering.Field.Operation
{
	public class NotFoundMethod: System.Exception
	{
		public NotFoundMethod (Type operation, Type field) : base(GetMessage(operation, field))
		{
		}

		private static String GetMessage(Type operation, Type field)
		{
			return String.Format("Operation {0} for the class {1} is not specified", operation.Name, field.FullName);
		}
	}
}

