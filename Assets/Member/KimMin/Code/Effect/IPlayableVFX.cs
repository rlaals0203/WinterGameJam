using UnityEngine;

namespace KimMin.Effect
{
    public interface IPlayableVFX
    {
        public string VFXName { get; }
        public void PlayVFX(Vector2 position,Quaternion rotation);
        public void StopVFX();
    }
}