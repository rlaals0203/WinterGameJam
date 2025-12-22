using UnityEngine;

public class HittingMoveState : EnemyState
{
    private float _interval = 2f;
    private float _prevTime;

    public HittingMoveState(Enemy enemy) : base(enemy) { }

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
