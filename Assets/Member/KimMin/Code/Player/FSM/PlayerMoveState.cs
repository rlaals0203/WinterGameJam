using UnityEngine;

namespace Code.Entities
{
    public class PlayerMoveState : PlayerCanAttackState
    {
        public PlayerMoveState(Entity entity, int animationHash) : base(entity, animationHash)
        {
        }
        
        public override void Update()
        {
            base.Update();
            _movement.TryMove();
        }
    }
}