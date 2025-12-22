using UnityEngine;

public class HittingDeadState : EnemyState
{
    public HittingDeadState(Enemy enemy) : base(enemy) { }

    public override void Enter()
    {
        Collider2D col = _enemy.GetComponent<Collider2D>();
        if (col != null) col.enabled = false;

        Rigidbody2D rb = _enemy.GetComponent<Rigidbody2D>();
        if (rb != null) rb.linearVelocity = Vector2.zero;

        Object.Destroy(_enemy.gameObject, 2f);
    }
}
