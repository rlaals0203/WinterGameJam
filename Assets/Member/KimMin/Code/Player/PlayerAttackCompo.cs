using System;
using System.Collections.Generic;
using Code.Combat;
using Code.Core;
using Code.Misc;
using DG.Tweening;
using KimMin.Core;
using KimMin.Dependencies;
using KimMin.Events;
using KimMin.ObjectPool.RunTime;
using UnityEngine;

namespace Code.Entities
{
    public class PlayerAttackCompo : MonoBehaviour, IEntityComponent
    {
        [SerializeField] private GameObject arrowObject;
        [SerializeField] private OverlapDamageCaster damageCaster;
        [SerializeField] private SpriteRenderer renderer;
        [SerializeField] private PoolItemSO slashEffect;
        
        private Player _player;
        private Vector3Int _direction;
        private PlayerMovement _movementCompo;
        private PlayerInkCompo _inkCompo;
        private List<GridObject> _prevGrids;
        private Color _gizmoColor = new Color32(200, 75, 75, 200);

        private readonly int _inkSkillAmount = 10;
        
        private Vector2 Range => _player.RemainDoubleRadius > 0 ? new Vector2(2, 1) : new Vector2(1, 1);

        [Inject] private GridManager _gridManager;

        public void Initialize(Entity entity)
        {
            _player = entity as Player;
            damageCaster.InitCaster(entity);

            _inkCompo = entity.GetCompo<PlayerInkCompo>();
            _movementCompo = entity.GetCompo<PlayerMovement>();
            _movementCompo.OnPositionChanged += SetGridGizmo;
            _player.PlayerInput.OnRightClickPressed += HandleRightClick;
        }
        
        private void HandleRightClick()
        {
            if (!GameManager.Instance.isCombatMode) return;
            var grid = _gridManager.GetGrid(_gridManager.WorldToGrid(_player.Position));
            var ink = _inkCompo.CurrentInk;
            
            if(grid.Type == ink || grid.Type == InkType.Destroyed) return;

            var inkLoadout = InkLoadoutManager.Instance;
            
            if(inkLoadout == null ||
               !inkLoadout.savedUsedAmount.ContainsKey(ink) ||
               inkLoadout.savedUsedAmount[ink] < 10) return;
            
            InkLoadoutManager.Instance.savedUsedAmount[ink] -= 10;
            grid.SetModify(Utility.GetGridColor(ink), ink);
        }

        public void Attack()
        {
            CastDamage(GetRangeGrids());
        }

        private void OnDestroy()
        {
            _movementCompo.OnPositionChanged -= SetGridGizmo;
            _player.PlayerInput.OnRightClickPressed -= HandleRightClick;
        }

        private void Update()
        {
            SetArrow();
        }

        private void CastDamage(List<GridObject> grids)
        {
            if (renderer == null) return;

            if (_direction.x == -1)
                renderer.flipX = true;
            else if (_direction.x == 1)
                renderer.flipX = false;

            if (_gridManager.TryGetRendererBounds(grids, out var bounds))
            {
                Vector3 pos = bounds.center;
                Vector3 size = bounds.size;

                damageCaster.SetSize(size);
                damageCaster.transform.position = pos;
                damageCaster.CastDamage(10);

                Collider2D[] hits = Physics2D.OverlapBoxAll(pos, size, 0);
                foreach (var hit in hits)
                {
                    BossHP boss = hit.GetComponent<BossHP>();
                    if (boss != null)
                    {
                        boss.TakeDamage(10);
                    }
                }
            }
            
            GameEventBus.RaiseEvent(EffectEvents.PlayPoolEffect.Initializer(
                bounds.center, Quaternion.Euler(0, 0, GetZRotation() + 90f),
                slashEffect, 1f));
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

            arrowObject.transform.rotation = Quaternion.Euler(0, 0, GetZRotation());
        }

        private float GetZRotation()
        {
            return _direction switch {
                { x: 0, y: 1 }  => 0,
                { x: 1, y: 0 }  => 270,
                { x: 0, y: -1 } => 180,
                _ => 90,
            };
        }

        private void SetGridGizmo()
        {
            if (!GameManager.Instance.isCombatMode ||
                GridManager.Instance == null) return;

            if (_prevGrids != null)
            {
                foreach (var grid in _prevGrids)
                {
                    if (grid.Type == InkType.None)
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