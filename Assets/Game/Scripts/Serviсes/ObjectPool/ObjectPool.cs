using System;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Scripts.Serviсes.ObjectPool
{
    public class ObjectPool<T> where T : UnityEngine.Component
    {
        private readonly T _prefab;
        private readonly Transform _parent;
        private readonly Stack<T> _pool;
        private readonly List<T> _activeObjects;
        private readonly Action<T> _onGet;
        private readonly Action<T> _onReturn;
        private readonly Action<T> _onCreate;

        /// <summary>
        /// Количество объектов в пуле (неактивных)
        /// </summary>
        public int CountInPool => _pool.Count;

        /// <summary>
        /// Количество активных объектов
        /// </summary>
        public int CountActive => _activeObjects.Count;

        /// <summary>
        /// Создать новый Object Pool
        /// </summary>
        /// <param name="prefab">Префаб для создания объектов</param>
        /// <param name="parent">Родительский Transform для созданных объектов</param>
        /// <param name="initialSize">Начальный размер пула</param>
        /// <param name="onCreate">Действие при создании нового объекта</param>
        /// <param name="onGet">Действие при получении объекта из пула</param>
        /// <param name="onReturn">Действие при возврате объекта в пул</param>
        public ObjectPool(
            T prefab,
            Transform parent = null,
            int initialSize = 0,
            Action<T> onCreate = null,
            Action<T> onGet = null,
            Action<T> onReturn = null)
        {
            _prefab = prefab;
            _parent = parent;
            _pool = new Stack<T>(initialSize);
            _activeObjects = new List<T>();
            _onCreate = onCreate;
            _onGet = onGet;
            _onReturn = onReturn;

            // Предварительное создание объектов
            for (int i = 0; i < initialSize; i++)
            {
                var obj = CreateNewObject();
                obj.gameObject.SetActive(false);
                _pool.Push(obj);
            }
        }

        /// <summary>
        /// Получить объект из пула
        /// </summary>
        public T Get()
        {
            T obj;

            if (_pool.Count > 0)
            {
                obj = _pool.Pop();
            }
            else
            {
                obj = CreateNewObject();
            }

            obj.gameObject.SetActive(true);
            _activeObjects.Add(obj);

            // Вызов callback
            _onGet?.Invoke(obj);

            // Если объект реализует IPoolable
            if (obj is IPoolable poolable)
            {
                poolable.OnGetFromPool();
            }

            return obj;
        }

        /// <summary>
        /// Вернуть объект в пул
        /// </summary>
        public void Return(T obj)
        {
            if (obj == null) return;

            // Вызов callback
            _onReturn?.Invoke(obj);

            // Если объект реализует IPoolable
            if (obj is IPoolable poolable)
            {
                poolable.OnReturnToPool();
            }

            obj.gameObject.SetActive(false);
            _activeObjects.Remove(obj);
            _pool.Push(obj);
        }

        /// <summary>
        /// Вернуть все активные объекты в пул
        /// </summary>
        public void ReturnAll()
        {
            // Копируем список, так как Return модифицирует _activeObjects
            var objectsToReturn = new List<T>(_activeObjects);

            foreach (var obj in objectsToReturn)
            {
                Return(obj);
            }
        }

        /// <summary>
        /// Очистить пул и уничтожить все объекты
        /// </summary>
        public void Clear()
        {
            foreach (var obj in _activeObjects)
            {
                if (obj != null)
                    UnityEngine.Object.Destroy(obj.gameObject);
            }

            _activeObjects.Clear();

            while (_pool.Count > 0)
            {
                var obj = _pool.Pop();
                if (obj != null)
                    UnityEngine.Object.Destroy(obj.gameObject);
            }
        }

        private T CreateNewObject()
        {
            var obj = UnityEngine.Object.Instantiate(_prefab, _parent);
            _onCreate?.Invoke(obj);
            return obj;
        }
    }
}