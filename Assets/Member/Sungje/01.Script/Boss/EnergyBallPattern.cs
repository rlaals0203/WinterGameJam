using KimMin.Core;
using KimMin.Events;
using KimMin.ObjectPool.RunTime;
using UnityEngine;

public class EnergyBallPattern : Pattern
{
    [SerializeField] private PoolItemSO energyBall;
    [SerializeField] private float speed;
    [SerializeField] private int damage;
    [SerializeField] private int count;
    [SerializeField] private float spreadAngle;

    public override void Execute(Boss boss)
    {
        Vector3 baseDir = (boss.Player.Position - boss.transform.position).normalized;

        for (int i = 0; i < count; i++)
        {
            float angle = spreadAngle * (i - (count - 1) * 0.5f);
            Vector3 dir = Quaternion.Euler(0, 0, angle) * baseDir;

            GameEventBus.RaiseEvent(
                SpawnEvents.SpawnEnergyBallEvent.Init(
                    energyBall,
                    boss.transform.position,
                    dir,
                    speed,
                    damage,
                    boss
                )
            );
        }
    }
}
