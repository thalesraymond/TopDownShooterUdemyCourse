using System;
using System.Collections;
using PlayerStates;
using ScriptableObjects;
using UnityEngine;
using UnityEngine.Serialization;

namespace WeaponStates
{
    public class Weapon : Entity
    {
        [SerializeField] private WeaponData weaponData;

        public WeaponData WeaponData => this.weaponData;

        [SerializeField] private int currentAmmo;

        public int CurrentAmmo => this.currentAmmo;

        private Player _player;

        protected WeaponStateMachine StateMachine { get; private set; }

        private bool _canFire = true;

        private void Start()
        {
            this._player = GetComponentInParent<Player>();

            this.StateMachine = new WeaponStateMachine(
                new WeaponIdleState(this._player),
                new WeaponFiringState(this._player),
                new WeaponReloadState(this._player)
            );
        }

        private void Update()
        {
            this.StateMachine.Update();
        }

        public bool CanShoot()
        {
            return this.HasAmmo();
        }

        public bool CanReload()
        {
            return this.weaponData.MagazineSize < this.CurrentAmmo ;
        }

        public bool HasAmmo()
        {
            return this.currentAmmo > 0;
        }

        public void Fire()
        {
            if (this.currentAmmo <= 0) return;

            if (!_canFire) return;

            _canFire = false;

            StartCoroutine(StartFiringCoroutine());
        }

        private IEnumerator StartFiringCoroutine()
        {
            var timeBetweenShots = 1f / this.weaponData.FireRate;

            this.currentAmmo -= 1;

            yield return new WaitForSeconds(timeBetweenShots);

            _canFire = true;
        }
    }
}