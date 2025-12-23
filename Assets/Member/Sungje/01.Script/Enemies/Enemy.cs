using Code.Combat;
using Code.Core;
using Code.Entities;
using KimMin.Core;
using KimMin.Dependencies;
using KimMin.Events;
using UnityEngine;

public abstract class Enemy : Entity
{
    [Inject] private Player player;

    [SerializeField] private GridManager gridManager;
    [SerializeField] private Animator animator;
    [SerializeField] private float hitDuration = 0.15f;

    [field: SerializeField] public EnemyDataSO EnemyDataSO { get; private set; }

    public bool IsSpoilMode { get; set; }
    public float RemainSlowTime { get; set; }

    private bool _isHit;
    private float _hitEndTime;

    private readonly int IsHitHash = Animator.StringToHash("IsHit");

    public float DistanceToPlayer =>
        Vector2.Distance(transform.position, Player.transform.position);

    protected override void Awake()
    {
        base.Awake();

        if (animator == null)
            animator = GetComponentInChildren<Animator>();

        OnDeadEvent.AddListener(HandleDead);
        OnHitEvent.AddListener(HandleHitEvent);
    }

    private void HandleDead()
    {
        GameEventBus.RaiseEvent(EnemyEvents.EnemyDeadEvent);
        Destroy(gameObject);
    }

    protected virtual void Update()
    {
        UpdateHitState();
    }

    private void OnDestroy()
    {
        OnHitEvent.RemoveListener(HandleHitEvent);
        OnDeadEvent.RemoveListener(HandleDead);
    }

    private void HandleHitEvent()
    {
        if (IsDead) return;

        _isHit = true;
        _hitEndTime = Time.time + hitDuration;

        animator.SetBool(IsHitHash, true);
    }

    private void UpdateHitState()
    {
        if (!_isHit) return;

        if (Time.time >= _hitEndTime)
        {
            _isHit = false;
            animator.SetBool(IsHitHash, false);
        }
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

    public GridManager GridManager
    {
        get
        {
            if (gridManager == null)
                gridManager = GridManager.Instance;
            return gridManager;
        }
    }

}
