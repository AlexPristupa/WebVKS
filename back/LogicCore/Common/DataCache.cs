using System;
using System.Collections.Generic;
using System.Text;
using LogicCore.Common.Collection;

namespace LogicCore
{
    public class DataCache<T>
    {
        readonly object _locker = new object();
        readonly Func<T> _loader;
        DateTime _update;
        DateTime _access;
        T _data;
        ObjectsMap _extended;

        public DataCache(Func<T> loader)
        {
            _loader = loader;
        }

        public Action<T, ObjectsMap> FillExtended;

        /// <summary>
        /// Максимальное время жизни данных в кэше
        /// </summary>
        public TimeSpan CacheLifeTime { get; set; } = TimeSpan.FromMinutes(30);

        /// <summary>
        /// Максимальное время жизни данных при простое
        /// </summary>
        public TimeSpan CacheIdleWait { get; set; } = TimeSpan.FromMinutes(1);

        public virtual T GetData()
        {
            lock (_locker)
            {
                var now = DateTime.Now;
                if (now - _update > CacheLifeTime ||
                    now - _access > CacheIdleWait)
                {
                    Update();
                }
                _access = DateTime.Now;
                return _data;
            }
        }

        [Obsolete]
        public T Data => GetData();

        public void Update()
        {
            lock (_locker)
            {
                _extended = null;
                _data = default(T); //отпускаем данные
                _data = _loader();
                _update = DateTime.Now;
                FillExtended?.Invoke(_data, _extended = new ObjectsMap());
            }
        }

        public void Update(T data, bool resetTime)
        {
            lock (_locker)
            {
                _extended = null;
                _data = data;
                if (resetTime) _update = DateTime.Now;
                FillExtended?.Invoke(_data, _extended = new ObjectsMap());
            }
        }

        public TExt GetExtended<TExt>() where TExt : class
        {
            return _extended?.Find<TExt>();
        }
    }

    public class DataCacheWithCheck<TData, TId> : DataCache<TData> where TId : struct
    {
        private Func<TId> _getLastId;
        private TId? _lastId;

        public DataCacheWithCheck(Func<TData> loader, Func<TId> getLastId) : base(loader)
        {
            _getLastId = getLastId;
            CacheIdleWait = TimeSpan.FromMinutes(10);
        }

        public override TData GetData()
        {
            if (_lastId == null)
            {
                _lastId = _getLastId();
            } 
            else
            {
                var id = _getLastId();
                if (_lastId == null || Equals(id, _lastId.Value) == false)
                {
                    Update();
                    _lastId = id;
                }
            }
            return base.GetData();
        }

        public void Update(TData data, TId lastId, bool resetTime)
        {
            Update(data, resetTime);
            _lastId = lastId;
        }

        public TId? LastId => _lastId;
    }
}