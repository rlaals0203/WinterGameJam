using System;
using Code.Core;
using Code.GameFlow;
using KimMin.Dependencies;
using UnityEngine;

namespace Code.Entities
{
    public class ExtractorCompo : MonoBehaviour, IEntityComponent
    {
        [SerializeField] private InkExtractor extractor;
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

        private void HandleRightClick()
        {
            var extr = Instantiate(extractor);
            var cellPos = _gridManager.WorldToGrid(_player.Position);
            extr.transform.position = cellPos;
            int area = _gridManager.GetGrid(cellPos).Area;
            var data = InkTable.StageDatas[_stage];
            extr.InitExtractor(data[area]);
        }
    }
}