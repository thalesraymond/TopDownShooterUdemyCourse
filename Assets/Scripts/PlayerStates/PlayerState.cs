using Inputs;
using PlayerPartials;
using UnityEngine;

namespace PlayerStates
{
    public class PlayerState
    {
        protected PlayerStateMachine StateMachine;

        protected Player Player;
        
        protected Vector2 PlayerAimValue { get; private set; }

        protected Vector2 PlayerMovementValue { get; private set; }

        protected Vector3 AimDirection;

        public PlayerState(Player player, PlayerStateMachine stateMachine)
        {
            this.Player = player;

            this.StateMachine = stateMachine;
        }

        public virtual void Enter()
        {
        }

        public virtual void Exit()
        {
            
        }

        public virtual void Update()
        {
            this.PlayerMovementValue = InputManager.Instance.PlayerMovementValue;

            this.PlayerAimValue = InputManager.Instance.PlayerAimValue;
            
            this.ApplyGravity();

            this.DoPlayerAim();
            
            this.UpdateAnimator();
        }

        private void DoPlayerAim()
        {
            var ray = Camera.main.ScreenPointToRay(this.PlayerAimValue);

            if (!Physics.Raycast(ray, out var hitInfo, Mathf.Infinity, this.Player.aimLayerMask)) return;
            
            this.AimDirection = hitInfo.point - this.Player.transform.position;
            this.AimDirection.y = 0f;
            this.AimDirection.Normalize();

            this.Player.transform.forward = this.AimDirection;

            this.Player.UpdateAimCrosshairPosition(new Vector3(hitInfo.point.x, hitInfo.point.y, hitInfo.point.z));
        }

        private void ApplyGravity()
        {
            if (this.Player.CharacterController.isGrounded)
                return;
            
            this.Player.CharacterController.Move(Vector3.down * (this.Player.gravityScale * Time.deltaTime));
        }

        private void UpdateAnimator()
        {
            var playerMovementVector3 = new Vector3(this.PlayerMovementValue.x, 0, this.PlayerMovementValue.y);
            
            var xVelocity = Vector3.Dot(playerMovementVector3, this.Player.transform.right);
            var zVelocity = Vector3.Dot(playerMovementVector3, this.Player.transform.forward);
            
            this.Player.SetAnimatorVelocity(xVelocity, zVelocity);
        }
    }
}