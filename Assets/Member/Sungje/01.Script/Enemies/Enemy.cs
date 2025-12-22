using Code.Core;
using Code.Entities;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public enum EnemyStateType
{
    Move,
    Attack,
    MeleeAttack,
    Dead,
}

public abstract class Enemy : Entity, IDamageable
{
    [field: SerializeField] public EnemyDataSO enemyDataSO { get; private set; }
    [SerializeField] private int _currentHealth;

    protected Dictionary<EnemyStateType, EnemyState> StateEnum = new();

    public EnemyStateType currentState;
    protected EnemyStateType previousState { get; set; }
    public bool IsAttack { get; set; }
    public Player player;

    public EnemyAttackCompo AttackCompo { get; set; }
    public EnemyMoveCompo MoveCompo { get; private set; }

    protected override void Awake()
    {
        base.Awake();

        player = GameObject.FindAnyObjectByType<Player>();

        if (enemyDataSO != null)
            _currentHealth = enemyDataSO.maxHealth;

        AttackCompo = GetComponentInChildren<EnemyAttackCompo>();
        MoveCompo = GetComponentInChildren<EnemyMoveCompo>();
        //AnimTrigger = GetComponentInChildren<EnemyAnimationTrigger>();

        InitState();
    }

    protected abstract void InitState();

    private void OnEnable()
    {
        if (StateEnum.Count > 0)
            TransitionState(EnemyStateType.Move);
    }

    public void TransitionState(EnemyStateType newState)
    {
        if (StateEnum.ContainsKey(currentState))
            StateEnum[currentState].Exit();

        previousState = currentState;
        currentState = newState;

        if (StateEnum.ContainsKey(currentState))
            StateEnum[currentState].Enter();
        else
            Debug.LogError($"{currentState} 상태가 딕셔너리에 없습니다!");
    }

    protected virtual void Update()
    {
        if (StateEnum.ContainsKey(currentState))
            StateEnum[currentState].UpdateState();
        if(Keyboard.current.gKey.wasPressedThisFrame)
            ApplyDamage(Vector3.zero, Vector3.up);
    }

    protected virtual void FixedUpdate()
    {
        if (StateEnum.ContainsKey(currentState))
            StateEnum[currentState].FixedUpdateState();
    }

    protected virtual void HandleDead()
    {
        IsDead = true;
        TransitionState(EnemyStateType.Dead);
        Destroy(gameObject, 1f);
    }

    private void OnDrawGizmos()
    {
        if (enemyDataSO == null) return;
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, enemyDataSO.attackRange);
    }

    public void ApplyDamage(Vector3 hitPoint, Vector3 hitNormal)
    {
        _currentHealth -= 1;
        if (_currentHealth <= 0 && !IsDead)
        {
            HandleDead();
        }
    }
}