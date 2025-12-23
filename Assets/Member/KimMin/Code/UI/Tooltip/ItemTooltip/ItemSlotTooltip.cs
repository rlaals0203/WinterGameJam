using TMPro;
using UnityEngine;
using Work.LKW.Code.Items.ItemInfo;

namespace MinLibrary.UI.Tooltip
{
    public abstract class ItemSlotTooltip<TData> : BaseTooltip<TData> where TData : ItemDataSO
    {
        [SerializeField] private TextMeshProUGUI titleText;
        [SerializeField] private TextMeshProUGUI descriptionText;
        [SerializeField] private TextMeshProUGUI itemTypeText;
        
        protected sealed override void Show(TData data)
        {
            titleText.text = data.itemName;
            descriptionText.text = data.description;
            //itemTypeText.text = data.itemType.ToString();

            ShowData(data);
        }

        public sealed override void Hide()
        {
            titleText.text = string.Empty;
            descriptionText.text = string.Empty;
            //itemTypeText.text = string.Empty;
            
        }
        
        protected abstract void ShowData(TData data);
        protected abstract void HideData(TData data);
    }
}