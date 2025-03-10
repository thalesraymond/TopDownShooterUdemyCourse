using System;
using System.Collections.Generic;
using Inputs;

namespace PlayerStates
{
    public class PlayerIdleState : PlayerState
    {
        public PlayerIdleState(Player player) : base(player)
        {
        }

        protected override List<Type> ConflictingStates => new()
        {
            typeof(PlayerWalkState), 
            typeof(PlayerRunningState)
        };
        
        public override bool CanActivate()
        {
            return !InputManager.Instance.IsTryingToMove && !InputManager.Instance.IsTryingToRun;
        }
    }
}