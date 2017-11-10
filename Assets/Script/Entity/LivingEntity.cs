using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyGame {
    public class LivingEntity : MonoBehaviour, IDamageable {

        public float startingHealth;
        protected float health;
        protected bool dead;

        public event System.Action OnDeath;

        protected virtual void Start() {
            health = startingHealth;
        }
        void Update() {

        }

        public virtual void TakeHit(float damage, Vector3 hitPoint, Vector3 hitDirection) {
            // chèn thêm hiệu ứng cho RaycastHit hit
            TakeHit(damage);
        }
        public virtual void TakeHit(float damage) {
            health -= damage;
            if (health <= 0 && !dead) {
                Die();
            }
        }
        protected void Die() {
            dead = true;
            if (OnDeath != null) {
                OnDeath();
            };
            Destroy(gameObject);
        }
    }
}