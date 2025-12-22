using UnityEditor;
using UnityEngine;
using UnityEngine.Serialization;

namespace KimMin.Items
{
    public enum Rarity
    {
        Common,
        Rare,
        Epic,
    }
    
    public abstract class ItemDataSO : ScriptableObject
    {
        [Header("Item Info")]
        public string itemId;
        public string itemName;
        public Sprite itemImage;
        public string description;
        
        [Header("Properties")]
        [Tooltip("희귀도")]
        public Rarity rarity;
        [Tooltip("돈 가치")]
        public int value;
        [Tooltip("아이템 슬롯에 쌓일 최대 스택")]
        public int maxStack;

#if UNITY_EDITOR
        protected virtual void OnValidate()
        {
            if (string.IsNullOrEmpty(itemId))
            {
                itemId = GUID.Generate().ToString();
                AssetDatabase.SaveAssets();
            }
        }
#endif
        
    }
}