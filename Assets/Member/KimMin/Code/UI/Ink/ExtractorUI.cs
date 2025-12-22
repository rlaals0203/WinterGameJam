using Code.GameFlow;
using KimMin.UI.Bar;
using KimMin.UI.Core;
using UnityEngine;
using Utility = Code.Core.Utility;

namespace Code.UI
{
    public class ExtractorUI : MonoBehaviour, IUIElement<InkData[]>
    {
        [field: SerializeField] public Transform Root { get; private set; }
        [SerializeField] private IconTextBar iconTextBar;
        
        public void EnableFor(InkData[] element)
        {
            for (int i = 0; i < element.Length; i++)
            {
                var bar = Instantiate(iconTextBar, Root);
                bar.SetText($"{element[i].InkType} {element[i].Chance}%");
                bar.SetIconColor(Utility.GetGridColor(element[i].InkType));
            }
        }

        public void Disable() { }
    }
}