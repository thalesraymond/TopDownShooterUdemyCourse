using System.Collections.Generic;
using System.Linq;
using Inputs;
using PlayerStates;
using UnityEngine;

namespace WeaponStates
{
    public abstract class WeaponState
    {
        protected WeaponState(Player player)
        {
            this.Player = player;
        }
        
        protected abstract List<System.Type> ConflictingStates { get; }
        
        public abstract bool CanActivate();
        
        public bool HasConflictWith(HashSet<WeaponState> activeStates)
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
        }
    }
}