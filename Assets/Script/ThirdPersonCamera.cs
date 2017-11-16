using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyGame{
	public class ThirdPersonCamera : MonoBehaviour {

		public float mouseSensitivity = 10;
		Vector2 mouseLook;
		Vector2 smoothMovement;
		public float smooth = 2f;
		public Vector2 angleConstraint = new Vector2 (-20, 20);
		public Vector2 playerAngleConstraint = new Vector2(-15, 15);
		Transform player;

		void Start(){
			if (GameObject.FindGameObjectWithTag ("Player")) {
				player = GameObject.FindGameObjectWithTag ("Player").transform;
				transform.parent = player;
			} else {
				player = null;
			}
		}

		void Update () {
			if (player == null) return;

			Vector2 mouseMovement = new Vector2 (Input.GetAxisRaw ("Mouse X"), Input.GetAxisRaw ("Mouse Y")) * mouseSensitivity;
			smoothMovement.x = Mathf.Lerp (smoothMovement.x, mouseMovement.x, 1 / smooth);
			smoothMovement.y = Mathf.Lerp (smoothMovement.y, mouseMovement.y, 1 / smooth);

			mouseLook += smoothMovement;

			mouseLook.y = Mathf.Clamp (mouseLook.y, angleConstraint.x, angleConstraint.y);

			transform.localRotation = Quaternion.AngleAxis (-mouseLook.y, Vector3.right);
			player.eulerAngles = new Vector3 (-Mathf.Lerp(playerAngleConstraint.x, playerAngleConstraint.y, (mouseLook.y - angleConstraint.x) / (angleConstraint.y - angleConstraint.x)) , mouseLook.x, 0);
			//player.localRotation = Quaternion.AngleAxis (mouseLook.x, Vector3.up);
			//player.eulerAngles = new Vector3(-10, mouseLook.x, 0);
		}
	}
}
