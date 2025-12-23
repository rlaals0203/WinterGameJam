using System;
using System.Collections.Generic;
using Code.Core;
using EasyTransition;
using KimMin.Core;
using KimMin.Dependencies;
using KimMin.Events;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

namespace Code.GameFlow
{
    public class EnemySpawner : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI mobCountText;
        [SerializeField] private StageDataSO[] datas;
        [SerializeField] private TransitionSettings paintEffect;

private List<Enemy> _enemyList;
        private StageDataSO _currentStage;
        private float _delay;
        private float _lastTime;

        private int _enemyToSpawn;
        private int _enemyAlive;
        private int _totalEnemyCount;

        private bool _isComplete;

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

        private void InitStage()
        {
            _currentStage = datas[GameManager.Instance.currentStage];
            _enemyList = _currentStage.enemyList;
            _delay = _currentStage.enemyDelay;
            _enemyToSpawn = _currentStage.enemyCount;
            _enemyAlive = 0;
            _totalEnemyCount = _currentStage.enemyCount;
            UpdateText();
        }

        private void Update()
        {
            if (Keyboard.current.lKey.wasPressedThisFrame)
            {
                GameManager.Instance.isCombatMode = false;
                OnComplete();
            }

            if (Time.time - _lastTime > _delay && _enemyToSpawn > 0)
            {
                _lastTime = Time.time;
                SpawnEnemy();
            }

            if (_enemyToSpawn == 0 && _enemyAlive == 0 && !_isComplete)
            {
                OnComplete();
            }
        }

        private void SpawnEnemy()
        {
            _enemyToSpawn--;
            _enemyAlive++;

            var target = _enemyList[Random.Range(0, _enemyList.Count)];
            Instantiate(
                target,
                GridManager.Instance.GetRandomBorderGrid().Position,
                Quaternion.identity
            );
        }

        private void HandleEnemyDead(EnemyDeadEvent evt)
        {
            _enemyAlive--;
            UpdateText();
        }

        private void UpdateText()
        {
            int remaining = _enemyToSpawn + _enemyAlive;
            mobCountText.text = $"남은 적 : {remaining}/{_totalEnemyCount}";
        }

        private void OnComplete()
        {
            _isComplete = true;
            GameManager.Instance.isCombatMode = false;
            if (GameManager.Instance.currentStage < 3)
                GameManager.Instance.currentStage++;
            TransitionManager.Instance().Transition(SceneName.Game, paintEffect, 0);
        }
    }
}