using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyGame {
    public class WeaponController : MonoBehaviour {

        public Transform weaponHold;
        public Weapon startingWeapon;
        Weapon equippedWeapon;

        void Start() {
            if (startingWeapon != null) EquipWeapon(startingWeapon);
        }

        public void EquipWeapon(Weapon item) {
            if (equippedWeapon != null) Destroy(equippedWeapon.gameObject);
			equippedWeapon = Instantiate(item, weaponHold.position, weaponHold.rotation) as Weapon;
            equippedWeapon.transform.parent = weaponHold;
        }

        public void Attack() {
            if (equippedWeapon != null) equippedWeapon.Attack();
        }
    }
}