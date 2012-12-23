using System;
using System.Collections.Generic;

namespace TmatArt.Scattering.Field.Operation
{
	/// <summary>
	/// IoC container for IFieldOperation services
	/// </summary>
	/// <param name="T">Field class</param>
	public class ServiceContainer<T>
	{
		/// <summary>
		/// Relation between "IFieldOperation interface" and "class name"
		/// </summary>
		private readonly IDictionary<Type, Type> service;

		/// <summary>
		/// Constructor
		/// </summary>
		public ServiceContainer ()
		{
			this.service = new Dictionary<Type, Type>();
		}

		/// <summary>
		/// Register relation
		/// </summary>
		/// <typeparam name="TI">Interface to be registered</typeparam>
		/// <typeparam name="TC">Class implementing this interface</typeparam>
		public void Register<Ti, Tc>() where Tc: Ti, IFieldOperation<T>
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
			var obj = (Ti) Resolve(typeof(Ti));
			(obj as IFieldOperation<T>).SetField(field);
			return obj;
		}

		private object Resolve(Type type)
		{
			Type resolvedType = this.ResolvedType(type);
			if (resolvedType == null) {
				throw new Operation.NotFoundMethod (type, this.GetType());
			}
			return Activator.CreateInstance(resolvedType);
		}
	}
}

