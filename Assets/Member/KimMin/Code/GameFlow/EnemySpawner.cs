using System;
using System.Collections.Generic;
using Code.Core;
using KimMin.Core;
using KimMin.Dependencies;
using KimMin.Events;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
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

        private void Awake()
        {
            if (!GameManager.Instance.isCombatMode)
            {
                mobCountText.gameObject.SetActive(false);
                gameObject.SetActive(false);
            }
            
            GameEventBus.AddListener<EnemyDeadEvent>(HandleEnemyDead);
            InitStage();
        }

        private void HandleEnemyDead(EnemyDeadEvent evt)
        {
            _enemyLeft--;
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
            if (Time.time - _lastTime > _delay && _enemyLeft > 0) {
                _lastTime = Time.time;
                SpawnEnemy();
            }

            if (_enemyLeft == 0) {
                GameManager.Instance.isCombatMode = false;
                if (GameManager.Instance.currentStage < 3)
                    GameManager.Instance.currentStage++;
                SceneManager.LoadScene("GameScene");
            }
        }

        private void SpawnEnemy()
        {
            var target = _enemyList[Random.Range(0, _enemyList.Count)];
            var enemy = Instantiate(target, GridManager.Instance.
                GetRandomBorderGrid().Position, Quaternion.identity);
            mobCountText.text = $"남은 적 : {_enemyLeft}/{_currentStage.enemyCount}";
        }
    }
}