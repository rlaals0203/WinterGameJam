using KimMin.Core;
using KimMin.Events;
using KimMin.ObjectPool.RunTime;
using UnityEngine;

public class HomingBulletPattern : Pattern
{
    [SerializeField] private PoolItemSO homingBullet;
    [SerializeField] private float speed;
    [SerializeField] private int damage;
    [SerializeField] private int count;

    public override void Execute(Boss boss)
    {
        for (int i = 0; i < count; i++)
        {
            GameEventBus.RaiseEvent(
                SpawnEvents.SpawnHomingBullet.Init(
                    homingBullet,
                    boss.transform.position,
                    speed,
                    damage,
                    boss.Player,
                    boss
                )
            );
        }
    }
}
