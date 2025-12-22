using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class IntroManager : MonoBehaviour
{
    [SerializeField] Image team_img = null;
    [SerializeField] Image school_img = null;

    void Start()
    {
        StartCoroutine(ProcessIntroSequence(1.5f));
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

        SceneManager.LoadScene(SceneName.Lobby);
    }

    private Color SetAlpha(Color color, float alpha)
    {
        return new Color(color.r, color.g, color.b, alpha);
    }
}