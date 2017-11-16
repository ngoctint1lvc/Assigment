using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

namespace MyGame{
	public class MainmenuController : MonoBehaviour {

	    public void OnClickStart() {
	        SceneManager.LoadScene("GamePlay");
	    }

	    public void OnClickExit() {
			Application.Quit ();
	    }
	}
}