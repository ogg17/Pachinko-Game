using UnityEngine;
using VgGames.Core.EventBusModule;
using VgGames.Core.InjectModule;
using VgGames.Game.Signals;
using VgGames.Game.Sound;

namespace VgGames.Game.Obstacles.EndObstacles
{
    [RequireComponent(typeof(ObstacleCollision))]
    public class ObstacleEndSound : MonoBehaviour
    {
        [Inject] private EventBus _eventBus;
        
        private readonly SoundSignal _sound = new() { Type = SoundType.Coin };
        
        private ObstacleCollision _collision;

        private void Awake()
        {
            _collision = GetComponent<ObstacleCollision>();
            _collision.onCollision += OnCollision;
        }

        private void OnCollision(Collision2D col)
        {
            _eventBus.Invoke(_sound);
        }
    }
}