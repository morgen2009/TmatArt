using System;
using System.Collections.Generic;

namespace TmatArt.Scattering.Field.Operation
{
	/// <summary>
	/// IoC container for IFieldOperation services
	/// </summary>
	/// <param name="T">Field class</param>
	public class Container<T>
	{
		/// <summary>
		/// Relation between "IFieldOperation interface" and "class name"
		/// </summary>
		private readonly IDictionary<Type, Type> service;

		/// <summary>
		/// Constructor
		/// </summary>
		public Container()
		{
			this.service = new Dictionary<Type, Type>();
		}

		/// <summary>
		/// Register relation
		/// </summary>
		/// <typeparam name="TI">Interface to be registered</typeparam>
		/// <typeparam name="TC">Class implementing this interface</typeparam>
		public void Register<Ti, Tc>() where Tc: Ti
		{
			this.service.Add(typeof(Ti), typeof(Tc));
		}

		/// <summary>
		/// Get registered relation
		/// </summary>
		/// <param name="type">Type.</param>
		public Type ResolvedType(Type type)
		{
			return this.service[type];
		}

		/// <summary>
		/// Resolve the IFieldOperation (return instance of the related class)
		/// </summary>
		/// <typeparam name="Ti">IFieldOperation interface to be resolved</typeparam>
		public Ti Resolve<Ti>(T field)
		{
			var obj = (Ti) Resolve(typeof(Ti), field);
			return obj;
		}

		private object Resolve(Type type, T field)
		{
			Type resolvedType = this.ResolvedType(type);
			if (resolvedType == null) {
				throw new Operation.NotFoundException (type, this.GetType());
			}

			return Activator.CreateInstance(resolvedType, field);
		}
	}
}

