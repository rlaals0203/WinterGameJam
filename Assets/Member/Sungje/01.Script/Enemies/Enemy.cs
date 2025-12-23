using Code.Core;
using Code.Entities;
using UnityEngine;

public abstract class Enemy : Entity
{
    [SerializeField] private Player player;
    [SerializeField] private GridManager gridManager;
    
    public bool IsSpoilMode { get; set; }

    [field: SerializeField] public EnemyDataSO EnemyDataSO { get; private set; }
    public float RemainSlowTime { get; set; }
    
    public float DistanceToPlayer =>
        Vector2.Distance(transform.position, player.transform.position);
    
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
