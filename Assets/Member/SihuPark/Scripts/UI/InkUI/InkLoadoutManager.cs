using UnityEngine;
using System.Collections.Generic;
using Code.Core;
using AYellowpaper.SerializedCollections;


public class InkLoadoutManager : MonoBehaviour
{
    public static InkLoadoutManager Instance;

    public List<InkType> savedLoadout = new List<InkType>();
    public SerializedDictionary<InkType, int> savedUsedAmount = new SerializedDictionary<InkType, int>();
    public SerializedDictionary<InkType, int> savedRemainingAmount = new SerializedDictionary<InkType, int>();

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else Destroy(gameObject);
    }

    public void SaveData(List<InkType> loadout, Dictionary<InkType, int> usedAmount, Dictionary<InkType, int> remainingAmount)
    {
        savedLoadout.Clear();
        savedLoadout.AddRange(loadout);

        savedUsedAmount.Clear();
        foreach (var pair in usedAmount)
        {
            savedUsedAmount.Add(pair.Key, pair.Value);
        }

        savedRemainingAmount.Clear();
        foreach (var pair in remainingAmount)
        {
            savedRemainingAmount.Add(pair.Key, pair.Value);
        }
    }
}