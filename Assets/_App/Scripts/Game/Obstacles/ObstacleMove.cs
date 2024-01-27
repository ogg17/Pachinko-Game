using DG.Tweening;
using UnityEngine;
using VgGames.Core.Extra.GameState;

namespace VgGames.Game.Obstacles
{
    public class ObstacleMove : MonoBehaviour, IGameActivate
    {
        [SerializeField] private Ease ease;
        [SerializeField] private Vector2 end;
        [SerializeField] private float duration;

        private Vector2 _start;
        private bool _moveToward;

        private void Awake()
        {
            _start = transform.position;
        }

        public void GameActivate()
        {
            InvokeRepeating(nameof(Repeat), 0, duration);
        }

        private void Repeat()
        {
            transform.DOMove(_moveToward ? end : _start, duration).SetEase(ease);
            _moveToward = !_moveToward;
        }
    }
}