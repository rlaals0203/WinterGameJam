using UnityEngine;

/*public class HittingAttackState : EnemyState
{
    private float _timer;
    private EnemyAttackCompo _attackCompo;

    public HittingAttackState(Enemy enemy) : base(enemy)
    {
        _attackCompo = enemy.GetCompo<EnemyAttackCompo>();
    }

    public override void Enter()
    {
        _attackCompo.TryDoAttack();
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
}*/
