using System.Collections.Generic;
using UnityEngine;
public enum EnemyStateType
{
    Idle,
    Move,
    Attack,
    Die,
}

public abstract class Enemy : Entity
{
    [field:SerializeField] protected EnemyDataSO enemyDataSO { get; private set; }
    [SerializeField] private int _currentHealth;

    protected Dictionary<EnemyStateType, EnemyState> StateEnum = new();

    public EnemyStateType currentState;
    protected EnemyStateType previousState { get; set; }
    public EnemyStateType nextState { get; set; }

    protected bool IsAttack = false;

    public EnemyAttackCompo attackCompo { get; set; }

    protected override void Awake()
    {
        if(enemyDataSO == null)
            _currentHealth = enemyDataSO.maxHealth;
    }

    protected virtual void Update()
    {
        if (enemyDataSO == null) return;
    }
}
