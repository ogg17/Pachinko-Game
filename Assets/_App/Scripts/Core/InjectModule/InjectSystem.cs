using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;
using Debug = VgGames.Core.CustomDebug.Debug;
using Object = UnityEngine.Object;

namespace VgGames.Core.InjectModule
{
    public static class InjectSystem
    {
        private static readonly Dictionary<Type, IInjectable> Instances = new();
        private static MonoBehaviour[] _allMonoBehaviours;

        public static void Add<T>(T injectable) where T : class, IInjectable => SetInstance(injectable);

        public static void InjectAllMono()
        {
            foreach (var monoBehaviour in _allMonoBehaviours) Inject(monoBehaviour);
        }

        public static void Inject(object obj)
        {
            var fields = obj.GetType().GetFields(BindingFlags.Instance | BindingFlags.NonPublic);
            foreach (var fieldInfo in fields)
            {
                if (!fieldInfo.GetCustomAttributes<Inject>(false).Any()) continue;
                if (fieldInfo.GetValue(obj) is not null) continue;
                fieldInfo.SetValue(obj, GetInstance(fieldInfo.FieldType));
                Debug.Log("Inject System: Injecting -> " +
                              $"Value: {fieldInfo.GetValue(obj).GetType().Name} -> Field: {fieldInfo.Name} in Object: {obj.GetType().Name}");
            }
        }

        public static void GetMonoBehaviour() => _allMonoBehaviours = Object.FindObjectsOfType<MonoBehaviour>();

        public static void Clear()
        {
            Instances.Clear();
            GetMonoBehaviour();
        }

        private static void SetInstance<T>(T injectable) where T : class, IInjectable
        {
            if(!Instances.TryAdd(typeof(T), injectable))
                throw new Exception($"Failed to Set {typeof(T)}: Instance has already been added!");
        }

        private static object GetInstance(Type type)
        {
            if (Instances.TryGetValue(type, out var instance)) return instance;
            throw new Exception($"Failed to Get {type}: Instance does not exist or was not added!");
        }
    }
}