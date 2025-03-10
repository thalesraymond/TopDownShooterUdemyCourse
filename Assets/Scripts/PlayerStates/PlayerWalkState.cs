using System;
using System.Collections.Generic;
using Inputs;
using UnityEngine;

namespace PlayerStates
{
    public class PlayerWalkState : PlayerState
    {
        private CharacterController _characterController;
        public PlayerWalkState(Player player) : base(player)
        {
        }

        protected override List<Type> ConflictingStates => new()
        {
            typeof(PlayerRunningState), 
            typeof(PlayerIdleState)
        };
        
        public override bool CanActivate()
        {
            return InputManager.Instance.IsTryingToMove && !InputManager.Instance.IsTryingToRun;
        }

        public override void Update()
        {
            base.Update();
            
            var movementDirection = new Vector3(InputManager.Instance.PlayerMovementValue.x, 0, InputManager.Instance.PlayerMovementValue.y) * (this.Player.WalkMovementSpeed * Time.deltaTime);

            this.Player.CharacterController.Move(movementDirection);
        }
    }
}