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
    [SerializeField] private TransitionSettings GameStartEffect;

    private int selectedIndex = -1;// ���� ���õ� ĭ ��ȣ
    private InkType[] slotsData; // �� ĭ�� ��� ��ũ ����

    private void Start()
    {
        slotsData = new InkType[slotButtons.Length];

        for (int i = 0; i < slotButtons.Length; i++)
        {
            int index = i;

            // ��ư Ŭ�� �̺�Ʈ ����
            slotButtons[i].onClick.RemoveAllListeners();
            slotButtons[i].onClick.AddListener(() => OnSlotClicked(index));

            // �ʱ�ȭ
            slotButtons[i].image.color = Color.gray;
            slotsData[i] = InkType.None;
        }

        if (highlightObj != null) highlightObj.gameObject.SetActive(false);
    }

    private void OnSlotClicked(int index)
    {
        selectedIndex = index;

        // ���̶���Ʈ �̵� �� ȸ��
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

        // ��ũ ������ ����
        if (InkStorage.Instance == null && !InkStorage.Instance.HasInk(type)) return;

        // ������ ���� �� ���� ����
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
