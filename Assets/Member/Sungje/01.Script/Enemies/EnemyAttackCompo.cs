using Code.Entities;
using UnityEngine;

public class EnemyAttackCompo : MonoBehaviour
{
    [field: SerializeField] public EnemyDataSO enemyDataSO { get; private set; }
    [SerializeField] private LayerMask targetLayer;

    private Enemy _enemy;
    private bool _isAttacking;

    private void Awake()
    {
        _enemy = GetComponent<Enemy>();
    }

    public void StartAttack()
    {
        _isAttacking = true;
    }

    public void DoAttack()
    {
        if (!_isAttacking) return;

        Collider2D hit = Physics2D.OverlapCircle(
            transform.position,
            enemyDataSO.attackRange,
            targetLayer
        );

        if (hit == null) return;

        Player target = hit.GetComponent<Player>();
        if (target == null) return;

        //target.TakeDamage(damage);
    }

    public void EndAttack()
    {
        _isAttacking = false;
    }

#if UNITY_EDITOR
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, enemyDataSO.attackRange);
    }
#endif
}
