using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LogicCore.Common.Collection
{
	/// <summary>
	/// Набор объектов по ключу
	/// </summary>
	public class ObjectsMap<TBaseObject> : IObjectsEditCollection<TBaseObject>, IEnumerable<TBaseObject>
		where TBaseObject : class
	{
		public ObjectsMap()
		{
		}

		public ObjectsMap(IEnumerable<TBaseObject> objects)
		{
			foreach (var obj in objects)
			{
				Add(obj);
			}
		}

		/// <summary>
		/// Использовать только ключевые типы для доступа (не добавлять конечные типы объектов)
		/// </summary>
		public bool UseKeyTypeOnly { get; set; }

		public ObjectsMap(params TBaseObject[] objects)
			: this((IEnumerable<TBaseObject>)objects)
		{
		}

		private readonly Dictionary<Type, TBaseObject> _objects = new Dictionary<Type, TBaseObject>();
		//private readonly List<Type> _keyTypes = new List<Type>();
	
		public TBaseObject Find(Type tKey)
		{
			TBaseObject result;
			_objects.TryGetValue(tKey, out result);
			return result;
		}

		public void Add<TKey>(TBaseObject data)
			where TKey : TBaseObject
		{
			Add(typeof(TKey), data);
			//добавляем конечный тип тоже
			if (UseKeyTypeOnly == false &&
				typeof(TKey) != data.GetType())
			{
				Add(data.GetType(), data);
			}
		}

		public void Add(TBaseObject data)
		{
			Add(data.GetType(), data);
		}

		public void Add(Type tKey, TBaseObject data)
		{
			_objects.Add(tKey, data);
		}

		public void Add(IEnumerable<Type> tKeys, TBaseObject data)
		{
			foreach (var tKey in tKeys)
			{
				_objects.Add(tKey, data);
			}
		}

		public TKey Get<TKey>()
			where TKey : TBaseObject
		{
			var result = Find(typeof(TKey));
			if (result == null)
			{
				throw new ArgumentException("Запрошен неизвестный тип данных " + typeof(TKey));
			}
			return (TKey)result;
		}

		public TData Get<TKey, TData>()
			where TKey : TBaseObject
			where TData : TKey
		{
			var result = Find(typeof(TKey));
			if (result == null)
			{
				throw new ArgumentException("Запрошен неизвестный тип данных " + typeof(TKey));
			}
			return (TData)result;
		}

		//public TData Get<TData>(TData @default)
		// where TData : TBaseObject
		//{
		//	object result;
		//	if (_objects.TryGetValue(typeof (TData), out result))
		//	{
		//		return  (TData)result;
		//	}
		//	return @default;
		//}

		public TKey Find<TKey>()
			where TKey : TBaseObject
		{
			var result = Find(typeof(TKey));
			return (TKey)result;
		}

		public TData Find<TKey, TData>()
			where TKey : TBaseObject
			where TData : TKey
		{
			var result = Find(typeof(TKey));
			return (TData)result;
		}

		/// <summary>
		/// Возвращает перечислитель, выполняющий итерацию в коллекции.
		/// </summary>
		/// <returns>
		/// Интерфейс <see cref="T:System.Collections.Generic.IEnumerator`1"/>, который может использоваться для перебора элементов коллекции.
		/// </returns>
		public IEnumerator<TBaseObject> GetEnumerator()
		{
			var result = _objects.Values.Distinct();
			return result.GetEnumerator();
		}

		/// <summary>
		/// Возвращает перечислитель, который осуществляет перебор элементов коллекции.
		/// </summary>
		/// <returns>
		/// Объект <see cref="T:System.Collections.IEnumerator"/>, который может использоваться для перебора элементов коллекции.
		/// </returns>
		IEnumerator IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}

		public IEnumerable<Type> Types
		{
			get
			{
				return _objects.Keys;
			}
		}
	}
}
