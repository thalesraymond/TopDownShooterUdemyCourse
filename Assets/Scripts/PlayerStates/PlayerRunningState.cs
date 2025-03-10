using System;
using System.Collections.Generic;
using Inputs;
using UnityEngine;

namespace PlayerStates
{
    public class PlayerRunningState : PlayerState
    {
        private CharacterController _characterController;
        
        public PlayerRunningState(Player player) : base(player)
        {
        }

        protected override List<Type> ConflictingStates => new()
        {
            typeof(PlayerWalkState),
            typeof(PlayerIdleState)
        };
        
        public override bool CanActivate()
        {
            return InputManager.Instance.IsTryingToRun && InputManager.Instance.IsTryingToMove;
        }

        public override void Enter()
        {
            base.Enter();
            
            this.Player.ToggleRunningAnimation(true);
        }

        public override void Exit()
        {
            base.Exit();
            
            this.Player.ToggleRunningAnimation(false);
        }

        public override void Update()
        {
            base.Update();
            
            var movementDirection = new Vector3(InputManager.Instance.PlayerMovementValue.x, 0, InputManager.Instance.PlayerMovementValue.y) * (this.Player.RunMovementSpeed * Time.deltaTime);

            this.Player.CharacterController.Move(movementDirection);
        }
    }
}