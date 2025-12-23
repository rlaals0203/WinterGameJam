using KimMin.Core;
using KimMin.Events;
using KimMin.ObjectPool.RunTime;
using UnityEngine;

public class EnergyAttackCompo : EnemyAttackCompo
{
    [SerializeField] private PoolItemSO energyBall;
    private readonly SpawnEnergyBallEvent _energyBallEvent = SpawnEvents.SpawnEnergyBallEvent;

    protected async override void ProcessAttack()
    {
        Vector2 dir = _enemy.Player.Position - _enemy.transform.position;
        GameEventBus.RaiseEvent(_energyBallEvent.Init(energyBall, _enemy.transform.position,
            dir, 7f, 15f, _enemy));
        await Awaitable.WaitForSecondsAsync(1.5f);
    }
}
