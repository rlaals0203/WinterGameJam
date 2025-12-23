using KimMin.Core;
using KimMin.ObjectPool.RunTime;
using UnityEngine;

namespace KimMin.Events
{
    public class SpawnEvents
    {
        public static SpawnBulletEvent SpawnBulletEvent = new();
    }
    
    public class SpawnBulletEvent : GameEvent
    {
        public PoolItemSO poolItem;
        public Vector3 position;
        public Vector3 direction;
        public float speed;
        public float damage;
        public Entity entity;

        public SpawnBulletEvent Init(PoolItemSO poolItem, Vector2 position, 
            Vector2 direction, float speed, float damage, Entity owner)
        {
            this.poolItem = poolItem;
            this.position = position;
            this.direction = direction;
            this.speed = speed;
            this.damage = damage;
            this.entity = owner;
            return this;
        }
    }
}