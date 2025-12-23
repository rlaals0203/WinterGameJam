using System;
using System.Collections.Generic;
using Code.Combat;
using Code.Core;
using Code.Misc;
using DG.Tweening;
using KimMin.Dependencies;
using UnityEngine;

namespace Code.Entities
{
    public class PlayerAttackCompo : MonoBehaviour, IEntityComponent
    {
        [SerializeField] private GameObject arrowObject;
        [SerializeField] private OverlapDamageCaster damageCaster;
        
        private Player _player;
        private Vector3Int _direction;
        private PlayerMovement _movementCompo;
        private PlayerInkCompo _inkCompo;
        private List<GridObject> _prevGrids;
        private Color _gizmoColor = new Color32(255, 200, 200, 100);

        private readonly int _inkSkillAmount = 10;
        
        private Vector2 Range => _player.RemainDoubleRadius > 0 ? new Vector2(2, 1) : new Vector2(1, 1);

        [Inject] private GridManager _gridManager;
        [Inject] private InkStorage _inkStorage;

        public void Initialize(Entity entity)
        {
            _player = entity as Player;
            damageCaster.InitCaster(entity);

            _inkCompo = entity.GetCompo<PlayerInkCompo>();
            _movementCompo = entity.GetCompo<PlayerMovement>();
            _movementCompo.OnPositionChanged += SetGridGizmo;
            _player.PlayerInput.OnRightClickPressed += HandleRightClick;
            _player.PlayerInput.OnLeftClickPressed += HandleLeftClick;
        }

        private void HandleRightClick()
        {
            if (!_inkStorage.HasInk(_inkCompo.CurrentInk) ||
                !_player.IsCombatMode) return;
            var grid = _gridManager.GetGrid(_gridManager.WorldToGrid(_player.Position));
            var ink = _inkCompo.CurrentInk;
            
            if(grid.Type == ink || grid.Type == InkType.Destroyed) return;
            _inkStorage.ModifyInk(ink, -_inkSkillAmount);
            grid.SetModify(Utility.GetGridColor(ink), ink);
        }

        private void HandleLeftClick()
        {
            if (!_player.IsCombatMode) return;
            CastDamage(GetRangeGrids());
        }

        private void OnDestroy()
        {
            _movementCompo.OnPositionChanged -= SetGridGizmo;
        }

        private void Update()
        {
            SetArrow();
        }

        private void CastDamage(List<GridObject> grids)
        {
            if (_gridManager.TryGetRendererBounds(grids, out var bounds))
            {
                Vector3 pos = bounds.center;
                Vector3 size = bounds.size;
                damageCaster.SetSize(size);
                damageCaster.transform.position = pos;
                damageCaster.CastDamage(10f);
            }
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
            if (!_player.IsCombatMode) return;

            if (_prevGrids != null)
            {
                foreach (var grid in _prevGrids)
                {
                    grid.ClearModify();
                }
            }
            
            List<GridObject> removeList = new();

            foreach (var grid in GetRangeGrids())
            {
                if (grid.Type != InkType.None || (grid.BlinkTween != null 
                                                  && grid.BlinkTween.IsActive())) 
                {
                    removeList.Add(grid);
                    continue;
                }
                
                grid.SetModify(_gizmoColor, InkType.None);
            }
            
            foreach (var item in removeList)
                _prevGrids.Remove(item);
        }
        
        private List<GridObject> GetRangeGrids()
        =>_prevGrids =_gridManager.GetForwardGrid(_movementCompo.Position, 
            _direction, (int)Range.x, (int)Range.y);
    }
}