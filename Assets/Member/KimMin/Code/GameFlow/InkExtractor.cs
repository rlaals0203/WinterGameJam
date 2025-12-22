using Code.Core;
using Code.UI;
using UnityEngine;

namespace Code.GameFlow
{
    public class InkExtractor : MonoBehaviour
    {
        [SerializeField] private ExtractorUI extractorUI;
        
        public void InitExtractor(InkData[] data)
        {
            Extract(data);
            var ui = Instantiate(extractorUI, transform.position, Quaternion.identity, transform);
            ui.transform.localScale = Vector3.zero;
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