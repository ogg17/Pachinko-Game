using System;
using UnityEngine;
using VgGames.Core.InjectModule;

namespace VgGames.Game.RuntimeData
{
    public class SettingsData: IInjectable
    {
        public SaveReactiveData<bool> Sound = new(true, nameof(Sound));
        public SaveReactiveData<LanguageType> Language = new(LanguageType.EnglishGb, nameof(Language));

        public void NextLanguage()
        {
            if (Language < LanguageType.Kazakh) Language.Value++;
            else Language.Value = LanguageType.EnglishGb;
        }

        public SettingsData()
        {
            if(!Language.HasSave)
                SetDefaultLocale();
        }

        private void SetDefaultLocale()
        {
            Language.Value = Application.systemLanguage switch {
                SystemLanguage.English => LanguageType.EnglishGb,
                SystemLanguage.Russian => LanguageType.Russian,
                SystemLanguage.Arabic => LanguageType.Arabic,
                //SystemLanguage.Spanish => LanguageType.Spanish,
                //SystemLanguage.Dutch => LanguageType.Dutch,
                SystemLanguage.Turkish => LanguageType.Turkish,
                SystemLanguage.Italian => LanguageType.Italian,
                SystemLanguage.French => LanguageType.French,
                _ => LanguageType.EnglishGb
            };

            if (Language != LanguageType.EnglishGb) return;
            
            var cult = System.Globalization.CultureInfo.CurrentCulture.NativeName;
            Language.Value = cult switch
            {
                "Kazakh" =>  LanguageType.Kazakh,
                "Bengali" => LanguageType.Bengali,
                "Hindi" => LanguageType.Hindi,
                _ => LanguageType.EnglishGb
            };
        }
    }

    [Serializable]
    public enum LanguageType
    {
        EnglishGb,
        //Dutch,
        //Spanish,
        Russian,
        Arabic,
        Bengali,
        Turkish,
        Italian,
        French,
        Hindi,
        Kazakh,
        Portuguese
    }
}