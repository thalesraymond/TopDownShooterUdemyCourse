using System;
using Inputs;
using PlayerStates;
using UnityEngine;

namespace PlayerPartials
{
    public partial class Player : Entity
    {
        public CharacterController CharacterController { get; private set; }
        
        [Header("Movement")]
        [SerializeField] public float movementSpeed = 5f;
        [SerializeField] public float gravityScale = 9.81f;
        
        [Header("Aiming")]
        [SerializeField] public LayerMask aimLayerMask;

        [SerializeField] private Transform aimCrosshair;

        private void Awake()
        {
            this.StateMachine = new PlayerStateMachine();

            this.IdleState = new PlayerIdleState(this, this.StateMachine);

            this.MoveState = new PlayerMoveState(this, this.StateMachine);
        }

        private void Start()
        {
            this.CharacterController = GetComponent<CharacterController>();
            
            this.StateMachine.Initialize(this.IdleState);
        
            InputManager.Instance.Controls.Player.Fire.performed += _ => Debug.Log("FIRE PRESSED");
        }

        private void Update()
        {
            this.StateMachine.CurrentState.Update();
        }
        
        public void UpdateAimCrosshairPosition(Vector3 position)
        {
            this.aimCrosshair.position = position;
        }
    }
}