using KimMin.Events;
using Unity.VisualScripting;
using UnityEditor.Build;
using UnityEngine;

public class DashAttackCompo : EnemyAttackCompo
{
    private readonly DashAttackEvent _dashAttackEvent = SpawnEvents.DashAttackEvent;
}
