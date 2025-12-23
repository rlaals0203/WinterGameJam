using KimMin.Core;
using KimMin.Events;
using KimMin.ObjectPool.RunTime;
using UnityEngine;

public class WarningAreaAttackCompo : EnemyAttackCompo
{
    [SerializeField] private PoolItemSO warningArea;
    private readonly SpawnWarningAreaEvent _spawnEvent = SpawnEvents.SpawnWarningArea;

    protected override void ProcessAttack()
    {
        GameEventBus.RaiseEvent(
            _spawnEvent.Init(
                warningArea,
                _enemy.Player.Position,
                Data.damage,
                _enemy
            )
        );
    }
}
