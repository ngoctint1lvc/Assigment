using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
	const float speed = 15f;

	void Update () {
		Vector2 input = new Vector2 (Input.GetAxisRaw ("Horizontal"), Input.GetAxisRaw ("Vertical"));

		Vector3 direction = new Vector3(input.x, 0, input.y).normalized;
		transform.Translate (direction * speed * Time.deltaTime);
	}
}
