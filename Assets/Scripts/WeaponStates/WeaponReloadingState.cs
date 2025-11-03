using System;
using System.Collections.Generic;
using Inputs;
using PlayerStates;
using UnityEngine;

namespace WeaponStates
{
    public class WeaponReloadState : WeaponState
    {
        public WeaponReloadState(Player player) : base(player)
        {
        }

        protected override List<Type> ConflictingStates => new()
        {
            typeof(WeaponFiringState)
        };

        public override bool CanActivate()
        {
            return !InputManager.Instance.IsTryingToShoot && InputManager.Instance.IsTryingToReload;
        }

        public override void Update()
        {
            base.Update();
        }
    }
}