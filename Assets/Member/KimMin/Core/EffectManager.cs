using KimMin.Dependencies;
using KimMin.Effect;
using KimMin.Events;
using KimMin.ObjectPool.RunTime;
using UnityEngine;

namespace KimMin.Core.Managers
{
    public class EffectManager : MonoBehaviour
    {
        [SerializeField] private PoolItemSO popupTextItem;
        
        [Inject] private PoolManagerMono _poolManager;

        private void Awake()
        {
            GameEventBus.AddListener<PlayPoolEffect>(HandlePlayPoolEffect);
        }

        private void OnDestroy()
        {
            GameEventBus.RemoveListener<PlayPoolEffect>(HandlePlayPoolEffect);
        }

        private async void HandlePlayPoolEffect(PlayPoolEffect evt)
        {
            PoolingEffect effect = _poolManager.Pop<PoolingEffect>(evt.targetItem);
            effect.PlayVFX(evt.position, evt.rotation);
            await Awaitable.WaitForSecondsAsync(evt.duration);
            _poolManager.Push(effect);
        }
    }
}