using Code.Entities;
using UnityEngine;

namespace Code.Combat
{
    public class HomingBullet : Bullet
    {
        [SerializeField] private float rotateSpeed = 720f;
        [SerializeField] private float homingDuration = 1.2f;

        private Transform _target;
        private float _timer;
        private bool _isHoming;

        public void Init(
            Vector3 position,
            Vector2 direction,
            float bulletSpeed,
            int damage,
            Player target,
            Entity entity
        )
        {
            base.Init(position, direction, bulletSpeed, damage, entity);
            _target = target.transform;
            _timer = 0f;
            _isHoming = true;
        }

        private void FixedUpdate()
        {
            if (!_isHoming || _target == null) return;

            _timer += Time.fixedDeltaTime;
            if (_timer >= homingDuration)
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
            _timer = 0f;
            _isHoming = false;
        }
    }
}
