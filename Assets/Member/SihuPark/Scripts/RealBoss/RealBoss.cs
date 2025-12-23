using System.Collections;
using UnityEngine;
using Code.Entities;

public class RealBoss : Enemy
{
    [Header("Attack")]
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private float attackCycleTime = 3f;
    [SerializeField] private Transform firePoint;

    private EnemyMoveCompo _moveCompo;

    private void Start()
    {
        _moveCompo = GetComponent<EnemyMoveCompo>();

        if (_moveCompo != null)
        {
            _moveCompo.Initialize(this);
        }

        StartCoroutine(BossPatternRoutine());
    }

    private IEnumerator BossPatternRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(attackCycleTime);
            int pattern = Random.Range(0, 2);

            switch (pattern)
            {
                case 0:
                    yield return StartCoroutine(Pattern_Shotgun());
                    break;
                case 1:
                    yield return StartCoroutine(Pattern_Circle());
                    break;
            }
        }
    }

    private IEnumerator Pattern_Shotgun()
    {
        if (Player == null) yield break;

        Vector2 dir = (Player.transform.position - transform.position).normalized;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;

        for (int i = -1; i <= 1; i++)
        {
            SpawnBullet(angle + (i * 15f));
        }

        yield return null;
    }

    private IEnumerator Pattern_Circle()
    {
        for (int i = 0; i < 360; i += 30)
        {
            SpawnBullet((float)i);
        }

        yield return new WaitForSeconds(1f);
    }

    private void SpawnBullet(float angle)
    {
        if (bulletPrefab == null || firePoint == null) return;

        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);

        BossBullet bulletScript = bullet.GetComponent<BossBullet>();
        if (bulletScript != null)
        {
            Vector2 dir = new Vector2(Mathf.Cos(angle * Mathf.Deg2Rad), Mathf.Sin(angle * Mathf.Deg2Rad));
            bulletScript.SetDirection(dir);
        }
    }
}