using KimMin.Core;
using KimMin.Events;
using KimMin.ObjectPool.RunTime;
using UnityEngine;

public class BulletPattern : Pattern
{
    [SerializeField] private PoolItemSO bullet;
    [SerializeField] private float speed;
    [SerializeField] private int damage;
    [SerializeField] private int count;
    [SerializeField] private float spreadAngle;

    public override void Execute(Boss boss)
    {
        Vector3 dir = (boss.Player.Position - boss.transform.position).normalized;

        for (int i = 0; i < count; i++)
        {
            float angle = spreadAngle * (i - (count - 1) * 0.5f);
            Vector3 rotatedDir = Quaternion.Euler(0, 0, angle) * dir;

            GameEventBus.RaiseEvent(
                SpawnEvents.SpawnBulletEvent.Init(
                    bullet,
                    boss.transform.position,
                    rotatedDir,
                    speed,
                    damage,
                    boss
                )
            );
        }
    }
}
