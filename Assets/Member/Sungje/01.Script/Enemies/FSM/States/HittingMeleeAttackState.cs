//using Code.Entities;
//using UnityEngine;
//using DG.Tweening;
//using System.Collections;

//public class HittingMeleeAttackState : EnemyState
//{
//    private float _attackRange;
//    private float _lastAttackTime;

//    public HittingMeleeAttackState(Enemy enemy) : base(enemy)
//    {
//        _attackRange = data.attackRange;
//    }

//    public override void UpdateState()
//    {
//        base.UpdateState();
//        if (_player == null) return;

//        float distance = Vector3.Distance(_enemy.transform.position, _player.transform.position);

//        if (distance > _attackRange)
//        {
//            _enemy.TransitionState(EnemyStateType.Move);
//            return;
//        }

//        if (Time.time >= _lastAttackTime + data.attackCooldown)
//        {
//            _enemy.StartCoroutine(SwingSequence());
//        }
//    }

//    private IEnumerator SwingSequence()
//    {
//        _lastAttackTime = Time.time;

//        Vector2 dir = (_player.transform.position - _enemy.transform.position).normalized;
//        float targetAngle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;

//        GameObject indicator = AttackVisualHelper.CreateIndicator(_enemy.transform, _attackRange, targetAngle);
//        yield return new WaitForSeconds(0.4f);
//        Object.Destroy(indicator);

//        float startAngle = targetAngle - 60f;
//        float endAngle = targetAngle + 60f;
//        GameObject weaponPivot = AttackVisualHelper.CreateSwingWeapon(_enemy.transform, startAngle, _attackRange);

//        weaponPivot.transform.DORotate(new Vector3(0, 0, endAngle), 0.2f).SetEase(Ease.OutCubic);

//        yield return new WaitForSeconds(0.1f);

//        if (AttackVisualHelper.CheckHit(_enemy.transform, _player.transform, dir, _attackRange))
//        {
//            _enemy.ApplyDamage(Vector3.zero, Vector3.up);
//        }

//        yield return new WaitForSeconds(0.1f);
//        Object.Destroy(weaponPivot);
//    }
//}