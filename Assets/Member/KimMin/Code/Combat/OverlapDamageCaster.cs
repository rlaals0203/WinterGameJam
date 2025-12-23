using UnityEngine;

namespace Code.Combat
{
    public class OverlapDamageCaster : DamageCaster
    {
        public enum OverlapCastType
        {
            Circle, Box
        }
        [SerializeField] protected OverlapCastType overlapCastType;
        [SerializeField] private Vector2 damageBoxSize;
        [SerializeField] private float damageRadius;

        private Collider2D[] _hitResults;

        public override void InitCaster(Entity owner)
        {
            base.InitCaster(owner);
            _hitResults = new Collider2D[maxHitCount];
        }

        public void SetSize(Vector2 size) => damageBoxSize = size;

        public override bool CastDamage(int damage)
        {
            int cnt = overlapCastType switch
            {
                OverlapCastType.Circle => Physics2D.OverlapCircle(transform.position, damageRadius, contactFilter, _hitResults),
                OverlapCastType.Box => Physics2D.OverlapBox(transform.position, damageBoxSize, 0, contactFilter, _hitResults),
                _ => 0
            };
            

            for (int i = 0; i < cnt; i++)
            {
                if (_hitResults[i].TryGetComponent(out IDamageable damageable))
                {
                    damageable.ApplyDamage(damage);
                }
            }

            return cnt > 0;
        }


#if UNITY_EDITOR
        private void OnDrawGizmos()
        {
            Gizmos.color = new Color(0.7f, 0.7f, 0, 1f);
            switch (overlapCastType)
            {
                case OverlapCastType.Circle:
                    Gizmos.DrawWireSphere(transform.position, damageRadius);
                    break;
                case OverlapCastType.Box:
                    Gizmos.DrawWireCube(transform.position, damageBoxSize);
                    break;
            }
        }
#endif
    }
}