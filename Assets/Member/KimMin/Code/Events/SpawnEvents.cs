using Code.Entities;
using KimMin.Core;
using KimMin.ObjectPool.RunTime;
using UnityEngine;

namespace KimMin.Events
{
    public class SpawnEvents
    {
        public static SpawnBulletEvent SpawnBulletEvent = new();
        public static SpawnEnergyBallEvent SpawnEnergyBallEvent = new();
        public static SpawnHomingBulletEvent SpawnHomingBullet = new();
        public static SpawnWarningAreaEvent SpawnWarningArea = new();
    }

    public class SpawnBulletEvent : GameEvent
    {
        public PoolItemSO poolItem;
        public Vector3 position;
        public Vector3 direction;
        public float speed;
        public int damage;
        public Entity entity;

        public SpawnBulletEvent Init(PoolItemSO poolItem, Vector3 position, 
            Vector3 rotation, float speed, int damage, Entity owner)
        {
            this.poolItem = poolItem;
            this.position = position;
            this.direction = rotation;
            this.speed = speed;
            this.damage = damage;
            this.entity = owner;
            return this;
        }
    }

    public class SpawnEnergyBallEvent : GameEvent
    {
        public PoolItemSO poolItem;
        public Vector3 position;
        public Vector3 direction;
        public float speed;
        public int damage;
        public Entity entity;
        public SpawnEnergyBallEvent Init(PoolItemSO poolItem, Vector3 position, 
            Vector3 rotation, float speed, int damage, Entity owner)
        {
            this.poolItem = poolItem;
            this.position = position;
            this.direction = rotation;
            this.speed = speed;
            this.damage = damage;
            this.entity = owner;
            return this;
        }
    }

    public class SpawnHomingBulletEvent : GameEvent
    {
        public PoolItemSO poolItem;
        public Vector3 position;
        public float speed;
        public int damage;
        public Entity owner;
        public Player target;

        public SpawnHomingBulletEvent Init(
            PoolItemSO poolItem,
            Vector3 position,
            float speed,
            int damage,
            Player target,
            Entity owner
        )
        {
            this.poolItem = poolItem;
            this.position = position;
            this.speed = speed;
            this.damage = damage;
            this.target = target;
            this.owner = owner;
            return this;
        }
    }

    public class SpawnWarningAreaEvent : GameEvent
    {
        public PoolItemSO poolItem;
        public Vector3 position;
        public int damage;
        public Entity owner;

        public SpawnWarningAreaEvent Init(
            PoolItemSO poolItem,
            Vector3 position,
            int damage,
            Entity owner
        )
        {
            this.poolItem = poolItem;
            this.position = position;
            this.damage = damage;
            this.owner = owner;
            return this;
        }
    }
}