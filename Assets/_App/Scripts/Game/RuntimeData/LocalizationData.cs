using System.Collections.Generic;
using System.IO;
using UniRx;
using UnityEngine;
using VgGames.Core.InjectModule;
using VgGames.Game.MiniJson;

namespace VgGames.Game.RuntimeData
{
    public class LocalizationData: IInjectable
    {
        [Inject] private SettingsData _settingsData;

        private Dictionary<string, object> _local;

        private readonly Dictionary<LanguageType, string> _localName = new()
        {
            { LanguageType.EnglishGb, "en" },
            //{ LanguageType.Dutch, "de" },
            //{ LanguageType.Spanish, "spa" },
            { LanguageType.Russian, "ru" },
            { LanguageType.Arabic, "ar" },
            { LanguageType.Bengali, "bn" },
            { LanguageType.Turkish, "tr" },
            { LanguageType.Italian, "it" },
            { LanguageType.French, "fr" },
            { LanguageType.Hindi, "hi" },
            { LanguageType.Kazakh, "kk" },
            { LanguageType.Portuguese , "pt" }
        };

        private bool _isLoaded;

        private Dictionary<string, object> Local
        {
            get
            {
                if (_isLoaded) return _local;

                _isLoaded = true;
                LoadLocal(_settingsData.Language);
                _settingsData.Language.OnChange.Subscribe(LoadLocal);
                return _local;
            }
        }

        private void LoadLocal(LanguageType l)
        {
            _local = Json.Deserialize(Resources.Load<TextAsset>(_localName[l]).text + ".json") as Dictionary<string, object>;
        }

        public string GetLocale(string name)
        {
            if (Local.TryGetValue(name, out var res))
                return res as string;
            throw new InvalidDataException("Name is not exist in Locale!");
        }

        public bool TryGetLocale(string name, out string value)
        {
            if (Local.TryGetValue(name, out var res))
            {
                value = res as string;
                return true;
            }
            value = default;
            return false;
        }
    }
}