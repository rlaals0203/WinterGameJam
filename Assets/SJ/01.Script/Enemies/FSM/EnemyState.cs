using UnityEngine;

public abstract class EnemyState
{
    protected Enemy _enemy;
    public string stateName;

    public EnemyState(Enemy enemy)
    {
        _enemy = enemy;
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
        ExtiState();
    }

    protected virtual void ExtiState()
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
