using System;
using System.Collections.Generic;
using TmatArt.Geometry;
using TmatArt.Scattering.Field;
using System.IO;

namespace TmatArt.Examples.Plot
{
	public class GnuplotFieldSaver
	{
		protected IEnumerable<Vector3d> points;
		protected Field field;
		protected String message;

		public GnuplotFieldSaver(Field field)
		{
			this.field  = field;
		}

		public void setPoints(IEnumerable<Vector3d> points)
		{
			this.points = points;
		}
		
		public void setMessage(String message)
		{
			this.message = message;
		}
		
		public void saveField(StreamWriter stream, Func<Vector3d, Vector3c, String> map)
		{
			if (message.Length > 0) {
				stream.Write(message);
			}

			foreach (Vector3d p in points) {
				Vector3c e = field.NearE(p);
				stream.WriteLine("{0}", map(p, e));
			}
		}
	}
}

