using System.Linq;
using Inputs;
using ScriptableObjects;
using Unity.VisualScripting;
using UnityEngine;
using WeaponStates;

namespace PlayerStates
{
    public class Player : Entity
    {
        private static readonly int XVelocity = Animator.StringToHash("xVelocity");
        private static readonly int ZVelocity = Animator.StringToHash("zVelocity");
        private static readonly int IsRunning = Animator.StringToHash("isRunning");
        private static readonly int IsFiring = Animator.StringToHash("isFiring");
        public CharacterController CharacterController { get; private set; }
        private Animator _animator;

        [Header("Movement")]
        [SerializeField] private float movementSpeed = 5f;
        [SerializeField] public float gravityScale = 9.81f;

        [Header("Weapons")]
        [SerializeField] private Weapon currentWeapon;

        [SerializeField] private GameObject weaponGroup;

        public Weapon CurrentWeapon => this.currentWeapon;

        public float WalkMovementSpeed => this.movementSpeed;

        public float RunMovementSpeed => this.movementSpeed * 2f;

        [Header("Aiming")]
        [SerializeField] public LayerMask aimLayerMask;

        [SerializeField] private Transform aimCrosshair;

        [SerializeField] private Transform leftHandWeaponTransform;


        protected PlayerStateMachine StateMachine { get; private set; }

        private void Awake()
        {
            this.StateMachine = new PlayerStateMachine(
                new PlayerIdleState(this),
                new PlayerRunningState(this),
                new PlayerWalkState(this),
                new PlayerShootingState(this),
                new PlayerAimingState(this),
                new PlayerFallingState(this)
            );
        }

        private void Start()
        {
            this.CharacterController = GetComponent<CharacterController>();

            this._animator = GetComponentInChildren<Animator>();

            foreach (Transform children in this.weaponGroup.transform)
            {
                children.gameObject.SetActive(false);
            }

            this.currentWeapon.gameObject.SetActive(true);

            InputManager.Instance.SwitchCurrentWeaponAction += this.SwitchWeapon;
        }

        private void Update()
        {
            this.StateMachine.Update();
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

        public void ToggleRunningAnimation(bool isRunning)
        {
            this._animator.SetBool(IsRunning, isRunning);
        }

        public void TriggerFiringAnimation()
        {
            this._animator.SetTrigger(IsFiring);
        }
        
        public void SwitchAnimationlayer(int layerIndex)
        {
            for (var i = 1; i < this._animator.layerCount; i++)
            {
                this._animator.SetLayerWeight(i, 0);
            }

            this._animator.SetLayerWeight(layerIndex, 1);
        }

        private void SwitchWeapon(string weaponKey)
        {
            foreach (Transform weapon in this.weaponGroup.transform)
            {
                weapon.gameObject.SetActive(false);

                var weaponEntity = weapon.gameObject.GetComponent<Weapon>();

                if (weaponEntity.WeaponData.Keybind != weaponKey) continue;

                var layer = 0;

                switch (weaponKey)
                {
                    case "4": 
                        layer = 2;
                        break;
                    case "5":
                        layer = 3;
                        break;
                    default:
                        layer = 1;
                        break;
                }
                
                this.SwitchAnimationlayer(layer);

                this.currentWeapon = weaponEntity;
                this.currentWeapon.gameObject.SetActive(true);

                var leftHandTransform = currentWeapon.gameObject.GetComponentsInChildren<Transform>()
                    .FirstOrDefault(weaponComponent => weaponComponent.CompareTag("LeftHandWeaponTransform"));

                if (!leftHandTransform)
                {
                    Debug.LogError("LeftHandWeaponTransform not found");
                    return;
                }

                this.leftHandWeaponTransform.localPosition = leftHandTransform.localPosition;
            }
        }
    }
}