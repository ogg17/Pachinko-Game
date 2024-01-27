using UnityEngine;

namespace VgGames.Core.Config
{
    [CreateAssetMenu(fileName = nameof(GameConfigStorage), 
        menuName = GameConfig.StorageMenuEntry + nameof(GameConfigStorage), order = 0)]
    public class GameConfigStorage : ScriptableObject
    {
        [SerializeField] private bool isDebug;
        [Tooltip("Timer Interval in Seconds")]
        [SerializeField] private int timerInterval = 3600;
        public bool IsDebug => isDebug;
        public int TimerInterval => timerInterval;
    }
}