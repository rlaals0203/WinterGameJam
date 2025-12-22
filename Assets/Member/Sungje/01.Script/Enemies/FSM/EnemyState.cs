using Code.Entities;
using UnityEngine;

public abstract class EnemyState
{
    protected Enemy _enemy;
    protected Player _player;
    protected EnemyDataSO data;
    public string stateName;

    public EnemyState(Enemy enemy)
    {
        _enemy = enemy;
        _player = enemy.player;
        this.data = enemy.enemyDataSO;
    }

    public void Enter()
    {
        EnterState();
    }

    protected virtual void EnterState()
    {
    }

    public void Exit()
    {
        ExitState();
    }

    protected virtual void ExitState()
    {
    }

    public virtual void UpdateState()
    {
    }

    public virtual void FixedUpdateState()
    {

    }


}
