using System;
using UnityEngine;

namespace Code.Misc
{
    public class GridObject : MonoBehaviour
    {
        [field: SerializeField] public SpriteRenderer Outline { get; private set; }
        [field: SerializeField] public SpriteRenderer Fill { get; private set; }
        
        public Vector2 Position => transform.position;

        public void SetOutline(Color color)
        {
            Outline.color = color;
        }

        public void SetFill(Color color)
        {
            Fill.color = color;
        }
    }
}