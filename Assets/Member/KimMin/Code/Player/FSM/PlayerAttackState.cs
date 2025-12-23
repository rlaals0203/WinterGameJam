using UnityEngine;

namespace Code.Entities
{
    public class PlayerAttackState : PlayerState
    {
        private PlayerAttackCompo _attackCompo;


        public PlayerAttackState(Entity entity, int animationHash) : base(entity, animationHash)
        {
            _attackCompo = entity.GetCompo<PlayerAttackCompo>();
        }

        public override void Update()
        {
            base.Update();
            if(_isTriggerCall)
                _player.ChangeState("IDLE");
        }

        public override void Enter()
        {
            base.Enter();
            _movement.CanMove = false;
            _attackCompo.Attack();
        }

        public override void Exit()
        {
            base.Exit();
            _movement.CanMove = true;
        }
    }
}