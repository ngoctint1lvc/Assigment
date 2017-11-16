using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyGame{
	public class AudioManager : MonoBehaviour {

		public static AudioManager instance;
		AudioSource[] sources;
		int activeSourceIndex;

		void Awake(){
			if (instance == null) {
				instance = this;
			} else {
				Destroy (this.gameObject);
			}

			sources = new AudioSource[2];
			for (int i = 0; i < 2; i++) {
				GameObject newGameObject = new GameObject ();
				sources[i] = newGameObject.AddComponent<AudioSource> ();
				newGameObject.transform.parent = transform;
				newGameObject.transform.localPosition = Vector3.zero;
				sources [i].loop = true;
				sources [i].spatialBlend = 1;
			}
		}
			
		public void PlaySound(AudioClip clip, Vector3 position){
			if (clip != null) {
				AudioSource.PlayClipAtPoint (clip, position);
			}
		}

		public void PlayThemeMusic(AudioClip clip, float volume = 1, float duration = 1){
			activeSourceIndex = 1 - activeSourceIndex;
			sources [activeSourceIndex].clip = clip;
			sources [activeSourceIndex].Play ();
			StartCoroutine (ChangeThemeMusic (volume, duration));
		}

		IEnumerator ChangeThemeMusic(float volume, float duration){
			float volumePercent = 0;
			while (volumePercent < 1) {
				volumePercent += Time.deltaTime * 1 / duration;
				sources [activeSourceIndex].volume = Mathf.Lerp (0, volume, volumePercent);
				sources [1 - activeSourceIndex].volume = Mathf.Lerp (volume, 0, volumePercent);
				yield return null;
			}
		}
	}
}
