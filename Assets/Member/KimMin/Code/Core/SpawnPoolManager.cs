using System;
using Code.Combat;
using KimMin.Core;
using KimMin.Dependencies;
using KimMin.Events;
using KimMin.ObjectPool.RunTime;
using UnityEngine;

namespace Code.Core
{
    public class SpawnPoolManager : MonoBehaviour
    {
        [Inject] private PoolManagerMono _poolManager;
        
        private void Awake()
        {
            GameEventBus.AddListener<PlayPoolEffect>(HandlePlayPoolEffect);
            GameEventBus.AddListener<SpawnBulletEvent>(HandleSpawnBullet);
        }

        private void OnDestroy()
        {
            GameEventBus.RemoveListener<PlayPoolEffect>(HandlePlayPoolEffect);
            GameEventBus.RemoveListener<SpawnBulletEvent>(HandleSpawnBullet);
        }

        private void HandleSpawnBullet(SpawnBulletEvent evt)
        {
            var bullet = _poolManager.Pop<Bullet>(evt.poolItem);
            bullet.Init(evt.position, evt.direction, 10f, evt.damage, evt.entity);
        }

        private void HandlePlayPoolEffect(PlayPoolEffect evt)
        {
            
        }
    }
}