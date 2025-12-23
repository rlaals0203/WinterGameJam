using System;
using System.Data.Common;
using Code.Entities;
using UnityEngine;
using UnityEngine.InputSystem;

public class EnemyAttackCompo : MonoBehaviour, IEntityComponent
{
    [SerializeField] private LayerMask targetLayer;

    protected Enemy _enemy;
    private float _lastAttackTime;

    private EnemyDataSO Data => _enemy.EnemyDataSO;
    
    public virtual void Initialize(Entity entity)
    {
        _enemy = entity as Enemy;
    }

    private void Update()
    {
        TryDoAttack();
    }

    public void TryDoAttack()
    {
        if (Data == null) return;
        
        if (Time.time - _lastAttackTime < Data.attackCooldown
            || _enemy.DistanceToPlayer > Data.attackRange) return;
        
        _lastAttackTime = Time.time;
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

#if UNITY_EDITOR
    private void OnDrawGizmosSelected()
    {
        if (_enemy == null || _enemy.EnemyDataSO == null) return;

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _enemy.EnemyDataSO.attackRange);
    }
#endif
}
