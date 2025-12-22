using System;
using System.Collections.Generic;
using Code.Core;
using Code.GameFlow;
using DG.Tweening;
using KimMin.Dependencies;
using UnityEngine;

namespace Code.Entities
{
    public class ExtractorCompo : MonoBehaviour, IEntityComponent
    {
        [SerializeField] private InkExtractor extractor;
        [SerializeField] private LayerMask whatIsExtractor;
        [SerializeField] private float detectRadius = 3f;
        [Inject] private GridManager _gridManager;

        private Player _player;
        private int _stage;

        public void Initialize(Entity entity)
        {
            _player = entity as Player;
            _player.PlayerInput.OnRightClickPressed += HandleRightClick;
        }

        private void OnDestroy()
        {
            _player.PlayerInput.OnRightClickPressed -= HandleRightClick;
        }

        private void Update()
        {
            //FindExtractor();
        }

        private void FindExtractor()
        {
            Collider2D[] hits = Physics2D.OverlapCircleAll(_player.transform.position,
                detectRadius, whatIsExtractor);

            HashSet<InkExtractor> inRange = new HashSet<InkExtractor>();

            foreach (var hit in hits)
            {
                if (!hit.TryGetComponent<InkExtractor>(out var extractor))
                    continue;

                inRange.Add(extractor);
                if (!extractor.IsVisible)
                {
                    extractor.extractorUI.Root.DOKill();
                    extractor.IsVisible = true;
                    extractor.extractorUI.Root
                        .DOScale(1f, 1f)
                        .SetEase(Ease.OutBack);
                }
            }

            foreach (var extractor in InkExtractor.All)
            {
                if (inRange.Contains(extractor) || !extractor.IsVisible) continue;
                extractor.extractorUI.Root.DOKill();
                extractor.IsVisible = false;
                extractor.extractorUI.Root
                    .DOScale(0f, 1f)
                    .SetEase(Ease.InBack);
            }
        }

        private void HandleRightClick()
        {
            var extr = Instantiate(extractor);
            var cellPos = _gridManager.WorldToGrid(_player.Position);
            extr.transform.position = cellPos;
            int area = _gridManager.GetGrid(cellPos).Area;
            var data = InkTable.StageDatas[_stage];
            extr.InitExtractor(data[area - 1]);
        }
    }
}