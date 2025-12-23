using Code.Core;
using Code.Entities;
using KimMin.Dependencies;
using UnityEngine;

public abstract class Enemy : Entity
{
    [Inject] private Player player;
    [SerializeField] private GridManager gridManager;
    
    public bool IsSpoilMode { get; set; }

    [field: SerializeField] public EnemyDataSO EnemyDataSO { get; private set; }
    public float RemainSlowTime { get; set; }
    
    public float DistanceToPlayer =>
        Vector2.Distance(transform.position, Player.transform.position);
    
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
