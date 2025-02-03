using Inputs;
using PlayerPartials;

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

            if (InputManager.Instance.IsTryingToMove)
                this.StateMachine.ChangeState(this.Player.MoveState);
        }
    }
}