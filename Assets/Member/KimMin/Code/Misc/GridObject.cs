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
        public GridType Type { get; private set; }

        private bool _isColored;
        private float _duration;

        private void Update()
        {
            if (!_isColored) return;
            
            _duration -= Time.deltaTime;
            if (_duration <= 0)
                ResetModify();
        }

        public void SetDestroyState(bool isDestroyed)
        {
            if(isDestroyed)
                SetModify(Color.grey, int.MaxValue, GridType.Black);
            else
                ResetModify();
        }

        public void SetModify(Color color, float duration, GridType type)
        {
            Fill.DOColor(color, 0.2f);
            _duration = duration;
            _isColored = true;

            Type = type;
        }

        private void ResetModify()
        {
            Fill.DOColor(Color.white, 0.2f);
            _isColored = false;
        }
    }
}