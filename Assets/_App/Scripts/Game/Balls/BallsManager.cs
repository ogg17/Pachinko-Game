using UnityEngine;
using VgGames.Core.EventBusModule;
using VgGames.Core.Extra;
using VgGames.Core.Extra.GameState;
using VgGames.Core.InjectModule;
using VgGames.Game.Signals;

namespace VgGames.Game.Balls
{
    public class BallsManager : MonoBehaviour, IInit, IGameActivate, IGameDeactivate
    {
        [Inject] private EventBus _eventBus;

        [SerializeField] private GameObject prefab;
        [SerializeField] private Transform spawnPoint;
        [SerializeField] private float spawnRate;

        private BallsPool _ballsPool;
        private BallsSpawner _ballsSpawner;

        public void Init()
        {
            _ballsPool = new BallsPool(prefab, 5);
            _ballsSpawner = new BallsSpawner(_ballsPool, spawnPoint);
        }

        public void GameActivate() => _eventBus.Add<InputSignal>(OnInput);

        private void OnInput(InputSignal signal)
        {
            if (signal.IsPush)
                IsPushed();
            else StopPushed();
        }

        private void IsPushed() => InvokeRepeating(nameof(Spawn), spawnRate, spawnRate);

        private void StopPushed() => CancelInvoke();

        private void Spawn() => _ballsSpawner.Spawn();

        public void GameDeactivate()
        {
            _eventBus.Remove<InputSignal>(OnInput);
        }
    }
}