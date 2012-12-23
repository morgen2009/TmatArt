using System;
using System.Linq;
using TmatArt.Common.Attribute;
using System.Text.RegularExpressions;
using System.Collections.Generic;

namespace TmatArt.Common
{
	public class Container
	{
		private static Container instance;
		private Container()
		{
			this.top = new ExampleGroup("Main");
			this.top.assignClass(typeof(Container));
			this.LoadFromAnnotation();
		}
		public static Container Instance()
		{
			if (Container.instance == null) {
				Container.instance = new Container();
			}
			return Container.instance;
		}

		public ExampleGroup top;

		private void LoadFromAnnotation()
		{
			ExampleGroup top = this.top;

			System.Reflection.Assembly.GetExecutingAssembly().GetTypes().ToList().ForEach(delegate (Type type) {
				ExampleGroup parent = (type.GetCustomAttributes(typeof(ExampleGroup), false) as ExampleGroup[]).DefaultIfEmpty(null).First();

				if (parent != null) {
					parent.assignClass(type);

					foreach (var m in type.GetMethods()) {
						Example example = (m.GetCustomAttributes(typeof(Example), false) as Example[]).DefaultIfEmpty(null).First();
						if (example != null) {
							example.assignMethod(m);
							parent.children.Add(example);
						}
					};

					top.children.Add(parent);
				}
			});

			this.top = top;
		}

		public IEnumerable<IExample> Find(String pattern)
		{
			Regex reg = new Regex(pattern, RegexOptions.IgnoreCase);
			return this.Find(this.top, reg);
		}

		private IEnumerable<IExample> Find(IExample current, Regex pattern, String prefix = "")
		{
			String name = prefix + "." + current.GetName();
			if (pattern.IsMatch(name)) {
				yield return current;
			}

			if (current is ExampleGroup) {
			foreach (IExample item in (current as ExampleGroup).children) {
					foreach (IExample found in this.Find(item, pattern, name)) {
						yield return found;
					}
				}
			}
		}
	}
}

