using System;
using System.Collections.Generic;
using Inputs;
using UnityEngine;

namespace PlayerStates
{
    public class PlayerReloadingState : PlayerState
    {
        private bool _isReloading = false;

        public PlayerReloadingState(Player player) : base(player)
        {
        }

        protected override List<Type> ConflictingStates => new()
        {
            typeof(PlayerShootingState)
        };

        public override bool CanActivate()
        {
            return !InputManager.Instance.IsTryingToShoot
                && InputManager.Instance.IsTryingToReload
                && this.Player.CurrentWeapon.CanReload();
        }

        public override void Enter()
        {
            base.Enter();

            this.Player.Rig.weight = 0;

            this.Player.TriggerReloadingAnimation();
        }

        public override void Exit()
        {
            base.Exit();

            this.Player.Rig.weight = 1;
        }
    }
}