using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class ChangeReadyPage : MonoBehaviour
{
    [Header("Page")]
    [SerializeField] private RectTransform shop_page;
    [SerializeField] private RectTransform color_page;

    [Header("Movement Settings")]
    [SerializeField] private Transform offScreenTarget;
    [SerializeField] private float tiltAngle = 15f;
    [SerializeField] private float duration = 0.5f;

    [Header("Btn")]
    [SerializeField] private Button shop_swap_btn;
    [SerializeField] private Button color_swap_btn;

    private Vector2 _centerPos;
    private bool _isMainActive = true;

    private void Awake()
    {
        _centerPos = shop_page.anchoredPosition;

        // 시작 시 컬러 페이지를 타겟 오브젝트의 위치로 이동
        if (offScreenTarget != null)
        {
            color_page.anchoredPosition = offScreenTarget.localPosition;
        }

        color_page.localRotation = Quaternion.Euler(0, 0, tiltAngle);

        if (shop_swap_btn != null) shop_swap_btn.onClick.AddListener(SwapPages);
        if (color_swap_btn != null) color_swap_btn.onClick.AddListener(SwapPages);
    }

    public void SwapPages()
    {

        if (_isMainActive)
        {
            MoveOut(shop_page);
            MoveIn(color_page);
        }
        else
        {
            MoveOut(color_page);
            MoveIn(shop_page);
        }

        _isMainActive = !_isMainActive;
    }

    private void MoveIn(RectTransform target)
    {
        target.DOKill();
        target.DOAnchorPos(_centerPos, duration).SetEase(Ease.OutBack);
        target.DORotate(Vector3.zero, duration).SetEase(Ease.OutBack);
    }

    private void MoveOut(RectTransform target)
    {
        target.DOKill();
        target.DOAnchorPos(offScreenTarget.localPosition, duration).SetEase(Ease.InBack);
        target.DORotate(new Vector3(0, 0, tiltAngle), duration).SetEase(Ease.InBack);
    }
}