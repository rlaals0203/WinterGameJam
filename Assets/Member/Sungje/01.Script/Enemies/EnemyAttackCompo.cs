using UnityEngine;

public class EnemyAttackCompo : MonoBehaviour
{
    [SerializeField] private Transform batPivot;
    [SerializeField] private float swingSpeed = 720f;
    [SerializeField] private float swingAngle = 120f;

    private float _currentAngle;
    private bool _isSwinging;
    private Enemy _enemy;

    private void Awake()
    {
        _enemy = GetComponent<Enemy>();
        batPivot.gameObject.SetActive(false);
    }

    private void Update()
    {
        if (!_isSwinging) return;

        float delta = swingSpeed * Time.deltaTime;
        _currentAngle += delta;

        if (_currentAngle >= swingAngle)
        {
            EndAttack();
            return;
        }

        batPivot.localRotation = Quaternion.Euler(0, 0, -_currentAngle);
    }

    public void StartAttack()
    {
        _currentAngle = 0f;
        _isSwinging = true;
        batPivot.localRotation = Quaternion.identity;
        batPivot.gameObject.SetActive(true);
    }

    public void EndAttack()
    {
        _isSwinging = false;
        batPivot.gameObject.SetActive(false);
    }
}
