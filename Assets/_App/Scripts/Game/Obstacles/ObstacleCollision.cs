using System;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace VgGames.Game.Obstacles
{
    public class ObstacleCollision : MonoBehaviour
    {
        [SerializeField] private float collisionDelay;
        
        public Action<Collision2D> onCollision;
        
        private bool _isCol;

        private void OnCollisionEnter2D(Collision2D other)
        {
            if (_isCol) return;
            _isCol = true;
            onCollision?.Invoke(other);
            CollisionDelay().Forget();
        }

        private async UniTaskVoid CollisionDelay()
        {
            await UniTask.WaitForSeconds(collisionDelay);
            _isCol = false;
        }
    }
}