using System.Collections.Generic;
using UnityEngine;

namespace Code.GameFlow
{
    [CreateAssetMenu(fileName = "StageData", menuName = "SO/StageData", order = 0)]
    public class StageDataSO : ScriptableObject
    {
        public List<Enemy> enemyList;
        public float enemyDelay;
        public int enemyCount;
        public GameObject paint;
        public int row;
        public int column;
    }
}