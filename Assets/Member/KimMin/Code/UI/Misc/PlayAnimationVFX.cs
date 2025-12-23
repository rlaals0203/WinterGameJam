using KimMin.Effect;
using UnityEngine;

namespace KimMin.UI.Misc
{
    public class PlayAnimationVFX : MonoBehaviour, IPlayableVFX
    {
        [SerializeField] private AnimationClip clip;
        [SerializeField] private Animator animator;
        [SerializeField] private GameObject effectObj;
        public string VFXName => clip.name;
        private bool _isPlaying;
        
        private void Update()
        {
            if(!_isPlaying) return;
            var info = animator.GetCurrentAnimatorStateInfo(0);
            if (info.normalizedTime >= 1f && !info.loop)
            {
                effectObj.SetActive(false);
                _isPlaying = false;
            }
        }

        public void PlayVFX(Vector2 position, Quaternion rotation)
        {
            effectObj.SetActive(true);
            animator.Play(VFXName);
            transform.position = position;
            transform.rotation = rotation;
            _isPlaying = true;
        }

        public void StopVFX()
        {
            animator.StopPlayback();
        }
    }
}