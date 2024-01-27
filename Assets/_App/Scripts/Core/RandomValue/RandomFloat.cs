using UnityEngine;

namespace VgGames.Core.RandomValue
{
    [System.Serializable]
    public class RandomFloat
    {
        [SerializeField] private float min;
        [SerializeField] private float max;

        public RandomFloat(float s, float e)
        {
            min = s;
            max = e;
        }
        
        private float? _value;
        private float Value
        {
            get
            {
                _value = Random.Range(min, max);
                return (float)_value;
            }
        }

        public float GetImmutableValue() => _value ?? Value;
        
        public static implicit operator float(RandomFloat v) => v.Value;
    }
}