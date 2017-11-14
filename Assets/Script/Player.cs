using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyGame {
	[RequireComponent(typeof(PlayerController))]
	[RequireComponent(typeof(WeaponController))]
	public class Player : LivingEntity {

		public float moveSpeed;
		public Vector2 viewAngleConstraint = new Vector2 (-5f, 5f);
		PlayerController controller;
		Camera viewCamera;
		WeaponController weaponController;
		Animator myAnima;

		protected override void Start() {
			Cursor.lockState = CursorLockMode.Locked;
			base.Start();
			GetAttribute();
		}

		void Update() {

			if (Input.GetKeyDown (KeyCode.Space)) {
				if (Cursor.lockState == CursorLockMode.Locked) {
					Cursor.lockState = CursorLockMode.None;
				} else {
					Cursor.lockState = CursorLockMode.Locked;
				}
			}
			MoveInput();
			//LookInput();
			AttackInput();
		}

		void GetAttribute() {
			controller = GetComponent<PlayerController>();
			weaponController = GetComponent<WeaponController>();
			myAnima = GetComponent<Animator>();
			viewCamera = Camera.main;
		}

		void MoveInput() {
			// Movement Input
			Vector3 moveInput = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
			Vector3 moveVelocity = moveInput.normalized * moveSpeed;
			controller.Move(moveVelocity);

			if (moveInput.normalized != Vector3.zero) myAnima.SetBool("Walk", true);
			else myAnima.SetBool("Walk", false);
		}

		void LookInput() {
			// Look input
			Ray ray = viewCamera.ScreenPointToRay(new Vector3 (Screen.width * 0.5f, Screen.height * 0.5f, 0));

			Plane groundPlane = new Plane(Vector3.up * 2, Vector3.up);
			// rayDistance là khoảng cách từ camera đến điểm trên tia sáng ray
			float rayDistance;
			// lấy giao điểm của mặt phẳng groundPlane và tia sáng ray, rayDistance = |camera->groundPlane|
			if (groundPlane.Raycast(ray, out rayDistance)) {
				// lấy điểm point ở khoảng cách rayDistance dọc theo tia sáng từ camera
				Vector3 point = ray.GetPoint(rayDistance);
				Debug.DrawLine(ray.origin, point, Color.blue);
				controller.LookAt(point);
			};

			/*
			Vector2 cameraAngle = viewCamera.GetComponent<ThirdPersonCamera> ().angleConstraint;
			float viewAngle;
			if (cameraAngle.x < cameraAngle.y) {
				viewAngle = Mathf.Lerp (viewAngleConstraint.x, viewAngleConstraint.y, (viewCamera.transform.localEulerAngles.x - cameraAngle.x) / (cameraAngle.y - cameraAngle.x));
			} else {
				print ("Error: Please check camera angle constraints");
				return;
			}

			print ("view angle = " + viewAngle);
			gameObject.transform.eulerAngles = new Vector3 (viewAngle, transform.eulerAngles.y, transform.eulerAngles.z);
			*/
		}

		void AttackInput() {
			// Weapon Input
			if (Input.GetMouseButton(0)) {
				weaponController.Attack();
				//myAnima.SetBool("Shoot", true);
			}
		}
	}

	public enum CameraMode{
		FirstPerson,
		ThirdPerson
	}
}