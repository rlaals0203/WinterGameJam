using Code.Entities;
using UnityEngine;

public class EnemyAttackCompo : MonoBehaviour
{
    [SerializeField] private LayerMask targetLayer;

    private Enemy _enemy;
    private bool _isAttacking;

    private EnemyDataSO Data => _enemy.enemyDataSO;

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
        if (Data == null) return;

        Collider2D hit = Physics2D.OverlapCircle(
            transform.position,
            Data.attackRange,
            targetLayer
        );

        if (hit == null) return;

        Player target = hit.GetComponent<Player>();
        if (target == null) return;

        // target.TakeDamage(Data.attackDamage);
    }

    public void EndAttack()
    {
        _isAttacking = false;
    }

#if UNITY_EDITOR
    private void OnDrawGizmosSelected()
    {
        if (_enemy == null || _enemy.enemyDataSO == null) return;

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _enemy.enemyDataSO.attackRange);
    }
#endif
}
