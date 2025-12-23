using Code.Core;
using Code.Entities;
using UnityEngine;

public abstract class EnemyState
{
    protected Enemy _enemy;
    protected Player _player => _enemy.Player;
    protected EnemyDataSO data;
    protected GridManager GridManager => _enemy.GridManager;

    protected float DistanceToPlayer =>
        Vector2.Distance(_enemy.transform.position, _player.transform.position);

    protected EnemyState(Enemy enemy)
    {
        _enemy = enemy;
        data = enemy.EnemyDataSO;
    }

    public virtual void Enter() { }
    public virtual void Exit() { }
    public virtual void UpdateState() { }
}
