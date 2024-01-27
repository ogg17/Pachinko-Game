using UniRx;
using VgGames.Core.InjectModule;
using VgGames.Game.RuntimeData;

namespace VgGames.Game.UI.GameText
{
    public class BallsText : BaseText
    {
        [Inject] private BallsData _ballsData;
        protected override void BaseInit()
        {
            SetBalls();
            _ballsData.Balls.OnChange.Subscribe(SetBalls);
        }

        public override void GameDeactivate() => SetBalls();

        private void SetBalls() => SetValue(_ballsData.Balls.Value.ToString());
        private void SetBalls(int value) => SetValue(value.ToString());
    }
}