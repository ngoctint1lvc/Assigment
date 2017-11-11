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

        protected override void Start() {
            base.Start();

            controller = GetComponent<PlayerController>();
            weaponController = GetComponent<WeaponController>();
            myAnima = GetComponent<Animator>();
			//myAnima = GameObject.Find("Soldier3").GetComponent<Animator>();

            viewCamera = Camera.main;
        }

        void Update() {
            // Movement Input
            Vector3 moveInput = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
            Vector3 moveVelocity = moveInput.normalized * moveSpeed;
            controller.Move(moveVelocity);

            //viewCamera.transform.SetPositionAndRotation(viewCamera.transform.position + moveVelocity * Time.fixedDeltaTime, viewCamera.transform.rotation);

            if (moveInput.normalized != Vector3.zero) myAnima.SetBool("Walk", true);
            else myAnima.SetBool("Walk", false);

            // Look input
            // ray là tia sáng từ main camera đến con trỏ chuột
            Ray ray = viewCamera.ScreenPointToRay(Input.mousePosition);
            // tạo mặt phẳng ảo
            Plane groundPlane = new Plane(Vector3.up, Vector3.zero);
            // rayDistance là khoảng cách từ camera đến một điểm trên tia sáng thuộc con trỏ chuột
            float rayDistance;
            // lấy giao điểm của mặt phẳng groundPlane và tia sáng ray, rayDistance = |camera->groundPlane|
            if (groundPlane.Raycast(ray, out rayDistance)) {
                // lấy điểm point ở khoảng cách rayDistance dọc theo tia sáng từ camera
                Vector3 point = ray.GetPoint(rayDistance);
                //Debug.DrawLine(ray.origin, point, Color.blue);
                controller.LookAt(point);
            };

            // Weapon Input
            if (Input.GetMouseButton(0)) {
                weaponController.Attack();
                //myAnima.SetBool("Shoot", true);
            }
        }
    }
}