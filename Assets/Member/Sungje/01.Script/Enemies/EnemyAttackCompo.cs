using Code.Entities;
using UnityEngine;

public class EnemyAttackCompo : MonoBehaviour, IEntityComponent
{
    [SerializeField] private LayerMask targetLayer;
    [SerializeField] private Animator animator;

    protected Enemy _enemy;
    private float _lastAttackTime;
    private bool _isAttack;

    public bool IsAttack => _isAttack;

    private readonly int IsAttackHash = Animator.StringToHash("IsAttack");

    protected EnemyDataSO Data => _enemy.EnemyDataSO;

    public void Initialize(Entity entity)
    {
        _enemy = entity as Enemy;
        if (animator == null)
            animator = GetComponentInChildren<Animator>();
    }

    private void Update()
    {
        TryDoAttack();
        UpdateMotion();
    }

    public void TryDoAttack()
    {
        if (_isAttack) return;
        if (Data == null || _enemy.IsSpoilMode) return;
        if (Time.time - _lastAttackTime < Data.attackCooldown) return;
        if (_enemy.DistanceToPlayer > Data.attackRange) return;

        _lastAttackTime = Time.time;
        _isAttack = true;

        ProcessAttack();
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

    public void EndAttack()
    {
        _isAttack = false;
    }

    private void UpdateMotion()
    {
        animator.SetBool(IsAttackHash, _isAttack);
    }

#if UNITY_EDITOR
    private void OnDrawGizmosSelected()
    {
        if (_enemy == null || _enemy.EnemyDataSO == null) return;

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(
            transform.position,
            _enemy.EnemyDataSO.attackRange
        );
    }
#endif
}
