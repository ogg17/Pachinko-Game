using System;
using UniRx;
using UnityEngine;
using VgGames.Core.Config;
using Debug = VgGames.Core.CustomDebug.Debug;

namespace VgGames.Game.RuntimeData
{
    public class SaveReactiveData<T>
    {
        public readonly ISubject<T> OnChange = new Subject<T>();

        private T _value;
        private bool _isLoaded;
        private readonly string _name;
        private readonly Predicate<T> _predicate;
        private readonly bool _savable;

        public T Value
        {
            get
            {
                if (!_savable) return _value;
                
                if (!PlayerPrefs.HasKey(_name + GameConfig.Hash) || _isLoaded) return _value;
                _value = JsonUtility.FromJson<Wrapper<T>>(PlayerPrefs.GetString(_name + GameConfig.Hash)).value;
                _isLoaded = true;
                OnChange.OnNext(_value);

                return _value;
            }
            set
            {
                if (_predicate != null && !_predicate.Invoke(value))
                {
                    Debug.Log($"{_name}:{typeof(T)} Predicate block!");
                    OnChange.OnNext(_value);
                    return;
                }

                _value = value;
                OnChange.OnNext(_value);

                if (!_savable) return;
                
                PlayerPrefs.SetString(_name + GameConfig.Hash,
                    JsonUtility.ToJson(new Wrapper<T> { value = _value }));
                PlayerPrefs.Save();
            }
        }

        public bool HasSave => PlayerPrefs.HasKey(_name + GameConfig.Hash);
        public bool IsLoaded => _isLoaded;

        public SaveReactiveData(T value, string name, Predicate<T> predicate = null, bool savable = true)
        {
            _name = name;
            _predicate = predicate;
            _savable = savable;
            if(!HasSave) Value = value;
        }

        public static implicit operator T(SaveReactiveData<T> data) => data.Value;
    }

    [Serializable]
    public class Wrapper<T>
    {
        public T value;
    }
}