using UniRx;
using VgGames.Core.InjectModule;
using VgGames.Game.RuntimeData;

namespace VgGames.Game.UI.GameText
{
    public class CoinsText : BaseText
    {
        [Inject] private CoinsData _coinsData;
        protected override void BaseInit()
        {
            SetCoins();
            _coinsData.Coins.OnChange.Subscribe(SetCoins);
        }

        public override void GameDeactivate() => SetCoins();

        private void SetCoins() => SetValue(_coinsData.Coins.Value.ToString());
        private void SetCoins(int value) => SetValue(value.ToString());
    }
}