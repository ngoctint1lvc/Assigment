using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
[RequireComponent(typeof(Animator))]
public class EnemyAudio : MonoBehaviour {

	public AudioClip stepOnGround;
	public AudioClip stepOnWater;
	public AudioClip enemyComeAudio;
	public AudioClip enemyAttackAudio;

	AudioSource enemySoundAudio;
	AudioSource enemyStepAudio;

	float nextStepTime;
	float nextAttackTime;
	float nextComeTime;
	public const float runtStepRate = 4f;
	public const float walkStepRate = 1.5f;
	public const float waterLevel = 1.5f;
	Transform player;
	Animator animator;


	void Awake(){
		GameObject newGameObject = new GameObject ("Crawler Step Audio");
		newGameObject.AddComponent<AudioSource> ();
		newGameObject.transform.parent = transform;
		newGameObject.transform.localPosition = Vector3.zero;
	}


	void Start(){
		enemySoundAudio = GetComponent<AudioSource> ();
		enemyStepAudio = GameObject.Find ("Crawler Step Audio").GetComponent<AudioSource> ();
		enemyStepAudio.spatialBlend = 1;
		enemyStepAudio.volume = 1;
		animator = GetComponentInChildren<Animator> ();

		if (GameObject.FindGameObjectWithTag ("Player") != null) {
			player = GameObject.FindGameObjectWithTag ("Player").GetComponent<Transform> ();
		} else {
			player = null;
		}
	}

	void Update(){
		if (transform.position.y > waterLevel) {
			enemyStepAudio.clip = stepOnGround;
		} else {
			enemyStepAudio.clip = stepOnWater;
		}

		float currentSpeed = animator.GetFloat ("speed");
		if (currentSpeed > 0 && Time.time > nextStepTime) {
			if (currentSpeed > 0.5f) {
				nextStepTime = Time.time + 1 / runtStepRate;
			} else {
				nextStepTime = Time.time + 1 / walkStepRate;
			}
			enemyStepAudio.Play ();
		} 

		if (player == null) return;
		if (Vector3.Distance (transform.position, player.position) < 2f && Time.time > nextAttackTime) {
			nextAttackTime = Time.time + enemyAttackAudio.length;
			enemySoundAudio.clip = enemyAttackAudio;
			enemySoundAudio.Play ();
		} else if (Vector3.Distance (transform.position, player.position) < 30f && Time.time > nextComeTime) {
			nextComeTime = Time.time + enemyComeAudio.length;
			enemySoundAudio.clip = enemyComeAudio;
			enemySoundAudio.Play ();
		}
	}
}
