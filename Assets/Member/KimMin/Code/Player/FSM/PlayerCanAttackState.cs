using UnityEngine;

namespace Code.Entities
{
    public class PlayerCanAttackState : PlayerState
    {
        public PlayerCanAttackState(Entity entity, int animationHash) : base(entity, animationHash)
        {
        }
        
        public override void Enter()
        {
            base.Enter();
            _player.PlayerInput.OnLeftClickPressed += HandleAttackPressed;
        }

        public override void Exit()
        {
            base.Exit();
            _player.PlayerInput.OnLeftClickPressed -= HandleAttackPressed;
        }

        private void HandleAttackPressed()
        {
            _player.ChangeState("ATTACK");
        }
    }
}