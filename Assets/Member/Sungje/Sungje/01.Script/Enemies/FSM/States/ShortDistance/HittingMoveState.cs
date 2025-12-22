using Code.Core;
using KimMin.Dependencies;
using UnityEngine;

public class HittingMoveState : EnemyState
{
    public HittingMoveState(Enemy enemy) : base(enemy)
    {
    }

    protected override void EnterState()
    {
        base.EnterState();
    }

    public override void UpdateState()
    {
        if (_player == null) return;

        float distance = Vector3.Distance(_enemy.transform.position, _player.transform.position);

        if (distance <= data.attackRange)
        {
            _enemy.TransitionState(EnemyStateType.Attack);
        }
        else
        {
            MoveTowardsPlayer();
        }
    }

    private void MoveTowardsPlayer()
    {
        if (_enemy.MoveCompo != null)
        {
            _enemy.MoveCompo.ProcessMove();
        }
    }

    protected override void ExitState()
    {
        base.ExitState();
    }
}