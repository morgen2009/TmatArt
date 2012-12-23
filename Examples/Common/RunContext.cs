using System;
using System.Collections.Generic;
using TmatArt.Common.Attribute;

namespace TmatArt.Common
{
	public class RunContext
	{
		public List<IExample> task;
		public String path;

		public RunContext (String path)
		{
			this.task = new List<IExample>();
			this.path = path;
		}

		public void AddExamples(IEnumerable<IExample> examples)
		{
			foreach (IExample item in examples) {
				this.AddExample(item);
			}
		}

		public void AddExample(IExample item)
		{
			if (this.task.Contains(item)) {
				this.task.Remove(item);
			}
			this.task.Add(item);
			item.SetStatus(ExampleStatus.PREPARED);
		}

		public void Execute()
		{
			foreach (IExample item in task) {
				if (item.GetStatus() == ExampleStatus.PREPARED) {
					item.Run(this);
				}
			}
		}
	}
}

