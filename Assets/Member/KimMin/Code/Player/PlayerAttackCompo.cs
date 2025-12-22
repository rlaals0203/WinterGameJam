using System;
using System.Collections.Generic;
using Code.Core;
using Code.Misc;
using KimMin.Dependencies;
using UnityEngine;

namespace Code.Entities
{
    public class PlayerAttackCompo : MonoBehaviour, IEntityComponent
    {
        [SerializeField] private GameObject arrowObject;
        
        private Player _player;
        private Vector3Int _direction;
        private PlayerMovement _movementCompo;
        private PlayerInkCompo _inkCompo;
        
        private List<GridObject> _prevGrids;
        private Color _gizmoColor = new Color32(255, 200, 200, 100);

        private readonly int _inkSkillAmount = 10;

        [Inject] private GridManager _gridManager;
        [Inject] private InkStorage _inkStorage;

        public void Initialize(Entity entity)
        {
            _player = entity as Player;

            _inkCompo = entity.GetCompo<PlayerInkCompo>();
            _movementCompo = entity.GetCompo<PlayerMovement>();
            _movementCompo.OnPositionChanged += SetGridGizmo;
            _player.PlayerInput.OnRightClickPressed += HandleRightClick;
        }

        private void HandleRightClick()
        {
            if (!_inkStorage.HasInk(_inkCompo.CurrentInk)) return;
            var grid = _gridManager.GetGrid(_gridManager.WorldToGrid(_player.Position));
            var ink = _inkCompo.CurrentInk;
            
            if(grid.Type == ink) return;
            _inkStorage.ModifyInk(ink, -_inkSkillAmount);
            grid.SetModify(_gridManager.GetGridColor(ink), ink);
        }

        private void OnDestroy()
        {
            _movementCompo.OnPositionChanged -= SetGridGizmo;
        }

        private void Update()
        {
            SetArrow();
        }

        private void SetArrow()
        {
            Vector2 center = new Vector2(Screen.width / 2f, Screen.height / 2f);
            Vector2 local = _player.PlayerInput.MousePosition - center;
            Vector3Int newDir;
            
            if (local.y > local.x && local.y > -local.x)
                newDir = Vector3Int.up;
            else if (local.y < local.x && local.y > -local.x)
                newDir = Vector3Int.right;
            else if (local.y < local.x && local.y < -local.x)
                newDir = Vector3Int.down;
            else
                newDir = Vector3Int.left;

            if (newDir == _direction)
                return;

            _direction = newDir;

            SetArrowTransform();
            SetGridGizmo();
        }

        private void SetArrowTransform()
        {
            arrowObject.transform.position =
                _player.transform.position + _direction;

            arrowObject.transform.rotation = _direction switch {
                { x: 0, y: 1 }  => Quaternion.Euler(0, 0, 0),
                { x: 1, y: 0 }  => Quaternion.Euler(0, 0, 270),
                { x: 0, y: -1 } => Quaternion.Euler(0, 0, 180),
                _ => Quaternion.Euler(0, 0, 90),
            };
        }

        private void SetGridGizmo()
        {
            if (_prevGrids != null)
            {
                foreach (var grid in _prevGrids)
                {
                    grid.ClearModify();
                }
            }
            
            var grids = _prevGrids =_gridManager.GetForwardGrid
                (_movementCompo.Position, _direction, 2, 0);
            
            List<GridObject> removeList = new();

            foreach (var grid in grids)
            {
                if (grid.Type != InkType.None) {
                    removeList.Add(grid);
                    continue;
                }
                
                grid.SetModify(_gizmoColor, InkType.None);
            }
            
            foreach (var item in removeList)
            {
                _prevGrids.Remove(item);
            }
        }
    }
}