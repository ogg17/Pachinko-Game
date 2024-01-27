using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;
using VgGames.Core.Extra;
using VgGames.Core.InjectModule;

namespace VgGames.Game.UI.Menus
{
    public class Menu : MonoBehaviour, IInit
    {
        [Inject] private MenusStateMachine _menus;

        [SerializeField] private MenuType type;
        [Header("Animation")] [SerializeField] private float duration = 0.3f;
        [SerializeField] private Vector3 disableScale = new Vector3(1, 0, 1);
        [SerializeField] private Vector2 disablePos = new Vector3(0, 0);
        [SerializeField] private Ease ease = Ease.InBack;

        private Vector3 _defaultScale = Vector3.one;
        private Vector2 _defaultPosition = Vector3.zero;
        private RectTransform _rect;

        public MenuType Type => type;

        public void Init()
        {
            _rect = GetComponent<RectTransform>();
            _defaultPosition = _rect.anchoredPosition;
            _defaultScale = _rect.localScale;

            _menus.Add(this);
        }

        public async UniTask Enable()
        {
            gameObject.SetActive(true);
            var trans = transform;
            
            var seq = DOTween.Sequence();
            await seq.Append(DOVirtual.Vector3(disablePos, _defaultPosition, duration,
                    value => { _rect.anchoredPosition = value; }))
                .Join(trans.DOScale(_defaultScale, duration)).SetEase(ease);
        }

        public async UniTask Disable()
        {
            var seq = DOTween.Sequence();
            await seq.Append(transform.DOScale(disableScale, duration))
                .Join(DOVirtual.Vector3(_defaultPosition, disablePos, duration,
                    value => { _rect.anchoredPosition = value; }))
                .AppendCallback(() => gameObject.SetActive(false)).SetEase(ease);
        }

        public void ForceDisable()
        {
            transform.localScale = disableScale;
            _rect.anchoredPosition = disablePos;
            gameObject.SetActive(false);
        }
    }
}