using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyGame {
	[RequireComponent(typeof(PlayerController))]
	[RequireComponent(typeof(WeaponController))]
	public class Player : LivingEntity {

		public float moveSpeed;
		PlayerController controller;
		Camera viewCamera;
		WeaponController weaponController;
		Animator myAnima;

		bool isStunned;
		float stopStunnedTime;
		Vector3 stunnedPosition;

		protected override void Start() {
			base.Start();
			Cursor.lockState = CursorLockMode.Locked;
			OnDeath += OnPlayerDeath;
			GetAttribute();
		}

		void Update() {
			// Toggle de hien / an con tro chuot
			if (Input.GetKeyDown (KeyCode.Space)) {
				if (Cursor.lockState == CursorLockMode.Locked) {
					Cursor.lockState = CursorLockMode.None;
				} else {
					Cursor.lockState = CursorLockMode.Locked;
				}
			}

			if (isStunned) {
				if (Time.time > stopStunnedTime) {
					isStunned = false;
				}
				transform.position = stunnedPosition;
				return;
			}
				
			MoveInput();
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

		void AttackInput() {
			// Weapon Input
			if (Input.GetMouseButton(0)) {
				weaponController.Attack();
				//myAnima.SetBool("Shoot", true);
			}
		}

		void OnPlayerDeath(){
			viewCamera.transform.parent = null;
		}

		public void Stunned(float stunnedTime){
			stopStunnedTime = Time.time + stunnedTime;
			isStunned = true;
			myAnima.SetBool ("Walk", false);

			// Luu vi tri bi choang hien tai
			stunnedPosition = transform.position;
		}
	}
}