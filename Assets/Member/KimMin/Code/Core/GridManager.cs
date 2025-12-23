using System;
using System.Collections.Generic;
using Code.Entities;
using Code.Misc;
using DG.Tweening;
using KimMin.Dependencies;
using UnityEngine;

namespace Code.Core
{
    public enum InkType
    {
        Red,
        Yellow,
        Green,
        Blue,
        Black,
        White,
        None
    }
    
    [Provide]
    public class GridManager : MonoSingleton<GridManager>, IDependencyProvider
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
                    GridObject gridObj = Instantiate(gridPrefab, transform);
                    Vector3Int cell = new Vector3Int(x, y, 0);
                    Vector3 worldPos = grid.CellToWorld(cell) + grid.cellSize / 2f + (Vector3)gridOffset;

                    gridObj.transform.position = worldPos;
                    gridObj.Area = GetAreaIndex(cell);;

                    _gridData[x, y] = gridObj;
                }
            }
        }
        
        private int GetAreaIndex(Vector3Int cell)
        {
            int areaWidth = row / 3;
            int areaHeight = col / 3;
            int areaX = Mathf.Clamp(cell.x / areaWidth, 0, 2);
            int areaY = Mathf.Clamp(cell.y / areaHeight, 0, 2);
            int invertedY = 2 - areaY;
            
            return invertedY * 3 + areaX + 1;
        }
        
        public bool IsValidCell(Vector3Int cell)
            => cell.x >= 0 && cell.x < row && cell.y >= 0 && cell.y < col;

        public GridObject GetGrid(Vector3Int cell)
        {
            if (!IsValidCell(cell)) return null;
            return _gridData[cell.x, cell.y];
        }
        
        public Vector3Int WorldToGrid(Vector3 worldPos)
        {
            return grid.WorldToCell(worldPos);
        }
        
        public GridObject GetCellInDirection(Vector3Int origin, Vector3 dir, int distance)
        {
            Vector3Int target = origin + ToVectorInt(dir) * distance;
            return GetGrid(target);
        }
        
        public Vector3Int MoveByGrid(Vector3Int from, Vector3Int to)
        {
            Vector3Int delta = to - from;

            if (Mathf.Abs(delta.x) > Mathf.Abs(delta.y))
                return new Vector3Int((int)Mathf.Sign(delta.x), 0, 0);
            
            return new Vector3Int(0, (int)Mathf.Sign(delta.y), 0);
        }
        
        public List<GridObject> GetForwardGrid(Vector3 origin, Vector3 dir, int length, int width)
        {
            List<GridObject> result = new();
            Vector3Int originCell = grid.WorldToCell(origin);
            Vector3Int forward = ToVectorInt(dir);  
            Vector3Int side = new Vector3Int(forward.y, -forward.x, 0);
            
            for (int l = 1; l <= length; l++)
            {
                for (int w = -width; w <= width; w++)
                {
                    Vector3Int cell = originCell + forward * l + side * w;
                    if (!IsValidCell(cell)) continue;
                    result.Add(GetGrid(cell));
                }
            }

            return result;
        }
        
        public Vector3Int ToVectorInt(Vector3 dir)
        {
            if (Mathf.Abs(dir.x) > Mathf.Abs(dir.y))
                return new Vector3Int((int)Mathf.Sign(dir.x), 0, 0);

            return new Vector3Int(0, (int)Mathf.Sign(dir.y), 0);
        }
        
        public List<GridObject> GetCellsInRadius(Vector3Int center, int radius)
        {
            List<GridObject> result = new();

            for (int y = -radius; y <= radius; y++)
            {
                for (int x = -radius; x <= radius; x++)
                {
                    Vector3Int cell = center + new Vector3Int(x, y, 0);
                    if (!IsValidCell(cell)) continue;

                    result.Add(_gridData[cell.x, cell.y]);
                }
            }

            return result;
        }

        public void MoveToPlayer(Transform target, Action callback = null)
        {
            Vector3Int targetCell = grid.WorldToCell(target.position);
            Vector3Int playerCell = grid.WorldToCell(_player.transform.position);
    
            Vector3Int step = MoveByGrid(targetCell, playerCell);
            Vector3Int nextCell = targetCell + step;
            Vector3 worldPos = grid.CellToWorld(nextCell) + grid.cellSize / 2f + (Vector3)gridOffset;

            target.DOMove(worldPos, 0.1f).OnComplete(() =>
            {
                if (callback != null) callback();
            });
        }
        
    }
}