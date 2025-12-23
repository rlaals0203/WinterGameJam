using DG.Tweening;
using EasyTransition;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Loading : MonoBehaviour
{

    [SerializeField] private TMP_Text loading_txt;
    [SerializeField] private TransitionSettings changeEffect;

    [Header("tip")]
    [SerializeField] private TMP_Text tip_txt;
    [SerializeField] private string[] random_tip_list;


    private void OnEnable()
    {
        StartCoroutine(LoadingTextAnim());
        StartCoroutine(RandomTip());
    }

    private IEnumerator LoadingTextAnim()
    {
        string loadingText = "Loading";
        int dotCount = 0;

        while (dotCount < 3)
        {
            for (int i = 0; i <= 3; i++)
            {
                loading_txt.text = loadingText + new string('.', i);
                yield return new WaitForSeconds(0.3f);
            }

            loading_txt.text = loadingText;
            yield return new WaitForSeconds(0.5f);

            dotCount++;

        }

        TransitionManager.Instance().Transition(SceneName.Game, changeEffect, 0);

    }

    private IEnumerator RandomTip()
    {
        if (random_tip_list.Length == 0)
        {
            Debug.LogWarning("Tip list is null");
            yield break;
        }

        int tipCount = random_tip_list.Length;

        int randomIndex = Random.Range(0, tipCount);

        string randomTip = random_tip_list[randomIndex];

        tip_txt.text = "Tip : " + randomTip;

        yield return null;
    }
}
