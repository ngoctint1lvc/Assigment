using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;

namespace MyGame {
    [RequireComponent(typeof(NavMeshAgent))]
    public class Enemy : LivingEntity {

        public enum State { Idle, Chasing, Attacking };
        State currentState;

        public ParticleSystem deathEffect;

        NavMeshAgent pathfinder;
        Transform target;
        LivingEntity targetEntity;
        //Material skinMaterial;

        //Color originalColor;

        float attackDistanceThreshold = 1.5f;
        float timeBetweenAttacks = 1;
        float damage = 1;

        float nextAttackTime;
        float myCollisionRadius;
        float targetCollisionRadius;

        bool hasTarget;

        Animator anima;

        void Awake() {
        }
        protected override void Start() {
            base.Start();
            pathfinder = GetComponent<NavMeshAgent>();
            //skinMaterial = GetComponent<Renderer>().material;
            //originalColor = skinMaterial.color;

            if (GameObject.FindGameObjectWithTag("Player") != null) {
                currentState = State.Chasing;

                hasTarget = true;

                anima = GetComponent<Animator>();
                //anima.SetBool("Walk", true);

                target = GameObject.FindGameObjectWithTag("Player").transform;
                targetEntity = target.GetComponent<LivingEntity>();
                targetEntity.OnDeath += OnTargetDeath;

                myCollisionRadius = GetComponent<CapsuleCollider>().radius;
                targetCollisionRadius = target.GetComponent<CapsuleCollider>().radius;

                StartCoroutine(PathUpdated());
            }
        }
        void Update() {
            if (hasTarget) {
                if (Time.time > nextAttackTime) {
                    float sqrDstToTarget = (target.position - transform.position).sqrMagnitude;
                    if (sqrDstToTarget < Mathf.Pow(attackDistanceThreshold + myCollisionRadius + targetCollisionRadius, 2)) {
                        nextAttackTime = Time.time + timeBetweenAttacks;
                        StartCoroutine(Attack());
                    };
                };
            }
        }

        public override void TakeHit(float damage, Vector3 hitPoint, Vector3 hitDirection) {
            if (damage >= health) {
                Destroy(Instantiate(deathEffect.gameObject, hitPoint, Quaternion.FromToRotation(Vector3.forward, hitDirection)) as GameObject, deathEffect.startLifetime);
            }
            base.TakeHit(damage, hitPoint, hitDirection);
        }
        IEnumerator Attack() {
            currentState = State.Attacking;
            anima.SetBool("Attack", true);
            Vector3 originalPosition = transform.position;

            // hướng từ enemy đến target
            Vector3 dirToTarget = (target.position - transform.position).normalized;
            // vị trí enemy attack = vị trí target trong worldpoint - (vectơ tổng bán kính của enemy và target)
            Vector3 attackPosition = target.position - dirToTarget * (myCollisionRadius + targetCollisionRadius);

            float attackSpeed = 3;
            float percent = 0;

            //skinMaterial.color = Color.red;
            bool hasAppliedDamage = false;

            while (percent <= 1) {
                if (percent >= .5f && !hasAppliedDamage) {
                    hasAppliedDamage = true;
                    targetEntity.TakeHit(damage);
                }
                percent += Time.deltaTime * attackSpeed;
                float interpolation = (-Mathf.Pow(percent, 2) + percent) * 4;
                // vị trí của enemy biến thiên theo hàm -4x^2+4x
                transform.position = Vector3.Lerp(originalPosition, attackPosition, interpolation);

                yield return null;
            }

            //skinMaterial.color = originalColor;
            anima.SetBool("Attack", false);
            currentState = State.Chasing;
            pathfinder.enabled = true;
        }
        IEnumerator PathUpdated() {
            float refreshRate = .25f;
            while (hasTarget) {
                if (currentState == State.Chasing) {
                    // hướng từ enemy đến target
                    Vector3 dirToTarget = (target.position - transform.position).normalized;
                    // vị trí target đối với enemy (vị trí enemy cần truy đuổi) = vị trí target trong worldpoint - (vectơ tổng bán kính của enemy và target + khoảng cách attack / 2)
                    Vector3 targetPosition = target.position - dirToTarget * (myCollisionRadius + targetCollisionRadius + attackDistanceThreshold / 2);
                    if (!dead) {
                        pathfinder.SetDestination(targetPosition);
                    }
                }
                yield return new WaitForSeconds(refreshRate);
            }
        }
        void OnTargetDeath() {
            hasTarget = false;
            currentState = State.Idle;
        }
    }
}