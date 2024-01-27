using System;
using System.Collections.Generic;
using System.Linq;
using VgGames.Core.InjectModule;
using Debug = VgGames.Core.CustomDebug.Debug;

namespace VgGames.Core.EventBusModule
{
    public class EventBus : IInjectable
    {
        private readonly Dictionary<string, List<EventCallback>> _events = new ();

        public void Add<T>(Action<T> callback, int order = 0)
        {
            var key = typeof(T).Name;
            if (_events.TryGetValue(key, out var e))
                e.Add(new EventCallback(callback, order));
            else
                _events.Add(key, new List<EventCallback> { new(callback, order) });

            _events[key] = _events[key].OrderByDescending(x => x.Order).ToList();
        }

        public void Remove<T>(Action<T> callback)
        {
            var key = typeof(T).Name;
            if (_events.TryGetValue(key, out var e))
            {
                var neededCallback = e.FirstOrDefault(c => c.Callback.Equals(callback));
                if (neededCallback != null) e.Remove(neededCallback);
            }
            else Debug.Log($"Failed to Remove {key}: Signal does not exist or was not added!");
        }

        public void Invoke<T>(T signal)
        {
            var key = typeof(T).Name;
            if (_events.TryGetValue(key, out var events))
            {
                for (var i = 0; i < events.Count; i++)
                {
                    var e = events[i];
                    var callback = e.Callback as Action<T>;
                    callback?.Invoke(signal);
                }
            }
            else Debug.Log($"Failed to Invoke {key}: Signal does not exist or was not added!");
        }
    }
}