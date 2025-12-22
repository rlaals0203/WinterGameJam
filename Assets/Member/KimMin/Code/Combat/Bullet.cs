using KimMin.Core;
using KimMin.Effect;
using KimMin.Events;
using KimMin.ObjectPool.RunTime;
using Unity.Mathematics;
using UnityEngine;

namespace Code.Combat
{
    public class Bullet : MonoBehaviour, IPoolable
    {
        protected Rigidbody2D _rb;
        [SerializeField] private OverlapDamageCaster damageCaster;
        [SerializeField] protected LayerMask enemyLayer;
        [SerializeField] private PoolItemSO effect;
        [SerializeField] private TrailRenderer trailRenderer;
        protected float _damage;
        protected float _speed;
        private Pool _myPool;

        [field:SerializeField] public PoolItemSO PoolItem { get; set; }
        public GameObject GameObject => gameObject;

        protected virtual void Awake()
        {
            _rb = GetComponent<Rigidbody2D>();
        }
        
        public virtual void Init(Vector3 position,Vector2 direction,float bulletSpeed,float damage,Entity entity)
        {
            transform.position = position;
            transform.right = direction;
            _speed = bulletSpeed;
            _rb.linearVelocity = direction * bulletSpeed;
            _damage = damage;
            damageCaster.InitCaster(entity);

            if (trailRenderer != null)
            {
                trailRenderer.enabled = true;
                trailRenderer.time = 0.1f;
            }
        }
        private bool _isHit = false;
        protected virtual void OnTriggerEnter2D(Collider2D collision)
        {
            if ((enemyLayer & (1 << collision.gameObject.layer)) == 0&&!_isHit)
                return;
            _isHit = true;
            OnTrigger(collision);
            PlayEffect();
            if (trailRenderer != null)
            {
                trailRenderer.enabled = false;
                trailRenderer.time = 0f;
            }
            _myPool.Push(this);
        }

        private void PlayEffect()
        {
            GameEventBus.RaiseEvent(EffectEvents.PlayPoolEffect.Initializer(
                transform.position, Quaternion.identity,
                PoolItem, 5f));
        }

        protected virtual void OnTrigger(Collider2D collision)
        {
            damageCaster.CastDamage(_damage);
        }

        public void SetUpPool(Pool pool) => _myPool = pool;

        public virtual void ResetItem()
        {
            _isHit = false;
            if (trailRenderer != null)
            {
                trailRenderer.enabled = false;
                trailRenderer.time = 0f;
            }
        }
    }
}