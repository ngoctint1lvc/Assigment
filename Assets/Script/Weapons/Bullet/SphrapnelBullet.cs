using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyGame {
    public class SphrapnelBullet : Projectile {

        public float speed = 10;
        public float damage = 10;
        float lifetime = 3;
        public GameObject bulletEffect;

        void Start() {
            Destroy(gameObject, lifetime);
            DamageAtNear(damage);
        }

        void Update() {
            MoveBullet(speed, damage);
        }

        public void SetSpeed(float newSpeed) {
            speed = newSpeed;
        }
        protected override void OnHitObject(Collider c, Vector3 hitPoint, float damage) {
            IDamageable damageableObject = c.GetComponent<IDamageable>();
            if (damageableObject != null) {
                Transform here = GetComponent<Transform>();
                bulletEffect = Instantiate(bulletEffect, here.position, Quaternion.identity);
                Destroy(bulletEffect, 0.5f);
                damageableObject.TakeHit(damage, hitPoint, transform.position);

                ExtendDamage(hitPoint);
            }
            GameObject.Destroy(gameObject);
        }

        void ExtendDamage(Vector3 hitPoint) {
            GameObject[] Enemy = GameObject.FindGameObjectsWithTag("Enemy");

            foreach (GameObject e in Enemy) {
                IDamageable damageableObject = e.GetComponent<IDamageable>();
                // Khoảng cách(^2) từ enemy đến hitPoint
                float distance = (e.transform.position - hitPoint).sqrMagnitude;
                if (distance < 20f) damageableObject.TakeHit(damage / 2, hitPoint, transform.position);
                else if (distance < 50f) damageableObject.TakeHit(damage / 4, hitPoint, transform.position);
            }
        }
    }
}