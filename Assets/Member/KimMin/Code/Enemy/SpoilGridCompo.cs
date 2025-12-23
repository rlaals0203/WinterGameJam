using System;
using System.Collections.Generic;
using Code.Core;
using Code.Misc;
using DG.Tweening;
using KimMin.Dependencies;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Code.Entities
{
    public class SpoilGridCompo : MonoBehaviour, IEntityComponent
    {
        [SerializeField] private int chancePerSecond = 2;
        private float _lastTime;

        private Enemy _enemy;
        
        public void Initialize(Entity entity)
        {
            _enemy = entity as Enemy;
        }

        private void Update()
        {
            if (_enemy == null) return;

            if (Time.time - _lastTime > 1f)
            {
                int rand = Random.Range(0, 100);
                _lastTime = Time.time;

                if (rand < chancePerSecond && !_enemy.IsSpoilMode)
                {
                    ProcessSpoil();
                }
            }
        }

        private void ProcessSpoil()
        {
            _enemy.IsSpoilMode = true;

            var cellPos = GridManager.Instance.WorldToGrid(_enemy.transform.position);
            var grids = GridManager.Instance.GetCellsInRadius(cellPos, 1);
            bool isCalled = false;

            foreach (var grid in grids)
            {
                if (grid.BlinkTween != null && grid.BlinkTween.IsActive())
                    grid.BlinkTween.Kill();
                
                Color originColor = grid.Fill.color;
                Color targetColor = Utility.GetGridColor(InkType.Black);
                grid.Fill.color = originColor;

                Sequence seq = DOTween.Sequence();
                seq.Append(grid.Fill.DOColor(targetColor, 0.3f));
                seq.Append(grid.Fill.DOColor(originColor, 0.3f));
                seq.SetLoops(6);

                seq.OnComplete(() => {
                    grid.Fill.color = originColor;
                    if (!isCalled) {
                        isCalled = true;
                        OnSpoilBlinkFinished(grids); }
                    grid.BlinkTween = null; });
                grid.BlinkTween = seq;
            }
        }

        private void OnSpoilBlinkFinished(List<GridObject> grids)
        {
            foreach (var grid in grids)
            {
                grid.SetDestroyState(true);
            }
            
            _enemy.IsSpoilMode = false;
        }
    }
}