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
        private int _enemyLeft;

        private bool m_isComplete;

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
            if (Keyboard.current.lKey.wasPressedThisFrame)
            {
                GameManager.Instance.isCombatMode = false;
                OnComplete();
            }
            
            if (Time.time - _lastTime > _delay && _enemyLeft > 0) {
                _lastTime = Time.time;
                SpawnEnemy();
            }

            if (_enemyLeft == 0 && !m_isComplete)
            {
                OnComplete();
            }
        }

        private void OnComplete()
        {
            m_isComplete = true;
            GameManager.Instance.isCombatMode = false;
            if (GameManager.Instance.currentStage < 3)
                GameManager.Instance.currentStage++;
            TransitionManager.Instance().Transition(SceneName.Game, paintEffect, 0);
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