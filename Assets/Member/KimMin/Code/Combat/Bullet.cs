using Code.Entities;
using KimMin.Core;
using KimMin.Events;
using KimMin.ObjectPool.RunTime;
using UnityEngine;

namespace Code.Combat
{
    public class Bullet : MonoBehaviour, IPoolable
    {
        protected Rigidbody2D _rb;
        [SerializeField] private LayerMask targetLayer;
        [SerializeField] private TrailRenderer trailRenderer;

        protected int _damage;
        protected float _speed;
        protected Entity _owner;

        private Pool _myPool;
        private bool _isHit;

        [field: SerializeField] public PoolItemSO PoolItem { get; set; }
        public GameObject GameObject => gameObject;

        protected virtual void Awake()
        {
            _rb = GetComponent<Rigidbody2D>();
        }

        public virtual void Init(
            Vector3 position,
            Vector2 direction,
            float bulletSpeed,
            int damage,
            Entity owner
        )
        {
            transform.position = position;
            transform.right = direction;
            _speed = bulletSpeed;
            _damage = damage;
            _owner = owner;

            _rb.linearVelocity = direction * bulletSpeed * 0.5f;

            if (trailRenderer != null)
            {
                trailRenderer.enabled = true;
                trailRenderer.time = 0.1f;
            }
        }

        protected virtual void OnTriggerEnter2D(Collider2D collision)
        {
            if (_isHit) return;
            if ((targetLayer & (1 << collision.gameObject.layer)) == 0) return;

            _isHit = true;

            collision.GetComponentInParent<IDamageable>()?.ApplyDamage(_damage);

            GameEventBus.RaiseEvent(
                EffectEvents.PlayPoolEffect.Initializer(
                    transform.position,
                    Quaternion.identity,
                    PoolItem,
                    5f
                )
            );

            if (trailRenderer != null)
            {
                trailRenderer.enabled = false;
                trailRenderer.time = 0f;
            }

            _myPool.Push(this);
        }

        public void SetUpPool(Pool pool)
        {
            _myPool = pool;
        }

        public virtual void ResetItem()
        {
            _isHit = false;
            _rb.linearVelocity = Vector2.zero;
            _owner = null;

            if (trailRenderer != null)
            {
                trailRenderer.enabled = false;
                trailRenderer.time = 0f;
            }
        }
    }
}
