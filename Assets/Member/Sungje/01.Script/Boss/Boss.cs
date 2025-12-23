using Code.Entities;
using KimMin.Dependencies;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : Enemy
{
    [SerializeField] private List<Pattern> patterns;
    [SerializeField] private float patternInterval = 2f;

    [Inject] public Player Player;

    private int _currentIndex;
    private bool _isRunning;

    protected override void Awake()
    {
        base.Awake();
        StartCoroutine(PatternRoutine());
    }

    private IEnumerator PatternRoutine()
    {
        _isRunning = true;

        while (_isRunning)
        {
            if (patterns.Count == 0)
            {
                yield return null;
                continue;
            }

            patterns[_currentIndex].Execute(this);

            _currentIndex = (_currentIndex + 1) % patterns.Count;

            yield return new WaitForSeconds(patternInterval);
        }
    }
}
