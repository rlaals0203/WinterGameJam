using KimMin.Core;
using UnityEngine;

namespace KimMin.Events
{
    public static class EnemyEvents
    {
        public static EnemyDeadEvent EnemyDeadEvent = new();
    }
    
    public class EnemyDeadEvent : GameEvent { }
}