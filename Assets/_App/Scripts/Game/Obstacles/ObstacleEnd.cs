using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;
using VgGames.Core.EventBusModule;
using VgGames.Core.InjectModule;
using VgGames.Game.Signals;
using VgGames.Game.Sound;

namespace VgGames.Game.Obstacles
{
    public class ObstacleEnd : MonoBehaviour
    {
        [Inject] private EventBus _eventBus;
        
        [SerializeField] private float scaleAnim;
        [SerializeField] private float duration;
        [SerializeField] private Ease ease;
        
        private bool _isCol;
        private Vector3 _startScale;

        private BallEndCollisionSignal _signal;
        private readonly SoundSignal _sound = new() { Type = SoundType.Coin };

        private void Awake()
        {
            _startScale = transform.localScale;
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            other.gameObject.SetActive(false);
            _eventBus.Invoke(_signal);
                        
            if(_isCol) return;
            _isCol = true;
            _eventBus.Invoke(_sound);
            ColAnim().Forget();
        }

        private async UniTaskVoid ColAnim()
        {
            await transform.DOScale(_startScale * scaleAnim, duration).SetEase(ease);
            await transform.DOScale(_startScale, duration).SetEase(ease);
            _isCol = false;
        }
    }
}