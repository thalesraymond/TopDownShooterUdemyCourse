using System.Collections.Generic;
using System.Linq;
using PlayerStates;

namespace WeaponStates
{
    public class WeaponStateMachine
    {
        private readonly HashSet<WeaponState> _activeStates = new();

        private readonly HashSet<WeaponState> _allStates = new();

        //args to add states in constructor
        public WeaponStateMachine(params WeaponState[] states)
        {
            foreach (var state in states) 
                _allStates.Add(state);
        }
        
        private void TryAddState(WeaponState state)
        {
            if (!state.CanActivate() || _activeStates.Contains(state)) return;
            
            if (state.HasConflictWith(_activeStates)) return;
                
            state.Enter();
                
            _activeStates.Add(state);
        }
        
        private void RemoveState(WeaponState state)
        {
            if (!_activeStates.Contains(state)) return;
            
            state.Exit();
            
            _activeStates.Remove(state);
        }
        
        public void Update()
        {
            var statesToRemove = new HashSet<WeaponState>();
            
            foreach (var state in _activeStates.Where(state => !state.CanActivate())) 
                statesToRemove.Add(state);
            
            foreach (var state in statesToRemove) 
                this.RemoveState(state);
            
            foreach (var state in _activeStates) 
                state.Update();
            
            foreach (var state in _allStates) 
                this.TryAddState(state);
        }

    }
}