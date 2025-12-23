using KimMin.Core;
using KimMin.Events;
using KimMin.ObjectPool.RunTime;
using UnityEngine;

public class WarningAreaPattern : Pattern
{
    [SerializeField] private PoolItemSO warningArea;
    [SerializeField] private int damage;

    private readonly SpawnWarningAreaEvent _spawnEvent = SpawnEvents.SpawnWarningArea;

    public override void Execute(Boss boss)
    {
        GameEventBus.RaiseEvent(
            _spawnEvent.Init(
                warningArea,
                boss.Player.Position,
                damage,
                boss
            )
        );
    }
}
