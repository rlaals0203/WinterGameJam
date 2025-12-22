using DG.Tweening;
using UnityEngine;

public static class AttackVisualHelper
{
    public static void ExecuteFullAttack(Transform owner, float range, float angle, System.Action onAttackHit)
    {
        GameObject indicator = CreateIndicator(owner, range, angle);
        Renderer rd = indicator.GetComponentInChildren<Renderer>();

        rd.material.color = new Color(1, 0, 0, 0);

        rd.material.DOFade(1f, 0.5f).OnComplete(() =>
        {
            Object.Destroy(indicator);

            GameObject weapon = CreateSwingWeapon(owner, angle - 45f, range);
            weapon.transform.DORotate(new Vector3(0, 0, angle + 45f), 0.2f)
                .SetEase(Ease.OutQuart)
                .OnComplete(() => Object.Destroy(weapon));

            onAttackHit?.Invoke();
        });
    }

    public static GameObject CreateIndicator(Transform owner, float range, float angle)
    {
        GameObject pivot = new GameObject("IndicatorPivot");
        pivot.transform.SetParent(owner);
        pivot.transform.localPosition = Vector3.zero;
        pivot.transform.localRotation = Quaternion.Euler(0, 0, angle);

        GameObject visual = GameObject.CreatePrimitive(PrimitiveType.Cube);
        Object.Destroy(visual.GetComponent<BoxCollider>());
        visual.transform.SetParent(pivot.transform);

        visual.transform.localScale = new Vector3(range, 1f, 0.01f);
        visual.transform.localPosition = new Vector3(range / 2f, 0, 0);

        return pivot;
    }

    public static GameObject CreateSwingWeapon(Transform owner, float startAngle, float range)
    {
        GameObject pivot = new GameObject("SwingPivot");
        pivot.transform.SetParent(owner);
        pivot.transform.localPosition = Vector3.zero;
        pivot.transform.localRotation = Quaternion.Euler(0, 0, startAngle);

        GameObject visual = GameObject.CreatePrimitive(PrimitiveType.Cube);
        Object.Destroy(visual.GetComponent<BoxCollider>());
        visual.transform.SetParent(pivot.transform);

        float weaponWidth = 0.1f;
        visual.transform.localScale = new Vector3(range * 2f, weaponWidth, 0.1f);
        visual.transform.localPosition = Vector3.zero;

        visual.GetComponent<Renderer>().material.color = Color.white;

        return pivot;
    }


    public static bool CheckHit(Transform owner, Transform target, Vector2 attackDir, float range)
    {
        float distance = Vector2.Distance(owner.position, target.position);
        if (distance > range + 0.1f) return false;

        Vector2 toTarget = ((Vector2)target.position - (Vector2)owner.position).normalized;
        float dot = Vector2.Dot(attackDir.normalized, toTarget);

        return dot > 0.7f;
    }
}