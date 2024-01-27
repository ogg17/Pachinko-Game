using VgGames.Core.InjectModule;

namespace VgGames.Game.RuntimeData
{
    public class BallsData : IInjectable
    {
        public SaveReactiveData<int> Balls = new(200, nameof(Balls), i => i >= 0);
    }
}