using KimMin.Core;
using KimMin.Events;
using KimMin.ObjectPool.RunTime;
using UnityEngine;

namespace Code.Combat
{
    public class BulletAttackCompo : EnemyAttackCompo
    {
        [SerializeField] private PoolItemSO bullet;
        private readonly SpawnBulletEvent _bulletEvent = SpawnEvents.SpawnBulletEvent;
        public override void Initialize(Entity entity)
        {
            base.Initialize(entity);
        }

        public async override void TryDoAttack()
        {
            for (int i = 0; i < 3; i++)
            {
                Vector2 dir = _enemy.Player.Position - _enemy.transform.position;
                GameEventBus.RaiseEvent(_bulletEvent.Init(bullet, _enemy.transform.position, 
                    dir, 10f, _enemy));
                await Awaitable.WaitForSecondsAsync(0.15f);
            }
        }
    }
}