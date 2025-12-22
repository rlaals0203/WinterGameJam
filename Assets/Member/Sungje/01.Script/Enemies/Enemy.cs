using Code.Core;
using Code.Entities;
using System.Collections.Generic;
using UnityEngine;

public enum EnemyStateType
{
    Move,
    Attack,
    Dead
}

public abstract class Enemy : MonoBehaviour
{
    [SerializeField] private Player player;

    public Player Player
    {
        get
        {
            if (player == null)
                player = FindAnyObjectByType<Player>();
            return player;
        }
    }

    [SerializeField] private GridManager gridManager;
    public GridManager GridManager => gridManager;

    public EnemyDataSO enemyDataSO;

    public EnemyAttackCompo AttackCompo { get; private set; }

    private Dictionary<EnemyStateType, EnemyState> _stateDict;
    private EnemyState _currentState;

    protected virtual void Awake()
    {
        AttackCompo = GetComponent<EnemyAttackCompo>();

        if (AttackCompo == null)
            Debug.LogError("EnemyAttackCompo ¾øÀ½", this);
    }

    protected virtual void Start()
    {
        InitFSM();
        TransitionState(EnemyStateType.Move);
    }

    protected virtual void InitFSM()
    {
        _stateDict = new Dictionary<EnemyStateType, EnemyState>
        {
            { EnemyStateType.Move, new HittingMoveState(this) },
            { EnemyStateType.Attack, new HittingAttackState(this) },
            { EnemyStateType.Dead, new HittingDeadState(this) }
        };
    }

    private void Update()
    {
        _currentState?.UpdateState();
    }

    public void TransitionState(EnemyStateType nextState)
    {
        if (_currentState == _stateDict[nextState]) return;

        _currentState?.Exit();
        _currentState = _stateDict[nextState];
        _currentState.Enter();
    }
}
