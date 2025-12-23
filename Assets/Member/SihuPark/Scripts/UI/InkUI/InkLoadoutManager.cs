using UnityEngine;
using System.Collections.Generic;
using Code.Core;

public class InkLoadoutManager : MonoBehaviour
{
    public static InkLoadoutManager Instance;
    public List<InkType> savedInks = new List<InkType>(); // 이게 인게임으로 넘겨서 가져갈 데이터입니다. 이거 쓰시면 되요

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