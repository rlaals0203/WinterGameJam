using Code.Core;
using Code.Entities;
using KimMin.Dependencies;
using System;
using UnityEngine;

public abstract class Enemy : Entity
{
    [SerializeField] private Player player;
    [SerializeField] private GridManager gridManager;

    public bool IsSpoilMode { get; set; }

    [field: SerializeField] public EnemyDataSO EnemyDataSO { get; private set; }
    public float RemainSlowTime { get; set; }

    public float DistanceToPlayer =>
    Vector2.Distance(transform.position, Player.transform.position);

    protected override void Awake()
    {
        base.Awake();
        OnDeadEvent.AddListener(HandleDeadEvent);
    }

    private void HandleDeadEvent()
    {
        if (IsDead) return;
        IsDead = true;
        Destroy(gameObject, 0.5f);
    }

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
}
