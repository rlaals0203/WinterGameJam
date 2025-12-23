using Code.Combat;
using Code.Core;
using Code.Entities;
using Code.Misc;
using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAttackCompo : MonoBehaviour
{
    private Enemy _enemy;
    private Player _player;

    [SerializeField] private float attackInterval = 3f;
    [SerializeField] private float warningTime = 1.0f;

    [SerializeField] private int forbiddenZoneDamage = 50;
    [SerializeField] private int blackMassDamage = 30;

    private float _lastAttackTime;
    private bool _useForbiddenNext = true;

    private void Awake()
    {
        _enemy = GetComponent<Enemy>();
        _player = FindAnyObjectByType<Player>();
    }

    private void Update()
    {
        if (_enemy == null) return;
        if (_enemy.IsSpoilMode) return;
        if (Time.time - _lastAttackTime < attackInterval) return;

        _lastAttackTime = Time.time;

        if (_useForbiddenNext)
            UseForbiddenZone();
        else
            UseBlackMass();

        _useForbiddenNext = !_useForbiddenNext;
    }

    private void UseForbiddenZone()
    {
        _enemy.IsSpoilMode = true;

        Vector3Int center = GridManager.Instance.WorldToGrid(_enemy.transform.position);
        List<GridObject> grids = GridManager.Instance.GetCellsInRadius(center, 1);

        StartCoroutine(DoGridWarning(grids, warningTime, () =>
        {
            ApplyAttack(grids, forbiddenZoneDamage);
            _enemy.IsSpoilMode = false;
        }));
    }

    private void UseBlackMass()
    {
        _enemy.IsSpoilMode = true;

        Vector3 origin = _enemy.transform.position;
        Vector3 dir = _enemy.transform.right;

        List<GridObject> grids = GridManager.Instance.GetForwardGrid(origin, dir, 5, 1);

        StartCoroutine(DoGridWarning(grids, warningTime, () =>
        {
            ApplyAttack(grids, blackMassDamage);
            _enemy.IsSpoilMode = false;
        }));
    }

    private void ApplyAttack(List<GridObject> grids, int damage)
    {
        for (int i = 0; i < grids.Count; i++)
            grids[i].SetDestroyState(true);

        if (_player == null) return;

        Vector3Int playerCell = GridManager.Instance.WorldToGrid(_player.transform.position);

        for (int i = 0; i < grids.Count; i++)
        {
            Vector3Int cell = GridManager.Instance.WorldToGrid(grids[i].transform.position);
            if (cell == playerCell)
            {
                var health = _player.GetCompo<EntityHealth>();
                health?.ApplyDamage(damage);
                break;
            }
        }
    }

    private IEnumerator DoGridWarning(
        List<GridObject> grids,
        float time,
        Action onFinished
    )
    {
        bool called = false;

        for (int i = 0; i < grids.Count; i++)
        {
            var grid = grids[i];

            if (grid.BlinkTween != null && grid.BlinkTween.IsActive())
                grid.BlinkTween.Kill();

            Color originColor = grid.Fill.color;
            Color targetColor = Utility.GetGridColor(InkType.Black);

            int loops = Mathf.Max(1, Mathf.RoundToInt(time / 0.5f));

            Sequence seq = DOTween.Sequence();
            seq.Append(grid.Fill.DOColor(targetColor, 0.25f));
            seq.Append(grid.Fill.DOColor(originColor, 0.25f));
            seq.SetLoops(loops);

            seq.OnComplete(() =>
            {
                grid.Fill.color = originColor;
                grid.BlinkTween = null;

                if (!called)
                {
                    called = true;
                    onFinished?.Invoke();
                }
            });

            grid.BlinkTween = seq;
        }

        yield return null;
    }
}
