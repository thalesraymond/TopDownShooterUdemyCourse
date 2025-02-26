using PlayerStates;
using Unity.VisualScripting;

namespace PlayerPartials
{
    public partial class Player
    {
        public PlayerStateMachine StateMachine { get; private set; }
        public PlayerIdleState IdleState { get; private set; }
        public PlayerWalkState WalkState { get; private set; }
        public PlayerRunningState RunningState { get; private set; }
    }
}