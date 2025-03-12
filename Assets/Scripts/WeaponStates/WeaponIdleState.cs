using System;
using System.Collections.Generic;
using Inputs;
using PlayerStates;

namespace WeaponStates
{
    public class WeaponIdleState : WeaponState
    {
        public WeaponIdleState(Player player) : base(player)
        {
        }

        protected override List<Type> ConflictingStates => new ();
        
        public override bool CanActivate()
        {
            return !InputManager.Instance.IsTryingToShoot;
        }
    }
}