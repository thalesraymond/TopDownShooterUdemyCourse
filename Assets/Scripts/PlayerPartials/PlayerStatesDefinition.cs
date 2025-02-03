using PlayerStates;
using Unity.VisualScripting;

namespace PlayerPartials
{
    public partial class Player
    {
        public PlayerStateMachine StateMachine { get; private set; }
        public PlayerIdleState IdleState { get; private set; }
        public PlayerMoveState MoveState { get; private set; }
    }
}