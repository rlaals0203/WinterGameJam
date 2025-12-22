using KimMin.Core;
using KimMin.Events;
using Unity.Cinemachine;
using UnityEngine;

namespace Code.Core
{
    public class CameraManager : MonoBehaviour
    {
        [SerializeField] private CinemachineImpulseSource impulseSource;

        private void Awake()
        {
            GameEventBus.AddListener<ImpulseEvent>(HandleCameraImpulse);
        }

        private void OnDestroy()
        {
            GameEventBus.RemoveListener<ImpulseEvent>(HandleCameraImpulse);
        }

        private void HandleCameraImpulse(ImpulseEvent evt)
        {
            impulseSource.GenerateImpulse(evt.power);   
        }
    }
}