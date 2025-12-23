using UnityEngine;

namespace Code.Combat
{
    public abstract class DamageCaster : MonoBehaviour
    {
        [SerializeField] protected int maxHitCount = 1;
        [SerializeField] protected ContactFilter2D contactFilter;

        protected Entity _owner;

        public virtual void InitCaster(Entity owner)
        {
            _owner = owner;
        }

        public abstract bool CastDamage(int damage);

        public bool CanCounter { get; set; }
        public Transform TargetTrm => _owner.transform;
    }
}