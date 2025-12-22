using Code.Entities;
using Code.Misc;
using DG.Tweening;
using KimMin.Dependencies;
using UnityEngine;

namespace Code.Core
{
    [Provide]
    public class GridManager : MonoBehaviour, IDependencyProvider
    {
        [SerializeField] private int row;
        [SerializeField] private int col;
        [SerializeField] private Grid grid;
        [SerializeField] private GridObject gridPrefab;
        [SerializeField] private Vector2 gridOffset;
        
        private GridObject[,] _gridData = new GridObject[100, 100];
        [Inject] private Player _player;
        
        float CellSize => grid.cellSize.x;

        private void Awake()
        {
            Vector2 offset = new Vector2(row * CellSize, col * CellSize) / 2;
            for (int y = 0; y < col; y++)
            {
                for (int x = 0; x < row; x++)
                {
                    GridObject grid = Instantiate(gridPrefab);
                    Vector2 pos = new Vector2(x * CellSize, y * CellSize) - offset + gridOffset;
                    grid.transform.position = pos;
                    _gridData[x, y] = grid;
                }
            }
        }

        public GridObject GetGridObjectFromPosition(Vector3 position)
        {
            Vector3Int gridPos = Vector3Int.RoundToInt(position);
            return _gridData[gridPos.x, gridPos.y];
        }
        
        public Vector3Int WorldToGrid(Vector3 worldPos)
        {
            return grid.WorldToCell(worldPos);
        }
        
        public Vector3Int MoveByGrid(Vector3Int from, Vector3Int to)
        {
            Vector3Int delta = to - from;

            if (Mathf.Abs(delta.x) > Mathf.Abs(delta.y))
                return new Vector3Int((int)Mathf.Sign(delta.x), 0, 0);
            else
                return new Vector3Int(0, (int)Mathf.Sign(delta.y), 0);
        }

        public void MoveToPlayer(Transform target)
        {
            Vector3Int targetCell = grid.WorldToCell(target.position);
            Vector3Int playerCell = grid.WorldToCell(_player.transform.position);
            Vector3Int step = MoveByGrid(targetCell, playerCell);
            Vector3 worldPos = grid.CellToWorld(targetCell + step) + grid.cellSize / 2f;
            target.DOMove(worldPos, 0.1f);
        }
    }
}