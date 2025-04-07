using UnityEngine;

namespace ScriptableObjects
{
    [CreateAssetMenu(menuName = "Create Weapon", fileName = "Items/Weapon", order = 0)]
    public class WeaponData : ItemData
    {   
        public int Damage;
        public float FireRate;
        public float Range;
        public int MagazineSize;
        public int MaxAmmo;
        public float ReloadTime;
        public float Recoil;
        public float Spread;
        public float Accuracy;
        public bool IsAutomatic;
        public string Keybind;

    }
}