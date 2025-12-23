using System;
using Code.Core;
using DG.Tweening;
using KimMin.Core;
using KimMin.Events;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Code.UI.Ink
{
    public class PlayerInkUI : MonoBehaviour
    {
        [SerializeField] private InkItemUI inkItemUI;
        
        private InkItemUI[] _inkItemUI = new InkItemUI[2];
        private Tween _frontTween;
        private Tween _backTween;
        
        private void Awake()
        {
            _inkItemUI[0] = Instantiate(inkItemUI, transform);
            _inkItemUI[1] = Instantiate(inkItemUI, transform);
            
            _inkItemUI[0].Rect.anchoredPosition = new Vector2(0, 0);
            _inkItemUI[1].Rect.anchoredPosition = new Vector2(300, 0);
            
            GameEventBus.AddListener<ChangeInkEvent>(HandleChangeInk);
        }

        private void OnDestroy()
        {
            GameEventBus.RemoveListener<ChangeInkEvent>(HandleChangeInk);
        }

        private void HandleChangeInk(ChangeInkEvent evt)
        {
            ChangeInk(evt.inkType);
        }

        private void ChangeInk(InkType inkType)
        {
            _frontTween?.Kill();
            _backTween?.Kill();
            
            _inkItemUI[1].SetupInk(inkType);
            _inkItemUI[1].Rect.anchoredPosition = new Vector2(300, 0);
            
            _inkItemUI[0].Rect.anchoredPosition = new Vector2(0, 0);
            
            _backTween = _inkItemUI[1].Rect.DOAnchorPosX(0f, 0.8f)
                .SetEase(Ease.OutBack);
            _frontTween  = _inkItemUI[0].Rect.DOAnchorPosX(-300f, 0.8f)
                .SetEase(Ease.OutBack).OnComplete(() => { _inkItemUI[0].SetupInk(inkType); });
        }
    }
}