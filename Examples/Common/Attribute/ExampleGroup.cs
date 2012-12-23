using System;
using System.Collections.Generic;

namespace TmatArt.Common.Attribute
{
	[System.AttributeUsage(System.AttributeTargets.Class)]
	public class ExampleGroup : System.Attribute, IExample
	{
		public String  title;
		public String  text;

		public Type type;
		public List<IExample> children;

		public ExampleStatus status;

		public ExampleGroup (String title = "")
		{
			this.title    = title;
			this.text     = "";
			this.type     = null;
			this.children = new List<IExample>();
		}

		public ExampleStatus GetStatus()
		{
			return this.status;
		}
		
		public void SetStatus(ExampleStatus status)
		{
			this.status = status;
		}
		
		public String GetName()
		{
			if (this.type != null) {
				return this.type.Name;
			} else {
				return "";
			}
		}
		
		public void assignClass(Type type)
		{
			this.type = type;
			if (this.title == "") {
				this.title = type.Name;
			}
		}

		public void Run(RunContext context)
		{
			if (this.status == ExampleStatus.PREPARED && type != null) {
				Console.WriteLine(String.Format("Run {0}", this.type.Name));

				this.status = ExampleStatus.RUNNING;
				children.ForEach(delegate(IExample e) {
					e.Run(context);
				});
				this.status = ExampleStatus.COMPLETE;
			}
		}
	}
}

