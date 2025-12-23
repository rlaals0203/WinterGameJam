using UnityEngine;

public enum GameState
{
    Harvesting = 0,
    Shop,
    Game
}

namespace Code.Core
{
    public class GameManager : MonoSingleton<GameManager>
    {
        public GameState GameState { get; set; }
        public bool isCombatMode = true;
        public int currentStage = 0;
    }
}
