using UnityEngine;

namespace VgGames.Core.RandomValue
{
    [System.Serializable]
    public class RandomInt
    {
        [SerializeField] private int min;
        [SerializeField] private int max;

        public RandomInt(int s, int e)
        {
            min = s;
            max = e;
        }
        
        private int? _value;
        private int Value
        {
            get
            {
                _value = Random.Range(min, max);
                return (int)_value;
            }
        }

        public int GetImmutableValue() => _value ?? Value;
        
        public static implicit operator int(RandomInt v) => v.Value;
    }
}