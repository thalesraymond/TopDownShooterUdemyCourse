using Inputs;
using PlayerPartials;
using UnityEngine;

namespace PlayerStates
{
    public class PlayerIdleState : PlayerState
    {
        public PlayerIdleState(Player player, PlayerStateMachine stateMachine) : base(player, stateMachine)
        {
        }

        public override void Enter()
        {
            base.Enter();
        }

        public override void Update()
        {
            base.Update();
            
            if (!InputManager.Instance.IsTryingToMove)
                return;

            if (InputManager.Instance.IsTryingToRun)
            {
                this.StateMachine.ChangeState(this.Player.RunningState);
                return;
            }
            
            this.StateMachine.ChangeState(this.Player.WalkState);
            
        }
    }
}