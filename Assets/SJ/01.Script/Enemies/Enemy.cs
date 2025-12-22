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

    public EnemyAttackCompo AttackCompo { get; set; }

    protected override void Awake()
    {
        base.Awake();
        if (enemyDataSO == null)
            _currentHealth = enemyDataSO.maxHealth;
        AttackCompo = GetComponentInChildren<EnemyAttackCompo>();
    }

    public void Initialize()
    {
    }

    private void OnEnable()
    {
        TransitionState(EnemyStateType.Idle);
    }

    public void TransitionState(EnemyStateType newState)
    {
        StateEnum[currentState].Exit();
        previousState = currentState;
        currentState = newState;
        StateEnum[currentState].Enter();
    }

    protected virtual void Update()
    {
        StateEnum[currentState].UpdateState();
    }
    protected virtual void FixedUpdate()
    {
        StateEnum[currentState].FixedUpdateState();
    }

    public virtual void HandleDead()
    {
        IsDead = true;
        OnDeadEvent?.Invoke();
        TransitionState(EnemyStateType.Die);
        DestroyObject();
    }
}
