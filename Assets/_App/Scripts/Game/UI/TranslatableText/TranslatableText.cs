using TMPro;
using UniRx;
using UnityEngine;
using UnityEngine.UI;
using VgGames.Core.Extra;
using VgGames.Core.InjectModule;
using VgGames.Game.RuntimeData;

namespace VgGames.Game.UI.TranslatableText
{
    [DisallowMultipleComponent]
    public class TranslatableText : MonoBehaviour, IInit
    {
        [Inject] private SettingsData _settings;
        [Inject] private LocalizationData _localization;

        [SerializeField] private string strName = Default;
        [SerializeField] private bool isEnable = true;

        private const string Default = "default";

        private Text _legacyText;
        private TMP_Text _tmpText;
        private bool _isTMP = false;
        
        public void Init()
        {
            if(!isEnable) return;

            if (TryGetComponent<Text>(out var text))
                _legacyText = text;
            else
            {
                _tmpText = GetComponent<TMP_Text>();
                _isTMP = true;
            }
            SetText();
            
            _settings.Language.OnChange.Subscribe(_ => SetText());
        }

        private void SetText()
        {
            if(_isTMP)
                _tmpText.text = _localization.TryGetLocale(strName, out var res) 
                    ? res : _localization.GetLocale(Default);
            else
                _legacyText.text = _localization.TryGetLocale(strName, out var res) 
                ? res : _localization.GetLocale(Default);
        }
    }
}