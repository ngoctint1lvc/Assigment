using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyGame {
    public abstract class Projectile : MonoBehaviour {

        public LayerMask collisionMask;

        protected void MoveBullet(float speed, float damage) {
            float moveDistance = speed * Time.deltaTime;
            CheckCollisions(moveDistance, damage);
            transform.Translate(Vector3.forward * moveDistance);
        }

        void CheckCollisions(float moveDistance, float damage) {
            Ray ray = new Ray(transform.position, transform.forward);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, moveDistance, collisionMask, QueryTriggerInteraction.Collide)) {
                OnHitObject(hit.collider, hit.point, damage);
            }
        }
        protected void DamageAtNear(float damage) {
            // Nếu vị trí của đạn và vị trí collisionMask chồng chéo lên nhau thì collision Mask sẽ chịu tác dụng của đạn
            Collider[] initialColisions = Physics.OverlapSphere(transform.position, .1f, collisionMask);
            if (initialColisions.Length > 0) {
                OnHitObject(initialColisions[0], transform.position, damage);
            }
        }
        protected abstract void OnHitObject(Collider c, Vector3 hitPoint, float damage);

    }
}