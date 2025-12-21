using System;
using System.Collections.Generic;
using Blade.SoundSystem;
using MinLibrary.Core;
using MinLibrary.Dependencies;
using MinLibrary.ObjectPool.RunTime;
using UnityEngine;

namespace Blade.Managers
{
    public class SoundManager : MonoBehaviour
    {
        [SerializeField] private PoolItemSO soundPlayer;

        [Inject] private PoolManagerMono _poolManager;
        
        private Dictionary<int, SoundPlayer> _soundPlayerDict = new Dictionary<int, SoundPlayer>();

        private void Awake()
        {
            GameEventBus.AddListener<PlaySFXEvent>(HandlePlaySFXEvent);
            GameEventBus.AddListener<StopSoundEvent>(HandleStopSoundEvent);
        }

        private void OnDestroy()
        {
            GameEventBus.RemoveListener<PlaySFXEvent>(HandlePlaySFXEvent);
            GameEventBus.RemoveListener<StopSoundEvent>(HandleStopSoundEvent);
        }

        private void HandlePlaySFXEvent(PlaySFXEvent evt)
        {
            SoundPlayer player = _poolManager.Pop<SoundPlayer>(soundPlayer);
            player.transform.position = evt.position;
            player.PlaySound(evt.clip);

            if (evt.channel > 0 && evt.clip.loop)
            {
                if (_soundPlayerDict.TryGetValue(evt.channel, out SoundPlayer beforePlayer))
                {
                    beforePlayer.StopAndGotoPool();
                    _soundPlayerDict.Remove(evt.channel);
                }
                _soundPlayerDict.Add(evt.channel, player);
            }else if(evt.channel <= 0 && evt.clip.loop)
            {
                Debug.LogWarning($"사운드 루프 설정이 되었으나 채널이 0 이하입니다. {evt.clip.name}");   
            }
        }

        private void HandleStopSoundEvent(StopSoundEvent evt)
        {
            if (_soundPlayerDict.TryGetValue(evt.channel, out SoundPlayer beforePlayer))
            {
                beforePlayer.StopAndGotoPool();
                _soundPlayerDict.Remove(evt.channel);
            }
        }
    }
}