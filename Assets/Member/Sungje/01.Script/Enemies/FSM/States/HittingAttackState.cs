using UnityEngine;

public class HittingAttackState : EnemyState
{
    private float _timer;
    private EnemyAttackCompo _attackCompo;

    public HittingAttackState(Enemy enemy) : base(enemy)
    {
        _attackCompo = enemy.GetCompo<EnemyAttackCompo>();
        Debug.Log(_attackCompo);
    }

    public override void Enter()
    {
        _attackCompo.DoAttack();
        _timer = 0f;
    }

    public override void UpdateState()
    {
        if (_player == null)
        {
            _enemy.TransitionState(EnemyStateType.Move);
            return;
        }

        if (DistanceToPlayer > data.attackRange)
        {
            _enemy.TransitionState(EnemyStateType.Move);
            return;
        }

        _timer += Time.deltaTime;
        if (_timer >= data.attackCooldown)
        {
            _timer = 0f;
        }
    }

    public override void Exit()
    {
    }
}
