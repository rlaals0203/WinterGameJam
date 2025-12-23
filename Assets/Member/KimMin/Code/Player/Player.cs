using DG.Tweening;
using KimMin.Dependencies;
using UnityEngine;

namespace Code.Entities
{
    public class Player : Entity, IDependencyProvider, IDamageable
    {
        private int maxHealth = 100;

        [SerializeField] private SpriteRenderer spriteRenderer;
        [SerializeField] private float hitFlashTime = 0.1f;

        [SerializeField] private int currentHealth;
        public int MaxHealth => maxHealth;
        public int CurrentHealth => currentHealth;

        [field: SerializeField] public PlayerInputSO PlayerInput { get; private set; }
        public int RemainTripleAttack { get; set; } = 0;
        public int RemainDoubleRadius { get; set; } = 0;
        public bool IsCombatMode { get; set; } = false;
        public Vector3 Position => transform.position;

        [Provide]
        public Player ProvidePlayer() => this;

        protected override void Awake()
        {
            base.Awake();
            currentHealth = maxHealth;
        }

        public void TakeDamage(int damage)
        {
            
        }

        public void ApplyDamage(int damage)
        {
            currentHealth -= damage;
            if (currentHealth < 0)
                currentHealth = 0;

            spriteRenderer.DOKill();
            spriteRenderer.color = Color.white;

            spriteRenderer
                .DOColor(Color.red, hitFlashTime)
                .SetLoops(2, LoopType.Yoyo);

            if (currentHealth == 0)
            {
                DestroyObject();
            }
        }
    }
}
