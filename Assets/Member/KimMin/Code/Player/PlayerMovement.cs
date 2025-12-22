using System;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

namespace Code.Player
{
    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField] private PlayerInputSO playerInput;
        
        public Vector2 Direction { get; private set; }
        public Vector2 Position { get; private set; }
        
        private Queue<Vector2> _movementQueue = new();

        private void Awake()
        {
            playerInput.OnMovePressed += HandleMove;
        }

        private void OnDestroy()
        {
            playerInput.OnMovePressed -= HandleMove;
        }

        private void Update()
        {
            if (_movementQueue.Count > 0)
            {
                Move();
            }
        }

        private void Move()
        {
            
        }

        private void HandleMove(Vector2 dir)
        {
            Debug.Log(dir);
            if (!CheckCanMove(dir)) return;
            _movementQueue.Enqueue(dir);
        }

        private bool CheckCanMove(Vector2 dir)
        {
            return true;
        }
    }
}