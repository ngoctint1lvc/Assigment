using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(AudioSource))]
public class PlayerFootStep : MonoBehaviour {

	public AudioClip stepOnGround;
	public AudioClip stepOnWater;

	public const float stepRate = 2.3f;
	float nextStepTime;
	AudioSource audioSource;

	void Start(){
		audioSource = GetComponent<AudioSource> ();
	}

	void Update(){
		if (transform.position.y < 1.5f) {
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
