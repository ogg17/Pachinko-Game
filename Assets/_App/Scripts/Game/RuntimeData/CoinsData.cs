using VgGames.Core.InjectModule;

namespace VgGames.Game.RuntimeData
{
    public class CoinsData : IInjectable
    {
        public readonly SaveReactiveInt Coins = new(0, "coins", i => i >= 0);
    }
}