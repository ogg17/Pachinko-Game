using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using VgGames.Core.InjectModule;
using Object = UnityEngine.Object;

namespace VgGames.Core.MonoBehaviours
{
    public class MonoBehavioursContainer : IInjectable
    {
        private readonly Dictionary<Type, List<MonoBehaviour>> _monoBehaviours = new();

        public void Add<T>() where T : MonoBehaviour
        {
            var key = typeof(T);
            var monoBehaviours = Object.FindObjectsOfType<T>().Select(t => t as MonoBehaviour).ToList();
            if (monoBehaviours == null || monoBehaviours.Count == 0)
                throw new Exception($"Failed to Add {key.Name}: MonoBehaviour does not exist on scene!");

            if (_monoBehaviours.TryAdd(key, monoBehaviours)) return;
            throw new Exception($"Failed to Add {key.Name}: MonoBehaviour has already been added!");
        }

        public T Get<T>() where T : MonoBehaviour
        {
            var key = typeof(T);
            if (_monoBehaviours.TryGetValue(key, out var value))
                return value.FirstOrDefault() as T;
            throw new Exception($"Failed to delete {key.Name}: MonoBehaviour does not exist or was not added");
        }

        public TM Get<TM>(Type key) where TM : MonoBehaviour
        {
            if (_monoBehaviours.TryGetValue(key, out var value))
                return value.FirstOrDefault(m => m is TM) as TM;
            throw new Exception($"Failed to delete {key.Name}: MonoBehaviour does not exist or was not added");
        }

        public List<T> GetAll<T>() where T : MonoBehaviour
        {
            var key = typeof(T);
            if (_monoBehaviours.TryGetValue(key, out var value))
                return value.Select(m => m as T).ToList();
            throw new Exception($"Failed to delete {key.Name}: MonoBehaviour does not exist or was not added");
        }

        public List<MonoBehaviour> GetAll()
        {
            var all = new List<MonoBehaviour>();
            foreach (var list in _monoBehaviours.Values) all.AddRange(list);
            return all;
        }

        public void Clear() => _monoBehaviours.Clear();
    }
}