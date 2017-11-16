using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace MyGame{
	[RequireComponent(typeof(NavMeshAgent))]
	[RequireComponent(typeof(EnemyAudio))]
	public class Enemy : LivingEntity {

		protected Animator animator;
		protected const float runSpeed = 10f;
		protected const float walkSpeed = runSpeed / 2f;
		public float walkDistance = 30;
		public float stopDistance = 2;

		protected bool isRunning;
		protected bool isAttack;
		protected Transform target;
		protected NavMeshAgent pathFinder;
		protected const float timeBetweenUpdateDestination = 0.1f;
		protected float nextTimeUpdateDestination;
		protected const float smoothTime = 0.5f;
		protected float currentVelocity = 0;	// Dung cho ham Mathf.SmoothDamp()

		protected float nextAttackTime;
		public float timeBetweenAttack = 0.5f;
		public float damage = 5f;

		public static event System.Action EnemyIsComming;

		protected override void Start () {
			base.Start ();
			animator = GetComponentInChildren<Animator> ();
			pathFinder = GetComponent<NavMeshAgent> ();
			animator.SetFloat ("speed", 0);
			SetTarget(GameObject.FindGameObjectWithTag ("Player"));
			OnDeath += OnEnemyDeath;
		}

		// Update is called once per frame
		protected virtual void Update () {
			// Kiem tra muc tieu
			if (target == null || dead) {
				NoTarget ();
				return;
			}

			// Neu co muc tieu thi tan cong
			if (Vector3.Distance (transform.position, target.position) > walkDistance) {
				if(isAttack) StopAttack ();
				pathFinder.speed = Mathf.SmoothDamp (pathFinder.speed, walkSpeed, ref currentVelocity, smoothTime);
				float speedPercent = Mathf.Lerp (0, 1, pathFinder.speed / runSpeed);
				animator.SetFloat ("speed", speedPercent);
			} else if (Vector3.Distance (transform.position, target.position) > stopDistance) {
				// Goi event de thay doi nhac nen
				if(EnemyIsComming != null){
					EnemyIsComming ();
				}

				if(isAttack) StopAttack ();
				pathFinder.speed = Mathf.SmoothDamp(pathFinder.speed, runSpeed, ref currentVelocity, smoothTime);
				float speedPercent = Mathf.Lerp (0, 1, pathFinder.speed / runSpeed);
				animator.SetFloat ("speed", speedPercent);
			} else {
				transform.LookAt (new Vector3(target.position.x, transform.position.y, target.position.z));
				Attack ();
			}
		}

		protected IEnumerator UpdateDestination(){
			while (target != null) {
				if(pathFinder.enabled == true) pathFinder.SetDestination (target.position);
				yield return new WaitForSeconds (timeBetweenUpdateDestination);
			}
		}

		// Stop movement and attack target
		public void Attack(){
			pathFinder.speed = 0;
			pathFinder.enabled = false;
			isAttack = true;
			animator.SetFloat ("speed", 0);
			animator.SetBool ("isAttack", true);

			if (Time.time > nextAttackTime) {
				nextAttackTime = Time.time + timeBetweenAttack;
				target.GetComponent<Player> ().TakeHit (damage);
				target.GetComponent<Player> ().Stunned (timeBetweenAttack * 0.5f);
			}
		}

		public void StopAttack(){
			pathFinder.enabled = true;
			isAttack = false;
			animator.SetBool ("isAttack", false);
		}

		// Kiem tra xem co muc tieu hay khong, neu co thi chay theo muc tieu
		public void SetTarget(GameObject target){
			if (target != null) {
				this.target = target.GetComponent<Transform> ();
				StopAllCoroutines ();
				StartCoroutine (UpdateDestination ());
			} else {
				this.target = null;
				print ("No target");
			}
		}

		void NoTarget(){
			if (isAttack) StopAttack ();
			pathFinder.enabled = false;
			animator.SetFloat ("speed", 0);
		}

		protected virtual void OnEnemyDeath(){
			pathFinder.enabled = false;
		}
	}
}
