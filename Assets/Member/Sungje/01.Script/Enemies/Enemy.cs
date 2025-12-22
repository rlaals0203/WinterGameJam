using Code.Core;
using Code.Entities;
using KimMin.Dependencies;
using System.Collections.Generic;
using UnityEngine;
public enum EnemyStateType
{
    Move,
    Attack,
    Dead
}
public class Enemy : MonoBehaviour
{
    public Player player;

    public Player Player
    {
        get
        {
            if (player == null)
                player = FindAnyObjectByType<Player>();
            return player;
        }
    }

    [Inject] public GridManager _gridManager;
    public GridManager GridManager => _gridManager;

    public EnemyDataSO enemyDataSO;

    public EnemyMoveCompo MoveCompo { get; private set; }
    public EnemyAttackCompo AttackCompo { get; private set; }

    private Dictionary<EnemyStateType, EnemyState> _stateDict;
    private EnemyState _currentState;

    private void Awake()
    {
        MoveCompo = GetComponent<EnemyMoveCompo>();
        AttackCompo = GetComponent<EnemyAttackCompo>();

        _stateDict = new Dictionary<EnemyStateType, EnemyState>
        {
            { EnemyStateType.Move, new HittingMoveState(this) },
            { EnemyStateType.Attack, new HittingAttackState(this) },
            { EnemyStateType.Dead, new HittingDeadState(this) }
        };
    }

    private void Start()
    {
        TransitionState(EnemyStateType.Move);
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

