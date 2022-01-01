using System;
using System.Collections;
using System.Collections.Generic;

namespace LogicCore.Common.Collection
{
	public interface IObjectsCollection<TBaseObject> : IEnumerable
	{
		TBaseObject Find(Type tKey);
		
		TKey Find<TKey>() 
			where TKey : TBaseObject;

		TData Find<TKey, TData>()
			where TKey : TBaseObject
			where TData : TKey;
		
		TKey Get<TKey>() 
			where TKey : TBaseObject;

		TData Get<TKey, TData>()
			where TKey : TBaseObject
			where TData : TKey;

		IEnumerable<Type> Types { get; }
	}

	public interface IObjectsEditCollection<TBaseObject> : IObjectsCollection<TBaseObject>
	{
		void Add<TKey>(TBaseObject data)
			where TKey : TBaseObject;

		void Add(TBaseObject data);

		void Add(Type tKey, TBaseObject data);

		void Add(IEnumerable<Type> tKey, TBaseObject data);
	}
}