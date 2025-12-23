using Code.Entities;
using UnityEngine;

public class EnemyAttackCompo : MonoBehaviour, IEntityComponent
{
    [SerializeField] private LayerMask targetLayer;
    [SerializeField] private Animator animator;
    [SerializeField] private float attackDuration = 0.4f;

    protected Enemy _enemy;
    private float _lastAttackTime;
    private float _attackEndTime;
    private bool _isAttack;
    private bool _isMove;

    private readonly int IsAttackHash = Animator.StringToHash("IsAttack");
    private readonly int IsMoveHash = Animator.StringToHash("IsMove");

    protected EnemyDataSO Data => _enemy?.EnemyDataSO;

    private Animator Animator
    {
        get
        {
            if (animator == null)
                animator = GetComponentInChildren<Animator>(true);
            return animator;
        }
    }

    public void Initialize(Entity entity)
    {
        _enemy = entity as Enemy;
        _isMove = true;
    }

    private void Update()
    {
        TryDoAttack();
        UpdateAttackTimer();
        UpdateMotion();
    }

    public void TryDoAttack()
    {
        if (_isAttack) return;
        if (_enemy == null || Data == null) return;
        if (_enemy.IsSpoilMode) return;
        if (Time.time - _lastAttackTime < Data.attackCooldown) return;
        if (_enemy.DistanceToPlayer > Data.attackRange) return;

        _lastAttackTime = Time.time;
        _attackEndTime = Time.time + attackDuration;

        _isAttack = true;
        _isMove = false;

        ProcessAttack();
    }

    private void UpdateAttackTimer()
    {
        if (!_isAttack) return;

        if (Time.time >= _attackEndTime)
        {
            _isAttack = false;
            _isMove = true;
        }
    }

    protected virtual void ProcessAttack()
    {
        Collider2D hit = Physics2D.OverlapCircle(
            transform.position,
            Data.attackRange,
            targetLayer
        );

        if (hit == null) return;

        Player target = hit.GetComponent<Player>();
        if (target == null) return;

        target.TakeDamage(Data.damage);
    }

    private void UpdateMotion()
    {
        if (Animator == null) return;

        Animator.SetBool(IsAttackHash, _isAttack);
        Animator.SetBool(IsMoveHash, _isMove);
    }
}
