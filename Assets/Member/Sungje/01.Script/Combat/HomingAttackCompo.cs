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
        private readonly SpawnHomingBulletEvent _spawnEvent = SpawnEvents.SpawnHomingBullet;

        protected override void ProcessAttack()
        {
            GameEventBus.RaiseEvent(
                _spawnEvent.Init(
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
