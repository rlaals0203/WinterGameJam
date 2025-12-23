using UnityEngine;

namespace Code.Core
{
    public static class Utility
    {
        public static Color GetGridColor(InkType inkType)
        {
            return inkType switch
            {
                InkType.Red => new Color32(200, 155, 155, 150),
                InkType.Yellow => new Color32(255, 250, 175, 125),
                InkType.Blue => new Color32(150, 175, 255, 125),
                InkType.Green => new Color32(175, 255, 175, 125),
                InkType.Black => new Color32(50, 50, 50, 175),
                InkType.Destroyed => new Color32(50, 50, 50, 175),
                InkType.None => new Color32(255, 255, 255, 30),
                _ => Color.white
            };
        }
    }
}