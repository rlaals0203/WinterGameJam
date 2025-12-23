using System;
using System.Collections.Generic;
using Code.Core;
using Code.GameFlow;
using DG.Tweening;
using KimMin.Dependencies;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

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
        private int _remainExtractor = 10;


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

            _gridManager.GetGrid(cellPos).CannotStand = true;
            extr.InitExtractor(data[area - 1]);
            AddInk(data[area - 1]);
            _remainExtractor--;

            if (_remainExtractor <= 0)
                SceneManager.LoadScene("Ready");
        }

        private void AddInk(InkData[] data)
        {
            int rand = Random.Range(0, data.Length);
            var ink = data[rand];
            InkStorage.Instance.ModifyInk(ink.InkType, 10);
            Debug.Log($"{ink.InkType}");
        }
    }
}