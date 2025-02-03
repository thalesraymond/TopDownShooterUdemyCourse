using UnityEngine;

namespace Inputs
{
    public class InputManager : MonoBehaviour
    {
        public static InputManager Instance { get; private set; }
        public Controls Controls { get; private set; }
    
        public Vector2 PlayerMovementValue { get; private set; }
        public bool IsTryingToMove { get; private set; }
        
        public Vector2 PlayerAimValue { get; private set; }

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                Destroy(this);
            }

            this.Controls = new Controls();
        }

        private void OnEnable()
        {
            this.Controls.Enable();
        }
    
        private void OnDisable()
        {
            this.Controls.Disable();
        }

        private void Update()
        {
            this.HandleMovement();
            
            this.HandleAim();
        }

        private void HandleMovement()
        {
            this.PlayerMovementValue = this.Controls.Player.Movement.ReadValue<Vector2>();
        
            this.IsTryingToMove = this.PlayerMovementValue.magnitude > 0;
        }
        
        private void HandleAim()
        {
            this.PlayerAimValue = this.Controls.Player.Aim.ReadValue<Vector2>();
        }
    }
}