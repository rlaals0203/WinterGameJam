using UnityEngine;

namespace Code.Entities
{
    public class PlayerIdleState : PlayerCanAttackState
    {
        public PlayerIdleState(Entity entity, int animationHash) : base(entity, animationHash)
        {   
        }

        public override void Update()
        {
            base.Update();
            
            if (_movement.TryMove())
                _player.ChangeState("MOVE");
        }
    }
}