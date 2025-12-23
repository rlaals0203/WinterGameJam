using Code.Core;
using Code.Entities;
using Code.Misc;
using UnityEngine;

public class EnemyMoveCompo : MonoBehaviour, IEntityComponent
{
    private float duration = 2f;
    private float _prevTime;

    private Enemy _enemy;
    private SpriteRenderer _spriteRenderer;

    public void Initialize(Entity entity)
    {
        _enemy = entity as Enemy;

        if (_enemy == null)
            return;

        if (_enemy.EnemyDataSO != null)
            duration = _enemy.EnemyDataSO.moveSpeed;

        _spriteRenderer = _enemy.GetComponentInChildren<SpriteRenderer>(true);
    }

    private void Update()
    {
        if (_enemy == null) return;

        ProcessMove();
        HandleFlip();
    }

    public void ProcessMove()
    {
        if (GridManager.Instance == null) return;

        if (Time.time - _prevTime > duration && !_enemy.IsSpoilMode)
        {
            GridManager.Instance.MoveToPlayer(
                _enemy.transform,
                _enemy,
                HandleCompleteMove
            );

            _prevTime = Time.time;
        }
    }

    private void HandleFlip()
    {
        if (_spriteRenderer == null) return;

        var player = _enemy.Player;
        if (player == null) return;

        float dirX = player.transform.position.x - _enemy.transform.position.x;
        if (dirX == 0f) return;

        _spriteRenderer.flipX = dirX < 0f;
    }

    private void HandleCompleteMove()
    {
        if (GridManager.Instance == null) return;

        var grid = GridManager.Instance.GetGrid(
            GridManager.Instance.WorldToGrid(_enemy.transform.position)
        );

        if (grid == null) return;
        if (grid.Type == InkType.None || grid.Type == InkType.Destroyed) return;

        grid.ClearModify();
    }
}
