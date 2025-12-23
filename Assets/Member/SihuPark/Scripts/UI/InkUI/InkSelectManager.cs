using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using Code.Core;
using UnityEngine.SceneManagement;

public class InkSelectManager : MonoBehaviour
{
    [SerializeField] private InkStorage inkStorage;
    [SerializeField] private Image[] slotImages;
    [SerializeField] private int maxSlots = 5;

    private List<InkType> currentSelection = new List<InkType>();

    private void Start() => UpdateSlotColors();
    public void OnClickInkButton(int inkTypeIndex)
    {
        InkType type = (InkType)inkTypeIndex;

        if (!inkStorage.HasInk(type)) return;

        if (currentSelection.Contains(type))
            currentSelection.Remove(type);
        else if (currentSelection.Count < maxSlots)
            currentSelection.Add(type);

        UpdateSlotColors();
    }

    private void UpdateSlotColors()
    {
        for (int i = 0; i < slotImages.Length; i++)
        {
            if (i < currentSelection.Count)
                slotImages[i].color = GetColorByType(currentSelection[i]);
            else
                slotImages[i].color = Color.gray;
        }
    }

    private Color GetColorByType(InkType type)
    {
        switch (type)
        {
            case InkType.Red: return Color.red;
            case InkType.Blue: return Color.blue;
            case InkType.Yellow: return Color.yellow;
            case InkType.Green: return Color.green;
            case InkType.White: return Color.white;
            case InkType.Black: return Color.black;
            default: return Color.white;
        }
    }

    public void OnClickStartBattle()
    {
        if (currentSelection.Count == 0) return;

        if (InkLoadoutManager.Instance != null)
            InkLoadoutManager.Instance.SaveSelectedInks(currentSelection);

        // SceneManager.LoadScene("BattleScene");
    }
}