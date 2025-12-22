using System;
using UnityEngine;

public class EnemyMoveState : MonoBehaviour, IEntityComponent
{
    private float _interval = 2f;
    private float _prevTime;

    private Enemy _enemy;
    private EnemyAttackCompo _attackCompo;

    public void Initialize(Entity entity)
    {
        _enemy = entity as Enemy;
        _attackCompo = entity.GetCompo<EnemyAttackCompo>();
        _interval = _enemy.EnemyDataSO.moveSpeed;
    }

    private void Update()
    {
        if (Time.time - _prevTime >= _interval)
        {
            _prevTime = Time.time;
            _enemy.GridManager.MoveToPlayer(_enemy.transform);
        }

        if (_enemy.DistanceToPlayer <= _enemy.EnemyDataSO.attackRange)
        {
            _attackCompo.TryDoAttack();
        }
    }
}
