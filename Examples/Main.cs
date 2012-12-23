using System;
using TmatArt.Common.Attribute;
using TmatArt.Common;

namespace Examples
{
	class MainClass
	{
		public static void Main (string[] args)
		{
			String pattern = "[^.]*";
			if (args.Length > 0) {
				pattern = args[0];
			}
			var context = new RunContext("./data/");
			context.AddExamples(Container.Instance().Find(pattern));
			context.Execute();
		}
	}
}
