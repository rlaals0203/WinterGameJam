using UnityEngine;

public class HittingAttackState : EnemyState
{
    private float _timer;

    public HittingAttackState(Enemy enemy) : base(enemy) { }

    public override void Enter()
    {
        _timer = 0f;
        _enemy.AttackCompo.StartAttack();
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
            _enemy.AttackCompo.DoAttack();
        }
    }

    public override void Exit()
    {
        if (_enemy.AttackCompo != null)
            _enemy.AttackCompo.EndAttack();
    }

}
