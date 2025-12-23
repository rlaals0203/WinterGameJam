using Code.Combat;
using KimMin.Core;
using UnityEngine;

public class EnergyBall : Bullet
{
    [SerializeField] private float speedMultiplier = 0.3f;

    private bool _fired;

    public override void Init(Vector3 position, Vector2 direction, float bulletSpeed, float damage, Entity entity)
    {
        if (_fired) return;

        _fired = true;
        base.Init(position, direction, bulletSpeed * speedMultiplier, damage, entity);
    }

    public override void ResetItem()
    {
        base.ResetItem();
        _fired = false;
    }
}
