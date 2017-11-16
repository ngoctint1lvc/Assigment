using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyGame{
	public class MusicThemeManager : MonoBehaviour {

		public AudioClip mainThemeMusic;
		public AudioClip enemyComeMusic;
		AudioSource enemyComeSource;
		GameObject player;

		void Awake(){
			enemyComeSource = gameObject.AddComponent<AudioSource> ();
			enemyComeSource.spatialBlend = 1;
			enemyComeSource.loop = false;
		}

		void Start(){
			AudioManager.instance.PlayThemeMusic (mainThemeMusic, 0.5f);
			enemyComeSource.clip = enemyComeMusic;
			Enemy.EnemyIsComming += OnEnemyCome;
		}

		void OnEnemyCome(){
			if (!enemyComeSource.isPlaying) {
				enemyComeSource.Play ();
			}
		}
	}
}