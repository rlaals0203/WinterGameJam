using Code.Entities;
using UnityEngine;
using UnityEngine.InputSystem;

public class EnemyAttackCompo : MonoBehaviour, IEntityComponent
{
    [SerializeField] private LayerMask targetLayer;

    protected Enemy _enemy;

    private EnemyDataSO Data => _enemy.enemyDataSO;
    
    public virtual void Initialize(Entity entity)
    {
        _enemy = entity as Enemy;
    }

    public virtual void DoAttack()
    {
        if (Data == null) return;

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

#if UNITY_EDITOR
    private void OnDrawGizmosSelected()
    {
        if (_enemy == null || _enemy.enemyDataSO == null) return;

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _enemy.enemyDataSO.attackRange);
    }
#endif
}
