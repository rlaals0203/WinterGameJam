using Code.Core;
using Code.Entities;
using System.Collections.Generic;
using AYellowpaper.SerializedCollections;
using UnityEngine;

public enum EnemyStateType
{
    Move,
    Attack,
    Dead
}

public abstract class Enemy : Entity
{
    [SerializeField] private Player player;
    [SerializeField] private GridManager gridManager;

    public EnemyDataSO enemyDataSO;

    [SerializedDictionary] private Dictionary<EnemyStateType, EnemyState> _stateDict;
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

    protected virtual void Start()
    {
        _stateDict = new Dictionary<EnemyStateType, EnemyState>
        {
            { EnemyStateType.Move, new EnemyMoveState(this) },
            { EnemyStateType.Attack, new HittingAttackState(this) },
            { EnemyStateType.Dead, new EnemyDeadState(this) }
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
