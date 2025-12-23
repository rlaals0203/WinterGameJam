using Blade.SoundSystem;
using Code.Entities;
using DG.Tweening;
using KimMin.Core;
using KimMin.Events;
using KimMin.StatSystem;
using UnityEngine;

namespace Code.Combat
{
    public class EntityHealth : MonoBehaviour,IEntityComponent, IDamageable, IAfterInitialize
    {
        private Entity _entity;
        private EntityStat _statCompo;

        [SerializeField] private StatSO hpStat;
        [SerializeField] private float maxHealth;
        [SerializeField] private float currentHealth;
        [SerializeField] private SpriteRenderer renderer;
        [SerializeField] private SoundSO hitSound;
        
        public delegate void HealthChange(float current, float max);
        public event HealthChange OnHealthChangeEvent;
        
        public void Initialize(Entity entity)
        {
            _entity = entity;
            _statCompo = entity.GetCompo<EntityStat>();
        }
        
        public void AfterInitialize()
        {
            currentHealth = maxHealth = _statCompo.SubscribeStat(hpStat, HandleMaxHPChange, 10f);
        }

        private void Start()
        {
            OnHealthChangeEvent?.Invoke(currentHealth, maxHealth);
        }

        private void OnDestroy()
        {
            _statCompo.UnSubscribeStat(hpStat,HandleMaxHPChange );
        }

        private void HandleMaxHPChange(StatSO stat, float currentvalue, float prevvalue)
        {
            float changed = currentvalue - prevvalue;
            maxHealth = currentvalue;
            if (changed > 0)
            {
                currentHealth = Mathf.Clamp(currentHealth + changed, 0, maxHealth);
            }
            else
            {
                currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
            }
        }

        public void ApplyDamage(int damage)
        {
            currentHealth = Mathf.Clamp(currentHealth -damage, 0, maxHealth);
            OnHealthChangeEvent?.Invoke(currentHealth, maxHealth);
            GameEventBus.RaiseEvent(SoundEvents.PlaySFXEvent.Initialize(hitSound));
            
            if (currentHealth <= 0)
            {
                _entity.OnDeadEvent?.Invoke();
            }

            renderer.DOColor(Color.white, 0.1f).SetLoops(1, LoopType.Yoyo);
            _entity.OnHitEvent?.Invoke();
        }
    }
}