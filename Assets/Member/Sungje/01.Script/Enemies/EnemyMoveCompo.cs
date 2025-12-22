using Code.Core;
using KimMin.Dependencies;
using UnityEngine;

public class EnemyMoveCompo : MonoBehaviour
{
    [SerializeField] private float duration = 2f;
    private float _prevTime = 0f;

    [Inject] private GridManager _gridManager;


    public void ProcessMove()
    {
        if (Time.time - _prevTime > duration)
        {
            _prevTime = Time.time;
            _gridManager.MoveToPlayer(transform);
        }
    }

    public void ResetTimer()
    {
        _prevTime = Time.time;
    }
}