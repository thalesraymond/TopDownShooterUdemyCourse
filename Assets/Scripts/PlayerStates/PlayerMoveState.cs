﻿using Inputs;
using PlayerPartials;
using UnityEngine;

namespace PlayerStates
{
    public class PlayerMoveState : PlayerState
    {
        private CharacterController _characterController;
        public PlayerMoveState(Player player, PlayerStateMachine stateMachine) : base(player, stateMachine)
        {
        }

        public override void Enter()
        {
            base.Enter();
        }

        public override void Exit()
        {
            base.Exit();
        }

        public override void Update()
        {
            base.Update();

            if (!InputManager.Instance.IsTryingToMove)
            {
                this.StateMachine.ChangeState(this.Player.IdleState);
                return;
            }
            
            var movementDirection = new Vector3(InputManager.Instance.PlayerMovementValue.x, 0, InputManager.Instance.PlayerMovementValue.y) * (this.Player.movementSpeed * Time.deltaTime);

            this.Player.CharacterController.Move(movementDirection);
        }
    }
}