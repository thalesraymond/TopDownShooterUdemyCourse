using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Inputs
{
    public class InputManager : MonoBehaviour
    {
        public static InputManager Instance { get; private set; }
        public Controls Controls { get; private set; }

        public Vector2 PlayerMovementValue { get; private set; }

        public bool IsTryingToMove { get; private set; }

        public bool IsTryingToRun { get; private set; }

        public bool IsTryingToShoot { get; private set; }

        public bool IsTryingToReload { get; private set; }

        public Vector2 PlayerAimValue { get; private set; }

        public Action<string> SwitchCurrentWeaponAction { get; set; }

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

            this.Controls.Player.SwitchWeapon.performed += context => this.SwitchCurrentWeapon(context);
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

            this.HandleFiring();

            this.HandleReloading();
        }

        private void HandleReloading()
        {
            this.IsTryingToReload = this.Controls.Player.Reload.IsPressed();
        }

        private void SwitchCurrentWeapon(InputAction.CallbackContext context)
        {
            this.SwitchCurrentWeaponAction(context.control.name);
        }

        private void HandleMovement()
        {
            this.PlayerMovementValue = this.Controls.Player.Movement.ReadValue<Vector2>();

            this.IsTryingToMove = this.PlayerMovementValue.magnitude > 0;

            this.IsTryingToRun = this.Controls.Player.Run.IsPressed();
        }

        private void HandleAim()
        {
            this.PlayerAimValue = this.Controls.Player.Aim.ReadValue<Vector2>();
        }

        private void HandleFiring()
        {
            this.IsTryingToShoot = this.Controls.Player.Fire.IsPressed();
        }
    }
}