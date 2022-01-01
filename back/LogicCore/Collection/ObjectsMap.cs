using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace LogicCore.Common.Collection
{
	/// <summary>
	/// Набор объектов по ключу
	/// </summary>
	public class ObjectsMap : IObjectsEditCollection<object>, IEnumerable<object>
	{
		public ObjectsMap()
		{
		}

		public ObjectsMap(IEnumerable objects)
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

		public ObjectsMap(params object[] objects)
			: this((IEnumerable<object>)objects)
		{
		}

		private readonly Dictionary<Type, object> _objects = new Dictionary<Type, object>();
		//private readonly List<Type> _keyTypes = new List<Type>();
	
		public object Find(Type tKey)
		{
			object result;
			_objects.TryGetValue(tKey, out result);
			return result;
		}

		public void Add<TKey>(object data)
		{
			Add(typeof(TKey), data);
			//добавляем конечный тип тоже
			if (UseKeyTypeOnly == false &&
				typeof(TKey) != data.GetType())
			{
				Add(data.GetType(), data);
			}
		}

		public void Add(object data)
		{
			Add(data.GetType(), data);
		}

		public void Add(Type tKey, object data)
		{
			_objects.Add(tKey, data);
		}

		public void Set(Type tKey, object data)
		{
			_objects[tKey] = data;
		}

		public void Set(object data)
		{
			Set(data.GetType(), data);
		}

		public void Add(IEnumerable<Type> tKeys, object data)
		{
			foreach (var tKey in tKeys)
			{
				_objects.Add(tKey, data);
			}
		}

		public TKey Get<TKey>()
		{
			var result = Find(typeof(TKey));
			if (result == null)
			{
				throw new ArgumentException("Запрошен неизвестный тип данных " + typeof(TKey));
			}
			return (TKey)result;
		}

		public TData Get<TKey, TData>()
			where TData : TKey
		{
			var result = Find(typeof(TKey));
			if (result == null)
			{
				throw new ArgumentException("Запрошен неизвестный тип данных " + typeof(TKey));
			}
			return (TData)result;
		}

		public TKey Find<TKey>()
		{
			var result = Find(typeof(TKey));
			return (TKey)result;
		}

		public TData Find<TKey, TData>()
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
		public IEnumerator<object> GetEnumerator()
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
