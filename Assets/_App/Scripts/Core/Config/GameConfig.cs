using System;
using UnityEngine;

namespace VgGames.Core.Config
{
    public static class GameConfig
    {
        public static readonly bool IsDebug;
        public static readonly TimeSpan TimerInterval;
        public const string StorageMenuEntry = "VgGames/Storage/";
        public static string Hash = String.Empty;

        static GameConfig()
        {
            var load = Resources.Load<GameConfigStorage>(nameof(GameConfigStorage));
            Hash = Application.identifier.GetHashCode().ToString();
            IsDebug = load.IsDebug;
            TimerInterval = TimeSpan.FromSeconds(load.TimerInterval);
        }
    }
}