using KimMin.Core;
using KimMin.ObjectPool.RunTime;
using UnityEngine;

namespace KimMin.Events
{
    public static class EffectEvents
    {
        public static PlayPoolEffect PlayPoolEffect = new PlayPoolEffect();
    }

    public class PlayPoolEffect : GameEvent
    {
        public Vector3 position;
        public Quaternion rotation;
        public PoolItemSO targetItem;
        public float duration;

        public PlayPoolEffect Initializer(Vector2 position, Quaternion rotation, PoolItemSO item, float duration)
        {
            this.position = position;
            this.rotation = rotation;
            this.targetItem = item;
            this.duration = duration;
            
            return this;
        }
    }
}