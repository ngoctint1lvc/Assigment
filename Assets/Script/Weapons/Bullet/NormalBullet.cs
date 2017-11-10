using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyGame {
    public class NormalBullet : Projectile {

        public float speed = 10;
        public float damage = 10;
        float lifetime = 3;


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
                damageableObject.TakeHit(damage, hitPoint, transform.position);
            }
            GameObject.Destroy(gameObject);
        }
    }
}