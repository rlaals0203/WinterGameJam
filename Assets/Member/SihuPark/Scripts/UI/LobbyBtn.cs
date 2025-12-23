using Code.Core;
using EasyTransition;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class LobbyBtn : MonoBehaviour
{
    [SerializeField] private TransitionSettings paintEffect;
    [SerializeField] private Button game_start_btn;
    [SerializeField] private Button game_exit_btn;
    [SerializeField] private CanvasGroup pressKeyCanvasGroup;

    private bool isTransitioning = false;

    private void Awake()
    {
        if (game_start_btn) game_start_btn.onClick.AddListener(() => StartCoroutine(OnGameStart()));
        if (game_exit_btn) game_exit_btn.onClick.AddListener(ExitGame);
    }

    private void Update()
    {
        if (isTransitioning) return;

        if (pressKeyCanvasGroup)
            pressKeyCanvasGroup.alpha = Mathf.PingPong(Time.time * 2f, 1f) + 0.3f;

        if (Input.anyKeyDown)
        {
            if (!Input.GetMouseButtonDown(0) && !Input.GetMouseButtonDown(1) && !Input.GetMouseButtonDown(2) && !Input.GetKeyDown(KeyCode.Escape))
            {
                StartCoroutine(OnGameStart());
            }
        }
    }

    private IEnumerator OnGameStart()
    {
        isTransitioning = true;

        for (int i = 0; i < 2; i++)
        {
            pressKeyCanvasGroup.alpha = 1f;
            yield return new WaitForSeconds(0.05f);
            pressKeyCanvasGroup.alpha = 0f;
            yield return new WaitForSeconds(0.05f);
        }
        pressKeyCanvasGroup.alpha = 1f;

        yield return new WaitForSeconds(0.1f);

        TransitionManager.Instance().Transition(SceneName.Loading, paintEffect, 0);
        if (GameManager.Instance) GameManager.Instance.isCombatMode = false;
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