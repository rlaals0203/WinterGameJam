using System;
using Code.Core;
using DG.Tweening;
using UnityEngine;

namespace Code.Misc
{
    public class GridObject : MonoBehaviour
    {
        [field: SerializeField] public SpriteRenderer Outline { get; private set; }
        [field: SerializeField] public SpriteRenderer Fill { get; private set; }
        
        public Vector2 Position => transform.position;
        public InkType Type { get; private set; }
        public Tween BlinkTween { get; set; }
        public bool CannotStand { get; set; }
        public int Area { get; set; }
        
        private Color _originColor = new Color32(255, 255, 255, 50);

        private bool _isColored;
        private float _duration;

        private void Awake()
        {
            Type = InkType.None;
        }

        private void Update()
        {
            if (!_isColored) return;
            
            _duration -= Time.deltaTime;
            if (_duration <= 0)
                ClearModify();
        }

        public void SetDestroyState(bool isDestroyed)
        {
            if (isDestroyed)
            {
                SetModify(Utility.GetGridColor(InkType.Black), InkType.Black);
                Type = InkType.Destroyed;
            }
            else
                ClearModify();
        }

        public void SetModify(Color color, InkType type, float duration = int.MaxValue)
        {
            Fill.DOColor(color, 0.1f);
            _duration = duration;
            _isColored = true;

            Type = type;
        }

        public void ClearModify()
        {
            Fill.DOColor(_originColor, 0.1f);
            _isColored = false;
        }
    }
}