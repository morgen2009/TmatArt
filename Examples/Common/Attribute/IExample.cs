using System;

namespace TmatArt.Common
{
	public enum ExampleStatus { SKIPED, PREPARED, RUNNING, COMPLETE };

	public interface IExample
	{
		ExampleStatus GetStatus();
		void SetStatus(ExampleStatus status);

		String GetName();
		void Run(RunContext context);
	}
}

