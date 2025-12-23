using Code.Core;
using EasyTransition;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InkSelectUI : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private Button[] slotButtons;
    [SerializeField] private Transform highlightObj;
    [SerializeField] private InkStorage inkStorage;
    [SerializeField] private TransitionSettings GameStartEffect;

    private int selectedIndex = -1;// 현재 선택된 칸 번호
    private InkType[] slotsData; // 각 칸에 담긴 잉크 정보

    private void Start()
    {
        slotsData = new InkType[slotButtons.Length];

        for (int i = 0; i < slotButtons.Length; i++)
        {
            int index = i;

            // 버튼 클릭 이벤트 연결
            slotButtons[i].onClick.RemoveAllListeners();
            slotButtons[i].onClick.AddListener(() => OnSlotClicked(index));

            // 초기화
            slotButtons[i].image.color = Color.gray;
            slotsData[i] = InkType.None;
        }

        if (highlightObj != null) highlightObj.gameObject.SetActive(false);
    }

    private void OnSlotClicked(int index)
    {
        selectedIndex = index;

        // 하이라이트 이동 및 회전
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

        InkType type = (InkType)inkTypeInt;

        // 잉크 없으면 무시
        if (inkStorage != null && !inkStorage.HasInk(type)) return;

        // 데이터 저장 및 색상 변경
        slotsData[selectedIndex] = type;
        slotButtons[selectedIndex].image.color = GetColorByType(type);
    }

    public void OnClickGameStart()
    {
        List<InkType> finalLoadout = new List<InkType>();

        foreach (var ink in slotsData)
        {
            if (ink != InkType.None) finalLoadout.Add(ink);
        }

        if (finalLoadout.Count > 0 && InkLoadoutManager.Instance != null)
        {
            InkLoadoutManager.Instance.SaveSelectedInks(finalLoadout);
            TransitionManager.Instance().Transition(SceneName.TestGame, GameStartEffect, 0);
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
