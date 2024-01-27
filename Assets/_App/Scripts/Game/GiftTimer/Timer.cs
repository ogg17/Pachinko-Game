using System;
using UniRx;
using UnityEngine;
using VgGames.Core.Config;
using VgGames.Core.InjectModule;
using VgGames.Game.RuntimeData;

namespace VgGames.Game.GiftTimer
{
    public class Timer
    {
        [Inject] private TimerData _timer;
        [Inject] private BallsData _balls;

        private IDisposable _time;

        public Timer()
        {
            InjectSystem.Inject(this);
        }
        
        public void GameActivate()
        {
            _time = Observable.Interval(TimeSpan.FromSeconds(1)).Subscribe(Time);
        }

        private void Time(long t)
        {
            _timer.Time.Value += 1;
            var time = DateTime.Now - DateTime.FromBinary(_timer.LastTime);
            _timer.Time.Value = Mathf.RoundToInt((float)time.TotalSeconds);
            if (time <= GameConfig.TimerInterval) return;
            
            _balls.Balls.Value = 200;
            _timer.Time.Value = 0;
            _timer.LastTime.Value = DateTime.Now.ToBinary();
        }
        
        public void GameDeactivate()
        {
            _timer.LastTime.Value = _timer.LastTime.Value;
            _time?.Dispose();
        }
    }
}