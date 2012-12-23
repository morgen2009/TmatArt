using System;
using System.Collections.Generic;

namespace TmatArt.Common.Attribute
{
	[System.AttributeUsage(System.AttributeTargets.Method)]
	public class Example : System.Attribute, IExample
	{
		public String  title;
		public String  text;
		
		public Type type;
		public String method;

		public ExampleStatus status;
		
		public Example (String title = "")
		{
			this.title    = title;
			this.text     = "";
			this.type     = null;
			this.method   = "";
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
			return this.method;
		}
		
		public void assignMethod(System.Reflection.MethodInfo method)
		{
			this.type = method.ReflectedType;
			this.method = method.Name;
			if (this.title == "") {
				this.title = method.Name;
			}
		}
		
		public void Run(RunContext context)
		{
			if (this.status == ExampleStatus.PREPARED && type != null) {
				Console.WriteLine(String.Format("Run {0}.{1}", this.type.Name, this.method));

				this.status = ExampleStatus.RUNNING;
				var instance = Activator.CreateInstance(this.type);
				type.GetMethod(this.method).Invoke(instance, null);

				this.status = ExampleStatus.COMPLETE;
			}
		}
	}
}

