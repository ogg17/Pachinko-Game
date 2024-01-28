using VgGames.Core.InjectModule;

namespace VgGames.Game.RuntimeData
{
    public class CoinsData : IInjectable
    {
        public readonly SaveReactiveData<int> Coins = new(0, nameof(Coins), i => i >= 0);
    }
}