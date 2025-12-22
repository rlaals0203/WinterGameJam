using UnityEngine;

public class EnemyMoveState : EnemyState
{
    private float _interval = 2f;
    private float _prevTime;

    public EnemyMoveState(Enemy enemy) : base(enemy)
    {
        _interval = enemy.enemyDataSO.moveSpeed;
    }

    public override void Enter()
    {
        _prevTime = Time.time;
    }

    public override void UpdateState()
    {
        if (_player == null) return;

        if (Time.time - _prevTime >= _interval)
        {
            _prevTime = Time.time;
            _enemy.GridManager.MoveToPlayer(_enemy.transform);
        }

        if (DistanceToPlayer <= data.attackRange)
        {
            _enemy.TransitionState(EnemyStateType.Attack);
        }
    }
}
