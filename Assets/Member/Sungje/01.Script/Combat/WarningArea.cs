using Code.Entities;
using KimMin.ObjectPool.RunTime;
using UnityEngine;
using DG.Tweening;
using System.Collections;

namespace Code.Combat
{
    public class WarningArea : MonoBehaviour, IPoolable
    {
        [SerializeField] private OverlapDamageCaster damageCaster;
        [SerializeField] private Collider2D hitCollider;
        [SerializeField] private SpriteRenderer spriteRenderer;
        [SerializeField] private float warningTime = 1.5f;
        [SerializeField] private Color startColor = Color.white;
        [SerializeField] private Color endColor = Color.red;

        private float _timer;
        private int _damage;
        private Pool _pool;
        private bool _hasDealtDamage;
        private Tween _colorTween;
        private Tween _scaleTween;

        [field: SerializeField] public PoolItemSO PoolItem { get; private set; }
        public GameObject GameObject => gameObject;

        public void Init(Vector3 position, int damage, Entity owner)
        {
            StopAllCoroutines();

            _damage = damage;
            _timer = 0f;
            _hasDealtDamage = false;

            transform.position = position;
            transform.localScale = Vector3.one;
            gameObject.SetActive(true);

            if (hitCollider != null)
                hitCollider.enabled = true;

            if (spriteRenderer != null)
            {
                spriteRenderer.color = startColor;
                _colorTween?.Kill();
                _colorTween = spriteRenderer
                    .DOColor(endColor, warningTime)
                    .SetEase(Ease.Linear);
            }

            damageCaster.InitCaster(owner);
        }

        private void FixedUpdate()
        {
            if (_hasDealtDamage)
                return;

            _timer += Time.fixedDeltaTime;

            if (_timer >= warningTime)
            {
                _hasDealtDamage = true;
                StartCoroutine(ExplodeRoutine());
            }
        }

        private IEnumerator ExplodeRoutine()
        {
            damageCaster.CastDamage(_damage);

            yield return new WaitForFixedUpdate();

            if (hitCollider != null)
                hitCollider.enabled = false;

            _scaleTween?.Kill();
            _scaleTween = transform
                .DOScale(1.25f, 0.12f)
                .SetEase(Ease.OutBack)
                .OnComplete(() =>
                {
                    _pool.Push(this);
                });
        }

        public void SetUpPool(Pool pool)
        {
            _pool = pool;
        }

        public void ResetItem()
        {
            StopAllCoroutines();

            _colorTween?.Kill();
            _scaleTween?.Kill();

            _timer = 0f;
            _damage = 0;
            _hasDealtDamage = false;

            if (spriteRenderer != null)
                spriteRenderer.color = startColor;

            if (hitCollider != null)
                hitCollider.enabled = false;
        }
    }
}
