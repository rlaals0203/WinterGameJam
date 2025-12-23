using Code.Core;
using EasyTransition;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InkSelectUI : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private Button[] slotButtons;
    [SerializeField] private Transform highlightObj;
    [SerializeField] private TransitionSettings GameStartEffect;

    [Header("Warning UI")]
    [SerializeField] private GameObject notEnoughMsgObj;

    private int selectedIndex = -1;
    private InkType[] slotsData;
    private const int INK_PER_SLOT = 10;

    private void Start()
    {
        slotsData = new InkType[slotButtons.Length];

        for (int i = 0; i < slotButtons.Length; i++)
        {
            int index = i;
            slotButtons[i].onClick.RemoveAllListeners();
            slotButtons[i].onClick.AddListener(() => OnSlotClicked(index));
            slotButtons[i].image.color = Color.gray;
            slotsData[i] = InkType.None;
        }

        if (highlightObj != null) highlightObj.gameObject.SetActive(false);
        if (notEnoughMsgObj != null) notEnoughMsgObj.SetActive(false); 
    }

    private void OnSlotClicked(int index)
    {
        selectedIndex = index;
        if (highlightObj != null)
        {
            highlightObj.gameObject.SetActive(true);
            highlightObj.position = slotButtons[index].transform.position;
            highlightObj.rotation = slotButtons[index].transform.rotation;
        }
    }

    public void SetInk(int inkTypeInt)
    {
        if (selectedIndex == -1) return;

        InkType newType = (InkType)inkTypeInt;

        if (InkStorage.Instance == null) return;

        int currentUsedAmount = 0;
        foreach (var ink in slotsData)
        {
            if (ink == newType) currentUsedAmount += INK_PER_SLOT;
        }

        int myTotalInk = InkStorage.Instance.GetRemainInk(newType);

        // 잉크가 모자라면 경고 메시지 띄우기
        if (myTotalInk - currentUsedAmount < INK_PER_SLOT)
        {
            StopAllCoroutines();
            StartCoroutine(ShowWarningEffect());
            return;
        }

        slotsData[selectedIndex] = newType;
        slotButtons[selectedIndex].image.color = GetColorByType(newType);
    }

    private IEnumerator ShowWarningEffect()
    {
        if (notEnoughMsgObj != null)
        {
            notEnoughMsgObj.SetActive(true);
            yield return new WaitForSeconds(1.0f);
            notEnoughMsgObj.SetActive(false);
        }
    }

    public void OnClickGameStart()
    {
        List<InkType> finalLoadout = new List<InkType>();
        Dictionary<InkType, int> usedInkAmount = new Dictionary<InkType, int>();

        foreach (var ink in slotsData)
        {
            if (ink != InkType.None)
            {
                finalLoadout.Add(ink);
                if (!usedInkAmount.ContainsKey(ink)) usedInkAmount[ink] = 0;
                usedInkAmount[ink] += INK_PER_SLOT;
            }
        }

        if (finalLoadout.Count > 0 && InkLoadoutManager.Instance != null && InkStorage.Instance != null)
        {
            foreach (var pair in usedInkAmount)
            {
                InkStorage.Instance.ModifyInk(pair.Key, -pair.Value);
            }

            InkLoadoutManager.Instance.SaveData(
                finalLoadout,
                usedInkAmount,
                InkStorage.Instance.GetInkDict()
            );

            TransitionManager.Instance().Transition(SceneName.Game, GameStartEffect, 0);
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
            default: return Color.gray;
        }
    }
}