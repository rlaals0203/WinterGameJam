using System;
using System.Collections.Generic;
using KimMin.Dependencies;
using KimMin.UI.Core;
using KimMin.UI.Tooltip;
using UnityEngine;

namespace KimMin.UI.Controller
{
    
    [Provide]
    public class TooltipController : MonoBehaviour, IDependencyProvider
    {
        [SerializeField] private List<BaseTooltip> tooltipTypes;
        [SerializeField] private Transform root;
        
        private Dictionary<Type, BaseTooltip> _tooltipMap = new();
        private Dictionary<Type, BaseTooltip> _tooltipCache = new();
        private BaseTooltip _currentTooltip;
        
        private bool _isFix;
        private bool isHover;
        
        RectTransform RootRect => root.transform as RectTransform;

        private void Awake()
        {
            MappingTooltip();
        }

        private void MappingTooltip()
        {
            foreach (var tooltip in tooltipTypes)
            {
                if(tooltip == null) continue;
                var type = tooltip.DataType;

                _tooltipMap.TryAdd(type, tooltip);
            }
        }

        public void BindEnterTooltip<TData>(GameObject go, Func<TData> dataCallback, bool isFix = false, RectTransform parentRect = null)
        {
            UIEventHandler evt = UIUtility.GetOrAddComponent<UIEventHandler>(go);
            
            evt.BindUIEvent(go, _ => {
                    var data = dataCallback.Invoke();
                    if (data == null) return;
                    
                    ShowTooltip(data.GetType(), data, isFix, parentRect);
                },
                EUIEvent.PointerEnter);
        }

        public void BindExitTooltip(GameObject go)
        {
            UIEventHandler evt = UIUtility.GetOrAddComponent<UIEventHandler>(go);
            evt.BindUIEvent(go, (e) => HideTooltip() , EUIEvent.PointerExit);
        }

        private void Update()
        {
            if (_currentTooltip != null && !_isFix)
            {
                SetTooltipPosition();
            }
        }

        private void SetTooltipPosition()
        {
            Vector2 mousePos = Input.mousePosition;
            RectTransform rect = _currentTooltip.RectTransform;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(RootRect, 
                mousePos, null, out var localPoint);

            rect.anchoredPosition = localPoint;
            SetPivot(rect, mousePos);
            AddOffset(rect, 5f);
        }

        private void ShowTooltip(Type type, object data,
            bool isFix = false, RectTransform parentRect = null)
        {
            if (!_tooltipCache.TryGetValue(type, out var tooltip))
            {
                if (!_tooltipMap.TryGetValue(type, out var prefab) || prefab == null) {
                    Debug.LogError($"{type}Type's tooltip is missing.");
                    return;
                }

                tooltip = Instantiate(_tooltipMap[type], root);
                _tooltipCache[type] = tooltip;
            }

            HideTooltip();
            tooltip.Show(data);
            _currentTooltip = tooltip;
            _isFix = isFix;
            
            if (_isFix && parentRect != null)
            {
                _currentTooltip.RectTransform.anchoredPosition = parentRect.anchoredPosition;
            }
        }

        private void HideTooltip()
        {
            if (_currentTooltip == null) return;
            _currentTooltip.Hide();
            _currentTooltip = null;
        }
        
        private void SetPivot(RectTransform rect, Vector3 mousePos)
        {
            float mouseX = mousePos.x;
            float mouseY = mousePos.y;
            float centerX = Screen.width / 2f;
            float centerY = Screen.height / 2f;
            
            if (mouseX > centerX && mouseY > centerY)
                rect.pivot = new Vector2(1, 1);
            else if (mouseX < centerX && mouseY > centerY)
                rect.pivot = new Vector2(0, 1);
            else if (mouseX < centerX && mouseY < centerY)
                rect.pivot = new Vector2(0, 0);
            else if (mouseX > centerX && mouseY < centerY)
                rect.pivot = new Vector2(1, 0);
        }
        
        private void AddOffset(RectTransform rect, float offset)
        {
            Vector2 pivot = rect.pivot;
            Vector2 target = Vector2.zero;

            if (pivot.x > 0.5f) target.x -= offset;
            else target.x += offset;
            if (pivot.y > 0.5f) target.y -= offset;
            else target.y += offset;

            rect.anchoredPosition += target;
        }
    }
}