using System;
using UniRx;
using VgGames.Core.Config;
using VgGames.Core.InjectModule;
using VgGames.Game.RuntimeData;

namespace VgGames.Game.UI.GameText
{
    public class TimerText : BaseText
    {
        [Inject] private TimerData _timer;

        protected override void BaseInit()
        {
            SetTime();
            _timer.Time.OnChange.Subscribe(SetTime);
        }

        public override void GameDeactivate() => SetTime();
        private void SetTime() => SetTime(_timer.Time.Value);
        private void SetTime(int value)
        {
            SetValue((GameConfig.TimerInterval - TimeSpan.FromSeconds(value)).ToString(@"hh\:mm\:ss"));
        }
    }
}