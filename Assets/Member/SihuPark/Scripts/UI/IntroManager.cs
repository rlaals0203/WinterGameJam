using System;
using EasyTransition;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class IntroManager : MonoBehaviour
{
    [SerializeField] Image team_img = null;
    [SerializeField] Image school_img = null;

    [SerializeField] private TransitionSettings paintEffect;

    void Start()
    {
        StartCoroutine(ProcessIntroSequence(1.5f));
    }

    private void Update()
    {
        if (Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            TransitionManager.Instance().Transition(SceneName.Lobby, paintEffect, 0);
        }
    }

    public IEnumerator ProcessIntroSequence(float fadeDuration)
    {

        team_img.color = SetAlpha(team_img.color, 0);
        school_img.color = SetAlpha(school_img.color, 0);


        while (school_img.color.a < 1.0f)
        {
            float newAlpha = school_img.color.a + (Time.deltaTime / fadeDuration);
            school_img.color = SetAlpha(school_img.color, newAlpha);
            yield return null;
        }
        school_img.color = SetAlpha(school_img.color, 1);

        yield return new WaitForSeconds(1.0f);

        while (school_img.color.a > 0.0f)
        {
            float newAlpha = school_img.color.a - (Time.deltaTime / fadeDuration);
            school_img.color = SetAlpha(school_img.color, newAlpha);
            yield return null;
        }
        school_img.color = SetAlpha(school_img.color, 0);

        while (team_img.color.a < 1.0f)
        {
            float newAlpha = team_img.color.a + (Time.deltaTime / fadeDuration);
            team_img.color = SetAlpha(team_img.color, newAlpha);
            yield return null;
        }
        team_img.color = SetAlpha(team_img.color, 1);

        yield return new WaitForSeconds(1.0f);
        while (team_img.color.a > 0.0f)
        {
            float newAlpha = team_img.color.a - (Time.deltaTime / fadeDuration);
            team_img.color = SetAlpha(team_img.color, newAlpha);
            yield return null;
        }
        team_img.color = SetAlpha(team_img.color, 0);

        TransitionManager.Instance().Transition(SceneName.Lobby, paintEffect, 0);
    }

    private Color SetAlpha(Color color, float alpha)
    {
        return new Color(color.r, color.g, color.b, alpha);
    }
}