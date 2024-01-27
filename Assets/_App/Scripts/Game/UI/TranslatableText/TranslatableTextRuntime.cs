using TMPro;
using UniRx;
using UnityEngine;
using VgGames.Game.RuntimeData;

namespace VgGames.Game.UI.TranslatableText
{
    public class TranslatableTextRuntime : MonoBehaviour
    {
        private SettingsData _settings;
        private LocalizationData _localization;

        [SerializeField] private string strName = Default;
        [SerializeField] private bool isEnable = true;

        private const string Default = "default";
        private TMP_Text _tmpText;

        public void Construct(SettingsData settingsData, LocalizationData localizationData)
        {
            _settings = settingsData;
            _localization = localizationData;
            
            Init();
        }

        private void Init()
        {
            if (!isEnable) return;
            
            _tmpText = GetComponent<TMP_Text>();
            SetText();
            _settings.Language.OnChange.Subscribe(_ => SetText());
        }

        private void SetText()
        {
            _tmpText.text = _localization.TryGetLocale(strName, out var res)
                ? res
                : _localization.GetLocale(Default);
        }
    }
}