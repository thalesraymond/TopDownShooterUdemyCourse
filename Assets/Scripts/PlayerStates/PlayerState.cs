using System.Collections.Generic;
using System.Linq;
using Inputs;
using UnityEngine;

namespace PlayerStates
{
    public abstract class PlayerState
    {
        protected PlayerState(Player player)
        {
            this.Player = player;
        }
        
        protected abstract List<System.Type> ConflictingStates { get; }
        
        public abstract bool CanActivate();
        
        public bool HasConflictWith(HashSet<PlayerState> activeStates)
        {
            return activeStates.Any(state => ConflictingStates.Contains(state.GetType()));
        }
        protected readonly Player Player;
        
        protected Vector2 PlayerAimValue { get; private set; }

        protected Vector2 PlayerMovementValue { get; private set; }

        protected Vector3 AimDirection;

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
            
            this.UpdateAnimator();
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