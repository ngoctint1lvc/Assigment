using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyGame {
    public class Weapon : MonoBehaviour {
        // Thời gian giữa các lần tấn công
        public float msDelay = 100;
        float nextTurn;

        public Transform muzzle;

        public NormalBullet nbullet;
        public SphrapnelBullet sbullet;
        
        public float muzzleVelocity = 35;

		public AudioClip clipReloadBullet;
		public AudioClip clipShoot;
		int remainBullets;
		public const int numberBullets = 5;
		public const float timeToReloadBullets = 1;

        delegate void Offensive();
        Offensive att;
		void Start() {
            if (this.tag == "Gun1") att = Shoot1;
            if (this.tag == "Gun2") att = Shoot2;
            if (this.tag == "Gun3") att = Shoot3;
        }

        public void Attack() {
            att();
        }

        void Shoot1() {
            if (Time.time > nextTurn) {
                nextTurn = Time.time + msDelay / 1000;

				if (remainBullets == 0) {
					nextTurn = Time.time + timeToReloadBullets;
					AudioManager.instance.PlaySound (clipReloadBullet, muzzle.position);
					remainBullets = numberBullets;
					return;
				}

				NormalBullet newProjectile = Instantiate(nbullet, transform.position, transform.rotation) as NormalBullet;
				newProjectile.transform.parent = transform;
				//newProjectile.transform.localRotation = Quaternion.identity;
				//newProjectile.transform.localPosition = Vector3.zero;

				//print ("bulletAngle : " + bulletAngle);
				//print ("bulletPosition: " + bulletPosition);

				newProjectile.transform.parent = null;

				newProjectile.SetSpeed(muzzleVelocity);
				//newProjectile.transform.parent = null;

				remainBullets--;
				AudioManager.instance.PlaySound (clipShoot, muzzle.position);
            }
        }

        void Shoot2() {
            if (Time.time > nextTurn) {
                nextTurn = Time.time + msDelay / 1000;

				if (remainBullets == 0) {
					nextTurn = Time.time + timeToReloadBullets;
					AudioManager.instance.PlaySound (clipReloadBullet, muzzle.position);
					remainBullets = numberBullets;
					return;
				}
                
				NormalBullet newProjectile = Instantiate(nbullet, transform.position, transform.rotation) as NormalBullet;
				newProjectile.transform.parent = transform;
				newProjectile.transform.localPosition = Vector3.zero;
				newProjectile.transform.localRotation = Quaternion.identity;
				newProjectile.transform.parent = null;
                newProjectile.SetSpeed(muzzleVelocity);

				remainBullets--;
				AudioManager.instance.PlaySound (clipShoot, muzzle.position);
            }
        }

        void Shoot3() {
            if (Time.time > nextTurn) {
                nextTurn = Time.time + msDelay / 1000;

				if (remainBullets == 0) {
					nextTurn = Time.time + timeToReloadBullets;
					AudioManager.instance.PlaySound (clipReloadBullet, muzzle.position);
					remainBullets = numberBullets;
					return;
				}

                SphrapnelBullet newProjectile = Instantiate(sbullet, transform.position, transform.rotation) as SphrapnelBullet;
				newProjectile.transform.parent = transform;
				newProjectile.transform.localPosition = Vector3.zero;
				newProjectile.transform.localRotation = Quaternion.identity;
				newProjectile.transform.parent = null;
                newProjectile.SetSpeed(muzzleVelocity);

				remainBullets--;
				AudioManager.instance.PlaySound (clipShoot, muzzle.position);
            }
        }
    } 
}