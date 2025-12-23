using System;
using System.Collections.Generic;
using Code.Core;
using DG.Tweening;
using KimMin.Dependencies;
using UnityEngine;

namespace Code.Entities
{
    public class PlayerMovement : MonoBehaviour, IEntityComponent
    {
        [SerializeField] private PlayerInputSO playerInput;
        public Vector2 Position { get; private set; }

        public event Action OnPositionChanged;

        private Player _player;
        private Queue<Vector2> _movementQueue = new();
        [Inject] private GridManager _gridManager;
        
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

            var cellPos = _gridManager.WorldToGrid(Position);
            _gridManager.ApplyGridBuff(_gridManager.GetGrid(cellPos), _player);
            
            float duration = 0.1f - (_movementQueue.Count * 0.02f);
            
            _player.transform.DOMove(Position, duration);
            OnPositionChanged?.Invoke();
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