using Code.Entities;
using UnityEngine;

public class HittingAttackState : EnemyState
{
    private float _lastAttackTime;

    public HittingAttackState(Enemy enemy) : base(enemy)
    {
    }

    protected override void EnterState()
    {
        base.EnterState();
    }

    public override void UpdateState()
    {
        base.UpdateState();

        if (_player == null) return;

        float distance = Vector3.Distance(_enemy.transform.position, _player.transform.position);

        if (distance > data.attackRange)
        {
            _enemy.TransitionState(EnemyStateType.Move);
        }
    }

    protected override void ExitState()
    {
        base.ExitState();
    }
}