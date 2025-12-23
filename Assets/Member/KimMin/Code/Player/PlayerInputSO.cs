using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Code.Entities
{
    [CreateAssetMenu(fileName = "PlayerInput", menuName = "SO/PlayerInput", order = 0)]
    public class PlayerInputSO : ScriptableObject, Controls.IPlayerActions
    {
        public event Action OnMovePressed;
        public event Action OnDashPressed;
        public event Action OnRightClickPressed;
        public event Action OnLeftClickPressed;
        public Vector2 MovementKey;
        public Vector2 MousePosition;
        
        private Controls _controls;
        
        private void OnEnable()
        {
            if (_controls == null)
            {
                _controls = new Controls();
                _controls.Player.SetCallbacks(this);
            }
            _controls.Player.Enable();
        }

        private void OnDisable()
        {
            _controls.Player.Disable();
        }

        public void OnMove(InputAction.CallbackContext context)
        {
            if (context.performed)
                OnMovePressed?.Invoke();
            
            MovementKey = context.ReadValue<Vector2>();
        }

        public void OnPoint(InputAction.CallbackContext context)
        {
            MousePosition = context.ReadValue<Vector2>();
        }

        public void OnDash(InputAction.CallbackContext context)
        {
            if(context.performed)
                OnDashPressed?.Invoke();
        }

        public void OnRightClick(InputAction.CallbackContext context)
        {
            if(context.performed)
                OnRightClickPressed?.Invoke();
        }

        public void OnLeftClick(InputAction.CallbackContext context)
        {
            if(context.performed)
                OnLeftClickPressed?.Invoke();
        }
    }
}