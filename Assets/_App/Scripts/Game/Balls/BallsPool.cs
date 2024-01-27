using UnityEngine;
using VgGames.Core.ObjectPoolModule;

namespace VgGames.Game.Balls
{
    public class BallsPool : ObjectPool
    {
        public BallsPool(string prefabName, int capacity, bool isExtendable = true, Transform parent = null) : base(prefabName, capacity, isExtendable, parent)
        {
        }

        public BallsPool(GameObject prefab, int capacity, bool isExtendable = true, Transform parent = null) : base(prefab, capacity, isExtendable, parent)
        {
        }
    }
}