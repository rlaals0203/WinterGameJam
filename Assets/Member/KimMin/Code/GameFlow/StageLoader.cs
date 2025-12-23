using System;
using Code.Core;
using KimMin.Dependencies;
using UnityEngine;

namespace Code.GameFlow
{
    public class StageLoader : MonoBehaviour
    {
        [SerializeField] private StageDataSO[] stages;

        [Inject] private GridManager _gridManager;

        private void Awake()
        {
            var currentStage = stages[GameManager.Instance.currentStage];
            _gridManager.CreateGrids(currentStage);
        }
    }
}