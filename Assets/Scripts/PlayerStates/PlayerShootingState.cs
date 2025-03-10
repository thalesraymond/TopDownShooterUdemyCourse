using System;
using System.Collections.Generic;
using Inputs;
using UnityEngine;

namespace PlayerStates
{
    public class PlayerShootingState : PlayerState
    {
        public PlayerShootingState(Player player) : base(player)
        {
        }

        protected override List<Type> ConflictingStates => new();

        public override bool CanActivate()
        {
            return InputManager.Instance.IsTryingToShoot;
        }

        public override void Enter()
        {
            base.Enter();
        }

        public override void Update()
        {
            base.Update();
            
            //check if ani
            
            this.Player.TriggerFiringAnimation();
        }
    }
}