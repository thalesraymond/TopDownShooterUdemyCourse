using System;
using Inputs;
using PlayerStates;
using UnityEngine;

namespace PlayerPartials
{
    public partial class Player : Entity
    {
        private static readonly int XVelocity = Animator.StringToHash("xVelocity");
        private static readonly int ZVelocity = Animator.StringToHash("zVelocity");
        public CharacterController CharacterController { get; private set; }
        private Animator _animator;
        
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
            
            this._animator = GetComponentInChildren<Animator>();
        }

        private void Update()
        {
            this.StateMachine.CurrentState.Update();
        }
        
        public void UpdateAimCrosshairPosition(Vector3 position)
        {
            this.aimCrosshair.position = position;
        }

        public void SetAnimatorVelocity(float xVelocity, float zVelocity)
        {   
            this._animator.SetFloat(XVelocity, xVelocity, .1f, Time.deltaTime);
            
            this._animator.SetFloat(ZVelocity, zVelocity, .1f, Time.deltaTime);
        }
    }
}