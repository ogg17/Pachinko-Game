using VgGames.Core.Config;

namespace VgGames.Core.CustomDebug
{
    public static class Debug
    {
        public static void Log(string value, string prefix = "--- Custom Debug: ")
        {
            if (GameConfig.IsDebug) UnityEngine.Debug.Log(prefix + value);
        }

        public static void LogError(string value, string prefix = "--- Custom Debug: ")
        {
            if (GameConfig.IsDebug) UnityEngine.Debug.LogError(prefix + "Error: " + value);
        }
    }
}