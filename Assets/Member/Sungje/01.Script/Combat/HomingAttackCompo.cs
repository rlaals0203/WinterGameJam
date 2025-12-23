using Code.Entities;
using KimMin.Core;
using KimMin.Events;
using KimMin.ObjectPool.RunTime;
using UnityEngine;

namespace Code.Combat
{
    public class HomingAttackCompo : EnemyAttackCompo
    {
        [SerializeField] private PoolItemSO homingBullet;
        private readonly SpawnHomingBulletEvent _homingAttackEvent = SpawnEvents.SpawnHomingBullet;

        protected override void ProcessAttack()
        {
            Vector2 dir = (_enemy.Player.Position - _enemy.transform.position).normalized;

            GameEventBus.RaiseEvent(
                _homingAttackEvent.Init(
                    homingBullet,
                    _enemy.transform.position,
                    Data.moveSpeed,
                    Data.damage,
                    _enemy.Player,
                    _enemy
                )
            );
        }
    }
}
