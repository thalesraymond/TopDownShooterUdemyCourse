using Inputs;
using PlayerPartials;
using UnityEngine;

namespace PlayerStates
{
    public class PlayerState
    {
        protected PlayerStateMachine StateMachine;

        protected Player Player;
        
        protected Vector2 PlayerAimValue { get; private set; }

        protected Vector2 PlayerMovementValue { get; private set; }

        public PlayerState(Player player, PlayerStateMachine stateMachine)
        {
            this.Player = player;

            this.StateMachine = stateMachine;
        }

        public virtual void Enter()
        {
        }

        public virtual void Exit()
        {
            
        }

        public virtual void Update()
        {
            this.PlayerMovementValue = InputManager.Instance.PlayerMovementValue;

            this.PlayerAimValue = InputManager.Instance.PlayerAimValue;
            
            this.ApplyGravity();
        }

        private void ApplyGravity()
        {
            if (this.Player.CharacterController.isGrounded)
                return;
            
            this.Player.CharacterController.Move(Vector3.down * (this.Player.gravityScale * Time.deltaTime));
        }
    }
}