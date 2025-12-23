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
            GameEventBus.AddListener<SpawnEnergyBallEvent>(HandleSpawnEnergyBall);
        }

        private void OnDestroy()
        {
            GameEventBus.RemoveListener<PlayPoolEffect>(HandlePlayPoolEffect);
            GameEventBus.RemoveListener<SpawnBulletEvent>(HandleSpawnBullet);
            GameEventBus.RemoveListener<SpawnEnergyBallEvent>(HandleSpawnEnergyBall);
        }

        private void HandleSpawnEnergyBall(SpawnEnergyBallEvent evt)
        {
            var bullet = _poolManager.Pop<EnergyBall>(evt.poolItem);
            bullet.Init(evt.position, evt.direction, 5f, evt.damage, evt.entity);
        }

        private void HandleSpawnBullet(SpawnBulletEvent evt)
        {
            var bullet = _poolManager.Pop<Bullet>(evt.poolItem);
            bullet.Init(evt.position, evt.direction, 5f, evt.damage, evt.entity);
        }


        private void HandlePlayPoolEffect(PlayPoolEffect evt)
        {
            
        }
    }
}