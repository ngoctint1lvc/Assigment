using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyGame{
	[RequireComponent(typeof(AudioSource))]
	[RequireComponent(typeof(Animator))]
	public class EnemyAudio : MonoBehaviour {

		public AudioClip stepOnGround;
		public AudioClip stepOnWater;
		public AudioClip enemyAttackAudio;
		public AudioClip enemyDeathAudio;

		AudioSource enemySoundAudio;
		AudioSource enemyStepAudio;
		[Range(-3, 3)]
		public float attackAudioPitch = 1;

		float nextStepTime;
		float nextAttackTime;
		float nextComeTime;
		public float runtStepRate = 4f;
		public float walkStepRate = 1.5f;
		public float waterLevel = 1.5f;
		Transform player;
		Animator animator;

		float stopDistance, walkDistance;
		bool isDead;

		void Awake(){
			GameObject newGameObject = new GameObject ("Enemy Step Audio");
			newGameObject.AddComponent<AudioSource> ();
			newGameObject.transform.parent = transform;
			newGameObject.transform.localPosition = Vector3.zero;
		}

		void Start(){
			stopDistance = GetComponent<Enemy> ().stopDistance;
			walkDistance = GetComponent<Enemy> ().walkDistance;
			enemySoundAudio = GetComponent<AudioSource> ();
			enemySoundAudio.pitch = attackAudioPitch;
			enemySoundAudio.clip = enemyAttackAudio;

			enemyStepAudio = GetComponentsInChildren<AudioSource> () [1];
			enemyStepAudio.spatialBlend = 1;
			enemyStepAudio.volume = 1;
			animator = GetComponentInChildren<Animator> ();

			isDead = false;
			GetComponent<LivingEntity> ().OnDeath += OnEnemyDeath;

			if (GameObject.FindGameObjectWithTag ("Player") != null) {
				player = GameObject.FindGameObjectWithTag ("Player").GetComponent<Transform> ();
			} else {
				player = null;
			}
		}

		void Update(){
			if (isDead) return;

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
			if (Vector3.Distance (transform.position, player.position) < stopDistance && Time.time > nextAttackTime) {
				nextAttackTime = Time.time + enemyAttackAudio.length;
				enemySoundAudio.Play ();
			}
		}

		void OnEnemyDeath(){
			isDead = true;
			enemySoundAudio.Stop ();
			enemyStepAudio.Stop ();
		}
	}
}
