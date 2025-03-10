using System;
using System.Collections.Generic;
using Inputs;
using UnityEngine;

namespace PlayerStates
{
    public class PlayerFallingState : PlayerState
    {
        public PlayerFallingState(Player player) : base(player)
        {
        }

        protected override List<Type> ConflictingStates => new();
        
        public override bool CanActivate()
        {
            return !Player.CharacterController.isGrounded;
        }
        
        private void ApplyGravity()
        {
            if (this.Player.CharacterController.isGrounded)
                return;
            
            this.Player.CharacterController.Move(Vector3.down * (this.Player.gravityScale * Time.deltaTime));
        }
        
        public override void Update()
        {
            base.Update();
            
            ApplyGravity();
        }
    }
}