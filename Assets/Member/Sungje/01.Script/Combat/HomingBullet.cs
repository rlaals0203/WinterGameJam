using Code.Entities;
using UnityEngine;

namespace Code.Combat
{
    public class HomingBullet : Bullet
    {
        [SerializeField] private float rotateSpeed = 720f;
        [SerializeField] private float homingDuration = 1.2f;

        private Transform _target;
        private float timer;
        private bool _isHoming;

        public void Init(
            Vector3 position,
            float bulletSpeed,
            int damage,
            Player target,
            Entity owner
        )
        {
            Vector2 dir = (target.Position - position).normalized;
            base.Init(position, dir, bulletSpeed, damage, owner);

            _target = target.transform;
            timer = 0f;
            _isHoming = true;
        }

        private void FixedUpdate()
        {
            if (!_isHoming || _target == null) return;

            timer += Time.fixedDeltaTime;
            if (timer >= homingDuration)
            {
                _isHoming = false;
                return;
            }

            Vector2 dir = ((Vector2)_target.position - _rb.position).normalized;
            float angle = Vector2.SignedAngle(transform.right, dir);
            float rotate = Mathf.Clamp(
                angle,
                -rotateSpeed * Time.fixedDeltaTime,
                rotateSpeed * Time.fixedDeltaTime
            );

            _rb.rotation += rotate;
            _rb.linearVelocity = (Vector2)transform.right * _speed;
        }

        public override void ResetItem()
        {
            base.ResetItem();
            _target = null;
            timer = 0f;
            _isHoming = false;
        }
    }
}
