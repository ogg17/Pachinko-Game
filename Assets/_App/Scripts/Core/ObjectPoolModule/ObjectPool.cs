using System;
using System.Collections.Generic;
using System.Linq;
using Cysharp.Threading.Tasks;
using JetBrains.Annotations;
using UnityEngine;
using Object = UnityEngine.Object;

namespace VgGames.Core.ObjectPoolModule
{
    public class ObjectPool
    {
        private readonly GameObject _prefab;
        private readonly Transform _parent;
        private readonly bool _isExtendable;
        
        private readonly List<Component> _pool = new();
        
        public ObjectPool(string prefabName, int capacity, bool isExtendable = true, Transform parent = null)
        {
            _prefab = GetPrefab(prefabName);
            _parent = parent;
            _isExtendable = isExtendable;
            CreatePool(capacity);
        }

        public ObjectPool(GameObject prefab, int capacity, bool isExtendable = true, Transform parent = null)
        {
            _prefab = prefab;
            _parent = parent;
            _isExtendable = isExtendable;
            CreatePool(capacity);
        }
        
        public virtual List<T> GetObjects<T>() where T : Component
            => _pool.OfType<T>().ToList();
        
        public virtual T GetObject<T>() where T : Component => GetObject<T>(_parent);

        [CanBeNull] public virtual T GetObject<T>(Transform parent) where T : Component
        {
            Component obj = _pool.OfType<T>().FirstOrDefault(o => o.gameObject.activeSelf == false);
            if (obj == null)
            {
                if (_isExtendable == false) return null;
                if (Instantiate(parent).TryGetComponent(typeof(T), out obj))
                    _pool.Add(obj);
                else throw new Exception($"Error: Component {typeof(T)} not found on Prefab!");
            }
            else obj.gameObject.SetActive(true);
            return obj as T;
        }
        
        public virtual async UniTask<T> GetObjectAsync<T>(Transform parent) where T : Component
        {
            var pool = _pool.OfType<T>();
            if (pool.ToList().Count <= 0) return null;
            await UniTask.WaitUntil(() => pool.Any(o => o.gameObject.activeSelf == false));
            Component obj = _pool.OfType<T>().First(o => o.gameObject.activeSelf == false);
            obj.gameObject.SetActive(true);
            return obj as T;
        }

        public void Disable(Component obj) => obj.gameObject.SetActive(false);

        protected virtual GameObject Instantiate() =>
            Instantiate(_parent);
        
        protected virtual GameObject Instantiate(Transform parent) =>
            Object.Instantiate(_prefab, parent);

        private GameObject GetPrefab(string prefabName)
        {
            var prefab = Resources.Load<GameObject>(prefabName);
            if (prefab == null) throw new Exception("Invalid prefab name!");
            return prefab;
        }

        private void CreatePool(int capacity)
        {
            if (capacity <= 0) return;
            for (var i = 0; i < capacity; i++)
            {
                var obj = Instantiate(_parent);
                obj.SetActive(false);
            }
        }
    }
}