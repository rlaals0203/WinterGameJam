using Code.Entities;
using UnityEngine;

public class EnemyBatHit : MonoBehaviour
{
    private Enemy _enemy;

    private void Awake()
    {
        _enemy = GetComponentInParent<Enemy>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Player player = other.GetComponentInParent<Player>();
        if (player == null) return;

        player.TakeDamage(_enemy.enemyDataSO.damage);
    }
}
