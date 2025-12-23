using Code.Entities;
using KimMin.ObjectPool.RunTime;
using UnityEngine;

namespace Code.Combat
{
    public class WarningArea : MonoBehaviour, IPoolable
    {
        [SerializeField] private OverlapDamageCaster damageCaster;
        [SerializeField] private float delay = 0.6f;

        private float _timer;
        private int _damage;
        private Pool _pool;

        [field: SerializeField] public PoolItemSO PoolItem { get; private set; }
        public GameObject GameObject => gameObject;

        public void Init(Vector3 position, int damage, Entity owner)
        {
            transform.position = position;
            _damage = damage;
            _timer = 0f;

            damageCaster.InitCaster(owner);
        }

        private void FixedUpdate()
        {
            _timer += Time.fixedDeltaTime;

            if (_timer >= delay)
            {
                damageCaster.CastDamage(_damage);
                _pool.Push(this);
            }
        }

        public void SetUpPool(Pool pool)
        {
            _pool = pool;
        }

        public void ResetItem()
        {
            _timer = 0f;
            _damage = 0;
        }
    }
}
