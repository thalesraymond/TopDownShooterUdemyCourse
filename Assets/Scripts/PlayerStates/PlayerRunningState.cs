using Inputs;
using PlayerPartials;
using UnityEngine;

namespace PlayerStates
{
    public class PlayerRunningState : PlayerState
    {
        private CharacterController _characterController;
        public PlayerRunningState(Player player, PlayerStateMachine stateMachine) : base(player, stateMachine)
        {
        }

        public override void Enter()
        {
            base.Enter();
            
            this.Player.ToggleRunningAnimation(true);
            
            // increase player speed by 20%
            
            
        }

        public override void Exit()
        {
            base.Exit();
            
            this.Player.ToggleRunningAnimation(false);
        }

        public override void Update()
        {
            base.Update();

            if (!InputManager.Instance.IsTryingToRun || !InputManager.Instance.IsTryingToMove)
            {
                this.StateMachine.ChangeState(this.Player.IdleState);
                
                return;
            }
            
            var movementDirection = new Vector3(InputManager.Instance.PlayerMovementValue.x, 0, InputManager.Instance.PlayerMovementValue.y) * (this.Player.RunMovementSpeed * Time.deltaTime);

            this.Player.CharacterController.Move(movementDirection);
        }
    }
}