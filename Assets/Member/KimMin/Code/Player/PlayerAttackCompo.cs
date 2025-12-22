using System;
using UnityEngine;

namespace Code.Entities
{
    public class PlayerAttackCompo : MonoBehaviour, IEntityComponent
    {
        [SerializeField] private GameObject arrowObject;
        
        private Player _player;
        private Vector3 _direction;
        
        public void Initialize(Entity entity)
        {
            _player = entity as Player;
        }

        private void Update()
        {
            SetArrow();
        }

        private void SetArrow()
        {
            Vector2 center = new Vector2(Screen.width / 2f, Screen.height / 2f);
            Vector2 local = _player.PlayerInput.MousePosition - center;

            if (local.y > local.x && local.y > -local.x) {
                _direction = new Vector3(0, 1);
                SetArrowTransform(_direction, Quaternion.Euler(0, 0, 0));
            }
            else if (local.y < local.x && local.y > -local.x) {
                _direction = new Vector3(1, 0);
                SetArrowTransform(_direction, Quaternion.Euler(0, 0, 270));
            }
            else if (local.y < local.x && local.y < -local.x) {
                _direction = new Vector3(0, -1);
                SetArrowTransform(_direction, Quaternion.Euler(0, 0, 180));
            }
            else {
                _direction = new Vector3(-1, 0);
                SetArrowTransform(_direction, Quaternion.Euler(0, 0, 90));
            }
        }

        private void SetArrowTransform(Vector3 direction, Quaternion rotation)
        {
            arrowObject.transform.position = _player.transform.position + direction;
            arrowObject.transform.rotation =  rotation;
        }
    }
}