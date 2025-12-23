using Code.Core;
using Code.Entities;
using Code.Misc;
using KimMin.Dependencies;
using UnityEngine;

[RequireComponent(typeof(Enemy))]
public class EnemyMoveCompo : MonoBehaviour, IEntityComponent
{
    private float duration = 1.2f;
    private float _prevTime;
    private Enemy _enemy;

    [Inject] private GridManager _gridManager;

    public void Initialize(Entity entity)
    {
        if (entity is not Enemy enemy)
        {
            enabled = false;
            return;
        }

        if (enemy.EnemyDataSO == null)
        {
            enabled = false;
            return;
        }

        _enemy = enemy;
        duration = _enemy.EnemyDataSO.moveSpeed;
        _prevTime = Time.time;
    }

    private void Update()
    {
        if (_enemy == null || _gridManager == null) return;
        ProcessMove();
    }

    private void ProcessMove()
    {
        if (Time.time - _prevTime <= duration) return;
        if (_enemy.IsSpoilMode) return;

        _gridManager.MoveToPlayer(_enemy.transform, _enemy, HandleCompleteMove);
        _prevTime = Time.time;
    }

    private void HandleCompleteMove()
    {
        var grid = _gridManager.GetGrid(_gridManager.WorldToGrid(_enemy.transform.position));
        if (grid == null) return;
        if (grid.Type == InkType.None || grid.Type == InkType.Destroyed) return;
        grid.ClearModify();
    }
}
