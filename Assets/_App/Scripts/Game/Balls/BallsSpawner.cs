using UnityEngine;
using VgGames.Core.InjectModule;
using VgGames.Game.RuntimeData;

namespace VgGames.Game.Balls
{
    public class BallsSpawner
    {
        [Inject] private BallsData _ballsData;
        
        private readonly BallsPool _pool;
        private readonly Transform _spawnPoint;
        private readonly float _random;

        public BallsSpawner(BallsPool pool, Transform spawnPoint, float random = 0.25f)
        {
            _pool = pool;
            _spawnPoint = spawnPoint;
            _random = random;
            
            InjectSystem.Inject(this);
        }

        public void Spawn()
        {
            if(_ballsData.Balls <= 0) return;
            
            var obj = _pool.GetObject<BallScript>();
            obj.transform.position = _spawnPoint.position + 
                                     (Vector3)Random.insideUnitCircle * _random;
            obj.StartBall();
            _ballsData.Balls.Value--;
        }
    }
}