using System;
using Code.Core;
using KimMin.Dependencies;
using UnityEngine;

namespace Code.Test
{
    public class TestMover : MonoBehaviour
    {
        private float duration = 2f;
        private float _prevTime = 0f;

        [Inject] private GridManager _gridManager;

        private void Update()
        {
            if (Time.time - _prevTime > duration)
            {
                _prevTime = Time.time;
            }
        }
    }
}