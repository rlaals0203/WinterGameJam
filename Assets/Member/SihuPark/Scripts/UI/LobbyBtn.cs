using EasyTransition;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LobbyBtn : MonoBehaviour
{
    [Header("Scene Change Effect")]
    [SerializeField] private TransitionSettings paintEffect;

    [Header("Button")]
    [SerializeField] private Button game_start_btn;
    [SerializeField] private Button game_exit_btn;

    private string TestScene = "TestGame"; //Scene Test

    private void Awake()
    {
        if(game_start_btn != null) game_start_btn.onClick.AddListener(GoToGameScene);

        if (game_exit_btn != null) game_exit_btn.onClick.AddListener(ExitGame);
    }

    private void GoToGameScene()
    {
        TransitionManager.Instance().Transition(TestScene, paintEffect, 0);
    }

    private void ExitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
                Application.Quit();
#endif
    }

}
