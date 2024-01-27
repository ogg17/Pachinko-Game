using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;
using VgGames.Core.EventBusModule;
using VgGames.Core.Extra;
using VgGames.Core.InjectModule;
using VgGames.Game.Signals;

namespace VgGames.Game.UI.Buttons
{
    public class PushButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IInit
    {
        [Inject] private EventBus _eventBus;

        [Header("Button")] [SerializeField] private Vector2 pushPosition = Vector2.one;
        [SerializeField] private Vector2 pushSize = Vector2.one;
        [SerializeField] private bool isChangeSize;
        [SerializeField] private float duration = 0.3f;
        [SerializeField] private RectTransform button;

        private Vector2 _defaultPos;
        private Vector2 _pushPos;

        private Vector2 _defaultSize;
        private Vector2 _pushSize;

        private readonly InputSignal _signal = new();

        public void Init()
        {
            _defaultPos = button.anchoredPosition;
            _pushPos = _defaultPos - pushPosition;

            _defaultSize = button.sizeDelta;
            _pushSize = _defaultSize + pushSize;
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            if (isChangeSize)
            {
                var seq = DOTween.Sequence();
                seq.Append(DOVirtual.Vector3(_defaultPos, _pushPos, duration / 3,
                        value => { button.anchoredPosition = value; }))
                    .Join(DOVirtual.Vector3(_defaultSize, _pushSize, duration / 3,
                        value => { button.sizeDelta = value; }));
            }

            _signal.IsPush = true;
            _eventBus.Invoke(_signal);
        }


        public void OnPointerUp(PointerEventData eventData)
        {
            if (isChangeSize)
            {
                var seq = DOTween.Sequence();
                seq.Append(DOVirtual.Vector3(_pushPos, _defaultPos, duration,
                        value => { button.anchoredPosition = value; }))
                    .Join(DOVirtual.Vector3(_pushSize, _defaultSize, duration,
                        value => { button.sizeDelta = value; }));
            }

            _signal.IsPush = false;
            _eventBus.Invoke(_signal);
        }
    }
}