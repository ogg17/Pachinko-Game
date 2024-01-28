using UnityEngine;

namespace VgGames.Game.Obstacles
{
    [RequireComponent(typeof(ObstacleCollision))]
    public class ObstacleDestroy : MonoBehaviour
    {
        private ObstacleCollision _collision;

        private void Awake()
        {
            _collision = GetComponent<ObstacleCollision>();
            _collision.onCollision += OnCollision;
        }

        private void OnCollision(Collision2D col) => col.gameObject.SetActive(false);
    }
}