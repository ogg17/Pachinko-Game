using UnityEngine;
using VgGames.Core.EventBusModule;
using VgGames.Core.InjectModule;
using VgGames.Game.Signals;

namespace VgGames.Game.Obstacles.EndObstacles
{
    [RequireComponent(typeof(ObstacleCollision))]
    public class ObstacleEnd : MonoBehaviour
    {
        [Inject] private EventBus _eventBus;

        private BallEndCollisionSignal _signal;
        private ObstacleCollision _collision;

        private void Awake()
        {
            _collision = GetComponent<ObstacleCollision>();
            _collision.onCollision += OnCollision;
        }

        private void OnCollision(Collision2D other)
        {
            _eventBus.Invoke(_signal);
        }
    }
}