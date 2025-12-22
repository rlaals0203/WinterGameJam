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


        Debug.Log("Attakkacasj");

        //if (_enemy.AnimTrigger != null)
        //{
        //    _enemy.AnimTrigger.OnAnimationEndEvent += AnimationEnd;
        //}
        //else
        //{
        //    _lastAttackTime = Time.time;
        //}
    }

    public override void UpdateState()
    {
        //if (_enemy.AnimTrigger == null)
        //{
        //    if (Time.time - _lastAttackTime > 1.0f)
        //    {
        //        AnimationEnd();
        //    }
        //}
    }

    private void AnimationEnd()
    {
        _enemy.TransitionState(EnemyStateType.Move);
    }

    protected override void ExitState()
    {
        //if (_enemy.AnimTrigger != null)
        //{
        //    _enemy.AnimTrigger.OnAnimationEndEvent -= AnimationEnd;
        //}

        _enemy.TransitionState(EnemyStateType.Move);
        base.ExitState();
    }
}