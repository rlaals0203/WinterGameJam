using System;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

namespace Code.Entities
{
    public class PlayerMovement : MonoBehaviour, IEntityComponent
    {
        [SerializeField] private PlayerInputSO playerInput;
        
        public Vector2 Direction { get; private set; }
        public Vector2 Position { get; private set; }

        private Player _player;
        private Queue<Vector2> _movementQueue = new();
        
        public void Initialize(Entity entity)
        {
            _player = entity as Player;
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
            Vector2 dir = _movementQueue.Dequeue();
            Position += dir;
            float duration = 0.1f - (_movementQueue.Count * 0.02f);
            _player.transform.DOMove(Position, duration);
        }

        private void HandleMove()
        {
            Vector2 dir = playerInput.MovementKey;
            if (!CheckCanMove(dir)) return;
            _movementQueue.Enqueue(dir);
        }

        private bool CheckCanMove(Vector2 dir)
        {
            return true;
        }
    }
}