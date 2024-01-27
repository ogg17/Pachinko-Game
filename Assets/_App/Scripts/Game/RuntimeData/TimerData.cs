using System;
using VgGames.Core.InjectModule;

namespace VgGames.Game.RuntimeData
{
    public class TimerData : IInjectable
    {
        public SaveReactiveInt Time = new(0, nameof(Time));
        public SaveReactiveData<long> LastTime = new(DateTime.Now.ToBinary(), nameof(LastTime));
    }
}