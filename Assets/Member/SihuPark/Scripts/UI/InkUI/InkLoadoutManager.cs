using UnityEngine;
using System.Collections.Generic;
using Code.Core;

public class InkLoadoutManager : MonoBehaviour
{
    public static InkLoadoutManager Instance;

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