using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace KimMin.UI.Bar
{
    public class BarComponent : MonoBehaviour
    {
        [SerializeField] private Image fill;
        [SerializeField] private Image trailFill;

        private Tween _trailTween;
        
        public void SetSlider(float current, float max)
        {
            float target = current / max;
            fill.transform.localScale = new Vector3(target, 1, 1);
            _trailTween?.Kill();
            _trailTween = DOVirtual.DelayedCall(0.25f, () =>
            {
                trailFill.transform.DOScaleX(target, 0.5f);
            });
        }
    }
}