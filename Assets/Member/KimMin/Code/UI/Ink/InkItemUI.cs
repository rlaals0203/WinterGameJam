using Code.Core;
using UnityEngine;
using UnityEngine.UI;

namespace Code.UI.Ink
{
    public class InkItemUI : MonoBehaviour
    {
        [SerializeField] private Image icon;
        
        public RectTransform Rect => transform as RectTransform;

        public void SetupInk(InkType inkType)
        {
            icon.color = Utility.GetGridColor(inkType);
        }
    }
}