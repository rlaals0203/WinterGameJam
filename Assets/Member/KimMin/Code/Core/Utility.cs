using UnityEngine;

namespace Code.Core
{
    public static class Utility
    {
        public static Color GetGridColor(InkType inkType)
        {
            return inkType switch
            {
                InkType.Red => new Color32(255, 175, 175, 100),
                InkType.Yellow => new Color32(255, 255, 175, 100),
                InkType.Blue => new Color32(150, 175, 255, 100),
                InkType.Green => new Color32(175, 255, 175, 100),
                InkType.Black => new Color32(50, 50, 50, 100),
                InkType.None => new Color32(255, 255, 255, 50),
                _ => Color.white
            };
        }
    }
}