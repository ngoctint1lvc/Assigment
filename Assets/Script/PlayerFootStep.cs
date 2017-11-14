using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyGame{
	[RequireComponent(typeof(Animator))]
	[RequireComponent(typeof(AudioSource))]
	public class PlayerFootStep : MonoBehaviour {

		public AudioClip stepOnGround;
		public AudioClip stepOnWater;

		public const float stepRate = 2.3f;
		public const float waterLevel = 1.5f;
		float nextStepTime;
		AudioSource audioSource;

		void Start(){
			audioSource = GetComponent<AudioSource> ();
		}

		void Update(){
			if (transform.position.y < waterLevel) {
				audioSource.clip = stepOnWater;
			} else {
				audioSource.clip = stepOnGround;
			}

			if (GetComponent<Animator> ().GetBool ("Walk") && Time.time > nextStepTime) {
				nextStepTime = Time.time + 1 / stepRate;
				audioSource.Play ();
			}
		}
	}
}