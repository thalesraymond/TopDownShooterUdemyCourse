using System;
using System.Collections.Generic;
using Inputs;
using PlayerStates;

namespace WeaponStates
{
    public class WeaponFiringState : WeaponState
    {
        public WeaponFiringState(Player player) : base(player)
        {
        }

        protected override List<Type> ConflictingStates { get; }
        public override bool CanActivate()
        {
            return InputManager.Instance.IsTryingToShoot && this.Player.CurrentWeapon.CanShoot();
        }

        public override void Update()
        {
            base.Update();
            
            this.Player.CurrentWeapon.Fire();
            
        }
    }
}