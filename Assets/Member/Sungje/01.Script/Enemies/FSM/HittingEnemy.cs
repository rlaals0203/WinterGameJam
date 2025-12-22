using System;
using UnityEngine;

public class HittingEnemy : Enemy
{
    private bool _isSpawned;

    protected override void InitState()
    {
        foreach (EnemyStateType stateType in Enum.GetValues(typeof(EnemyStateType)))
        {
            try
            {
                string enumName = stateType.ToString();
                Type t = Type.GetType($"Hitting{enumName}State");

                if (t != null)
                {
                    EnemyState state = Activator.CreateInstance(t, new object[] { this }) as EnemyState;
                    StateEnum.Add(stateType, state);
                }
            }
            catch (Exception e)
            {
                Debug.LogError($"[StateError] {stateType} 초기화 중 오류: {e.Message}");
            }
        }
    }

    public void SetSpawn()
    {
        _isSpawned = true;
    }

    protected override void HandleDead()
    {
        if (_isSpawned)
        {
            TransitionState(EnemyStateType.Dead);
        }
    }
}