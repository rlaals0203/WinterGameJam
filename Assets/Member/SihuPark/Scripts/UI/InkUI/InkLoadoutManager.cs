using UnityEngine;
using System.Collections.Generic;
using Code.Core;

public class InkLoadoutManager : MonoBehaviour
{
    public static InkLoadoutManager Instance;

    //전투 씬에서 가져다 쓸 데이터
    public List<InkType> savedInks = new List<InkType>();

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else Destroy(gameObject);
    }

    public void SaveSelectedInks(List<InkType> inks)
    {
        savedInks.Clear();
        savedInks.AddRange(inks);
    }
}