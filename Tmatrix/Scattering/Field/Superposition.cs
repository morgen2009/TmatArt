using System;
using System.Linq;
using System.Collections.Generic;
using TmatArt.Geometry;
using TmatArt.Numeric.Mathematics;

namespace TmatArt.Scattering.Field
{
	/// <summary>
	/// Compute superposition of fields (with weights)
	/// </summary>
	public class Superposition : Field
	{
		public List<KeyValuePair<Field, Complex>> fields;

		/// <summary>
		/// Initializes a new instance of the <see cref="TmatArt.Scattering.Field.Superposition"/> class.
		/// </summary>
		/// <param name="fields">Array of fields.</param>
		public Superposition (IEnumerable<Field> fields)
		{
			this.fields = new List<KeyValuePair<Field, Complex>>();

			foreach (Field f in fields) {
				this.AddField(f);
			}
		}

		/// <summary>
		/// Adds the field into superposition
		/// </summary>
		/// <param name="field">Field.</param>
		/// <param name="weight">Weight.</param>
		public void AddField(Field field, Complex weight)
		{
			this.fields.Add(new KeyValuePair<Field, Complex>(field, weight));
		}
		
		/// <summary>
		/// Adds the field into superposition with default weight (1)
		/// </summary>
		/// <param name="field">Field.</param>
		public void AddField(Field field)
		{
			this.AddField(field, Complex.ONE);
		}
		
		/// <see cref="TmatArt.Scattering.Field.NearE"/>
		public override Vector3c NearE (Vector3d r)
		{
			return fields.Sum( f => f.NearE(r) );
		}
		
		/// <see cref="TmatArt.Scattering.Field.FarE"/>
		public override Vector3c FarE (Euler e)
		{
			return fields.Sum( f => f.FarE(e) );
		}
		
		/// <see cref="TmatArt.Scattering.Operation<T>"/>
		public override T Resolve<T> ()
		{
			// TODO
			return default(T);
		}
	}

	public static class SuperpositionExtension
	{
		public static Vector3c Sum(this IEnumerable<KeyValuePair<Field, Complex>> fields, Func<Field, Vector3c> func)
		{
			Vector3c res = new Vector3c();
			
			foreach (var item in fields) {
				res = res + func(item.Key) * item.Value;
			}
			
			return res;
		}
	}
}

