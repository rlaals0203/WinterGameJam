using System;
using System.Collections.Generic;
using Code.Core;
using KimMin.Dependencies;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Code.GameFlow
{
    public class EnemySpawner : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI mobCountText;
        [SerializeField] private StageDataSO[] datas;
        
        private List<Enemy> _enemyList;
        private StageDataSO _currentStage;
        private float _delay;
        private float _lastTime;
        private int _enemyLeft;

        [Inject] private GridManager _gridManager;

        private void Awake()
        {
            if (GameManager.Instance.isCombatMode)
                InitStage();
        }

        private void InitStage()
        {
            _currentStage = datas[GameManager.Instance.currentStage];
            _enemyList = _currentStage.enemyList;
            _delay = _currentStage.enemyDelay;
            _enemyLeft = _currentStage.enemyCount;
        }

        private void Update()
        {
            if (Time.time - _lastTime > _delay)
            {
                _lastTime = Time.time;
                SpawnEnemy();
            }
        }

        private void SpawnEnemy()
        {
            var target = _enemyList[Random.Range(0, _enemyList.Count)];
            var enemy = Instantiate(target, _gridManager.
                GetRandomBorderGrid().Position, Quaternion.identity);
            _enemyLeft--;
            mobCountText.text = $"남은 적 : {_enemyLeft}/{_currentStage.enemyCount}";
        }
    }
}