using UnityEngine;
using VgGames.Core.EventBusModule;
using VgGames.Core.InjectModule;
using VgGames.Game.Signals;
using VgGames.Game.Sound;

namespace VgGames.Game.Obstacles
{
    [RequireComponent(typeof(ObstacleCollision))]
    public class ObstacleSound : MonoBehaviour
    {
        [Inject] private EventBus _eventBus;
        
        [SerializeField] private bool isDestroy;
        
        private readonly SoundSignal _sound = new() { Type = SoundType.Click };
        private readonly SoundSignal _loseSound = new() { Type = SoundType.Explosion };
        
        private ObstacleCollision _collision;

        private void Awake()
        {
            _collision = GetComponent<ObstacleCollision>();
            _collision.onCollision += OnCollision;
        }

        private void OnCollision(Collision2D col)
        {
            _eventBus.Invoke(isDestroy ? _loseSound : _sound);
        }
    }
}