using UnityEngine;

[CreateAssetMenu(fileName = "Supply", menuName = "SO/Supply", order = 0)]
public class SupplySO : ScriptableObject
{
    [Header("Basic Info")]
    public string SupplyName;
    public int SupplyID;

    [Header("Display")]
    public Sprite icon; // 아이템 이미지
    [TextArea(3, 5)]
    public string description; // 아이템 설명

    [Header("Economy")]
    public int price;
}
