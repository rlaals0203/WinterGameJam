using Code.Core;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Code.Entities
{
    public class PlayerDeadState : PlayerState
    {
        public PlayerDeadState(Entity entity, int animationHash) : base(entity, animationHash)
        {
        }

        public override void Enter()
        {
            base.Enter();
            _movement.CanMove = false;
        }

        public override void Update()
        {
            base.Update();
            if (_isTriggerCall)
            {
                GameManager.Instance.isCombatMode = false;
                SceneManager.LoadScene("GameScene");
            }
        }
    }
}