using System;
using UniRx;
using UnityEngine;

namespace VgGames.Game.RuntimeData
{
    public class SaveReactiveInt
    {
        public readonly ISubject<int> OnChange = new Subject<int>();

        private int _value;
        private bool _isLoaded;
        private readonly string _name;
        private readonly Predicate<int> _predicate;
        private readonly bool _savable;

        public int Value
        {
            get
            {
                if (!_savable) return _value;
                
                if (!PlayerPrefs.HasKey(_name) || _isLoaded) return _value;
                _value = PlayerPrefs.GetInt(_name);
                _isLoaded = true;
                OnChange.OnNext(_value);

                return _value;
            }
            set
            {
                if (_predicate != null && !_predicate.Invoke(value))
                {
                    Debug.Log($"{_name} Predicate block!");
                    OnChange.OnNext(_value);
                    return;
                }

                _value = value;
                OnChange.OnNext(_value);

                if (!_savable) return;
                
                PlayerPrefs.SetInt(_name, _value);
                PlayerPrefs.Save();
            }
        }

        public bool HasSave => PlayerPrefs.HasKey(_name);
        public bool IsLoaded => _isLoaded;

        public SaveReactiveInt(int value, string name, Predicate<int> predicate = null, bool savable = true)
        {
            _name = name;
            _predicate = predicate;
            _savable = savable;
            if(!HasSave) Value = value;
        }

        public static int ToInt(SaveReactiveData<int> data) => data.Value;
    }
}