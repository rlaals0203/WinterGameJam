using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace KimMin.UI.Bar
{
    public class IconTextBar : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI text;
        [SerializeField] private Image icon;

        public void SetText(string text)
        {
            this.text.text = text;
        }

        public void SetIcon(Sprite icon)
        {
            this.icon.sprite = icon;
        }

        public void SetIconColor(Color color)
        {
            this.icon.color = color;
        }

        public void SetTextAndIcon(string text, Sprite icon)
        {
            this.text.text = text;
            this.icon.sprite = icon;
        }
    }
}