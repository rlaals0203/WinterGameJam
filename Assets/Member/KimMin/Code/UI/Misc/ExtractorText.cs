using System;
using Code.Core;
using KimMin.Core;
using KimMin.Events;
using TMPro;
using UnityEngine;

namespace KimMin.UI.Misc
{
    public class ExtractorText : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI text;
        
        private void Awake()
        {
            if(GameManager.Instance.isCombatMode) gameObject.SetActive(false);
            GameEventBus.AddListener<ChangeExtractorEvent>(HandleChangeExtractor);
            text.text = $"우클릭을 하여 추출기를 설치하세요 (0/10)";
        }

        private void HandleChangeExtractor(ChangeExtractorEvent evt)
        {
            text.text = $"우클릭을 하여 추출기를 설치하세요 ({evt.count}/10)";
        }
    }
}