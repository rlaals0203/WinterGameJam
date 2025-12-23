using System.Collections.Generic;
using Code.Core;
using Code.UI;
using UnityEngine;
using UnityEngine.UI;

namespace Code.GameFlow
{
    public class InkExtractor : MonoBehaviour
    {
        public static readonly List<InkExtractor> All = new();
        
        [field: SerializeField] public ExtractorUI extractorUI { get; private set; }
        [field: SerializeField] public bool IsVisible { get;  set; }
        public ExtractorUI UI { get; private set; }
        
        public void InitExtractor(InkData[] data)
        {
            Extract(data);
            UI = Instantiate(extractorUI, transform.position, Quaternion.identity, transform);
            UI.EnableFor(data);
            UI.Root.localScale = Vector3.one * 0.6f;
            UI.Root.localPosition += Vector3.up * 0.8f;
            IsVisible = false;
        }
        
        private void OnEnable()
        {
            All.Add(this);
        }

        private void OnDisable()
        {
            All.Remove(this);
        }
        
        public InkType Extract(InkData[] data)
        {
            if (data == null || data.Length == 0)
                return InkType.None;

            int total = 0;
            for (int i = 0; i < data.Length; i++)
                total += data[i].Chance;

            int rand = UnityEngine.Random.Range(0, total);
            int acc = 0;

            for (int i = 0; i < data.Length; i++)
            {
                acc += data[i].Chance;
                if (rand < acc)
                    return data[i].InkType;
            }

            return InkType.None;
        }
    }
}