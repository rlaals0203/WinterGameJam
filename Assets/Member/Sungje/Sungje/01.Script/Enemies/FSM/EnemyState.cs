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
        Debug.Log("Enter State: " + stateName);
    }

    public void Exit()
    {
        ExitState();
    }

    protected virtual void ExitState()
    {
        Debug.Log("Exit State: " + stateName);
    }

    public virtual void UpdateState()
    {
    }

    public virtual void FixedUpdateState()
    {

    }


}
