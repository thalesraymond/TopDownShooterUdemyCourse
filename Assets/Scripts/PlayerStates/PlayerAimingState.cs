using System;
using System.Collections.Generic;
using UnityEngine;

namespace PlayerStates
{
    public class PlayerAimingState : PlayerState
    {
        private const int DefaultAimYValue = 1;
        public PlayerAimingState(Player player) : base(player)
        {
        }

        protected override List<Type> ConflictingStates => new();
        
        public override bool CanActivate()
        {
            return true;
        }
        
        private void DoPlayerAim()
        {
            var ray = Camera.main.ScreenPointToRay(this.PlayerAimValue);

            if (!Physics.Raycast(ray, out var hitInfo, Mathf.Infinity, this.Player.aimLayerMask)) return;
            
            this.AimDirection = hitInfo.point - this.Player.transform.position;
            this.AimDirection.y = 0f;
            this.AimDirection.Normalize();

            this.Player.transform.forward = this.AimDirection;
            
            Debug.Log("Player Position: " + this.Player.transform.position);
            
            Debug.Log("Aim Position: " + new Vector3(hitInfo.point.x, this.Player.transform.position.y + DefaultAimYValue, hitInfo.point.z));

            this.Player.UpdateAimCrosshairPosition(new Vector3(hitInfo.point.x, this.Player.transform.position.y + DefaultAimYValue, hitInfo.point.z));
        }
        
        public override void Update()
        {
            base.Update();
            
            this.DoPlayerAim();
        }
    }
}