using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicThemeManager : MonoBehaviour {

	public AudioClip mainThemeMusic;

	void Start(){
		AudioManager.instance.PlayThemeMusic (mainThemeMusic, 0.5f);
	}
}
