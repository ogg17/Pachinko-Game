
using TMPro;
using UnityEngine;
using VgGames.Core.Extra;
using VgGames.Core.Extra.GameState;

namespace VgGames.Game.UI.GameText
{
    [RequireComponent(typeof(TMP_Text))]
    public abstract class BaseText : MonoBehaviour, IInit, IGameActivate, IGameDeactivate
    {
        private TMP_Text _text;

        protected virtual void BaseInit() { }

        private void InitComponents() => _text = GetComponent<TMP_Text>();

        public void SetValue(string value) => _text.text = value;
        
        public virtual void GameUpdate(){ }

        public void Init()
        {
            InitComponents();
            BaseInit();
        }

        public virtual void GameActivate() { }
        public virtual void GameDeactivate() { }
    }
}