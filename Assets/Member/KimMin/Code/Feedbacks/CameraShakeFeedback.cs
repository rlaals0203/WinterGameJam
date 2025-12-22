using Unity.Cinemachine;
using UnityEngine;

namespace Code.Feedbacks
{
    public class CameraShakeFeedback : Feedback
    {
        [SerializeField] private float impulseForce = 0.6f;
        [SerializeField] private CinemachineImpulseSource impulseSource;
        
        public override void PlayFeedback()
        {
            impulseSource.GenerateImpulse(impulseForce); 
        }

        public override void StopFeedback() { }
    }
}