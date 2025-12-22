using UnityEngine;

public enum SupplyType
{
    Palette,
    Brush,
    Extractor
}

[CreateAssetMenu(fileName = "New Supply", menuName = "Shop/Supply Item", order = 1)]
public class SupplySO : ScriptableObject
{
    [Header("Basic Info")]
    public string SupplyName;
    public int SupplyID;
    public SupplyType supplyType;

    [Header("Display")]
    public Sprite icon;
    [TextArea(3, 5)]
    public string description;

    [Header("Economy")]
    public int price;
}