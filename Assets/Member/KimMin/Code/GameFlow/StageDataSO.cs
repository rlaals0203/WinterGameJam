using UnityEngine;

namespace Code.GameFlow
{
    [CreateAssetMenu(fileName = "StageData", menuName = "SO/StageData", order = 0)]
    public class StageDataSO : ScriptableObject
    {
        public Sprite picture;
        public int row;
        public int column;
    }
}