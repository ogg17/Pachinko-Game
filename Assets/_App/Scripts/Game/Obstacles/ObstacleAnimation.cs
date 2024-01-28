using DG.Tweening;
using UnityEngine;
using VgGames.Core.EventBusModule;
using VgGames.Core.InjectModule;

namespace VgGames.Game.Obstacles
{
    [RequireComponent(typeof(ObstacleCollision))]
    public class ObstacleAnimation : MonoBehaviour
    {
        [Inject] private EventBus _eventBus;

        [SerializeField] private float scaleAnim;
        [SerializeField] private float duration;
        [SerializeField] private Ease ease;
        
        private Vector3 _startScale;
        private ObstacleCollision _collision;

        private void Awake()
        {
            _startScale = transform.localScale;
            _collision = GetComponent<ObstacleCollision>();
            _collision.onCollision += OnCollision;
        }

        private void OnCollision(Collision2D col)
        {
            DOTween.Sequence().Append(transform.DOScale(_startScale * scaleAnim, duration).SetEase(ease))
                .Append(transform.DOScale(_startScale, duration).SetEase(ease));
        }
    }
}