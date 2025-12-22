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
    [SerializeField] private GridManager gridManager;

    public EnemyDataSO enemyDataSO;
    public EnemyAttackCompo AttackCompo { get; private set; }

    private Dictionary<EnemyStateType, EnemyState> _stateDict;
    private EnemyState _currentState;

    public Player Player
    {
        get
        {
            if (player == null)
                player = FindAnyObjectByType<Player>();
            return player;
        }
    }

    public GridManager GridManager => gridManager;

    protected virtual void Awake()
    {
        AttackCompo = GetComponent<EnemyAttackCompo>();
    }

    protected virtual void Start()
    {
        _stateDict = new Dictionary<EnemyStateType, EnemyState>
        {
            { EnemyStateType.Move, new HittingMoveState(this) },
            { EnemyStateType.Attack, new HittingAttackState(this) },
            { EnemyStateType.Dead, new HittingDeadState(this) }
        };

        TransitionState(EnemyStateType.Move);
    }

    private void Update()
    {
        _currentState?.UpdateState();
    }

    public void TransitionState(EnemyStateType next)
    {
        if (_currentState == _stateDict[next]) return;

        _currentState?.Exit();
        _currentState = _stateDict[next];
        _currentState.Enter();
    }
}
