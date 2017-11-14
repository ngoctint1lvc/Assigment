using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using MyGame;

[RequireComponent(typeof(NavMeshAgent))]
public class Crawler : LivingEntity {

	Animator animator;
	const float runSpeed = 10f;
	const float walkSpeed = runSpeed / 2f;
	bool isRunning;
	bool isAttack;
	Transform target;
	NavMeshAgent pathFinder;
	public LayerMask layer;
	const float timeBetweenUpdateDestination = 0.1f;
	float nextTimeUpdateDestination;
	const float stopDistance = 1.5f;
	const float smoothTime = 0.5f;
	float currentVelocity = 0;	// Dung cho ham Mathf.SmoothDamp()

	//public event System.Action OnCrawlerDeath;

	// Use this for initialization
	protected override void Start () {
		base.Start ();
		animator = GetComponentInChildren<Animator> ();
		pathFinder = GetComponent<NavMeshAgent> ();
		animator.SetFloat ("speed", 0);
		SetTarget(GameObject.FindGameObjectWithTag ("Player"));
	}

	// Update is called once per frame
	void Update () {
		// Kiem tra muc tieu, neu khong co muc tieu thi cho nguoi dung dieu khien
		if (target == null) return;

		// Neu co muc tieu thi tan cong
		if (Vector3.Distance (transform.position, target.position) > 30f) {
			if(isAttack) StopAttack ();
			pathFinder.speed = Mathf.SmoothDamp (pathFinder.speed, walkSpeed, ref currentVelocity, smoothTime);
			float speedPercent = Mathf.Lerp (0, 1, pathFinder.speed / runSpeed);
			animator.SetFloat ("speed", speedPercent);
		} else if (Vector3.Distance (transform.position, target.position) > stopDistance) {
			if(isAttack) StopAttack ();
			pathFinder.speed = Mathf.SmoothDamp(pathFinder.speed, runSpeed, ref currentVelocity, smoothTime);
			float speedPercent = Mathf.Lerp (0, 1, pathFinder.speed / runSpeed);
			animator.SetFloat ("speed", speedPercent);
		} else {
			transform.LookAt (new Vector3(target.position.x, transform.position.y, target.position.z));
			Attack ();
		}
	}

	IEnumerator UpdateDestination(){
		while (target != null) {
			if(pathFinder.enabled == true) pathFinder.SetDestination (target.position);
			yield return new WaitForSeconds (timeBetweenUpdateDestination);
		}
	}

	public void SetCrawlerSpeed(float speed){
		if(isAttack) StopAttack ();
		pathFinder.speed = Mathf.SmoothDamp (pathFinder.speed, walkSpeed, ref currentVelocity, smoothTime);
		float speedPercent = Mathf.Lerp (0, 1, pathFinder.speed / runSpeed);
		animator.SetFloat ("speed", speedPercent);
	}

	// Stop movement and attack target
	public void Attack(){
		pathFinder.speed = 0;
		pathFinder.enabled = false;
		isAttack = true;
		animator.SetFloat ("speed", 0);
		animator.SetBool ("isAttack", true);
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
			print (target.gameObject.name);
			StopAllCoroutines ();
			StartCoroutine (UpdateDestination ());
		} else {
			this.target = null;
			print ("No target");
		}
	}
}