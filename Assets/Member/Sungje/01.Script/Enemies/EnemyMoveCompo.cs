using Code.Combat;
using Code.Core;
using Code.Entities;
using Code.Misc;
using KimMin.Dependencies;
using UnityEngine;

public class EnemyMoveCompo : MonoBehaviour, IEntityComponent
{
    private float duration = 2f;
    private float _prevTime = 0f;
    private Enemy _enemy;

    [Inject] private GridManager _gridManager;
    
    public void Initialize(Entity entity)
    {
        _enemy = entity as Enemy;
        duration = _enemy.EnemyDataSO.moveSpeed;
    }

    private void Update()
    {
        ProcessMove();
    }

    public void ProcessMove()
    {
        if (Time.time - _prevTime > duration && !_enemy.IsSpoilMode)
        {
            _gridManager.MoveToPlayer(_enemy.transform, _enemy, HandleCompleteMove);
            _prevTime = Time.time;
        }
    }

    private void HandleCompleteMove()
    {
        var grid = _gridManager.GetGrid(_gridManager.WorldToGrid(_enemy.transform.position));
        if (grid.Type == InkType.None || grid.Type == InkType.Destroyed) return;
        grid.ClearModify();
    }
}