using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyGame {
    [RequireComponent(typeof(Rigidbody))]
    public class PlayerController : MonoBehaviour {

        Vector3 velocity;
        Rigidbody rigidbody;

        void Awake() {
            rigidbody = GetComponent<Rigidbody>();
        }

		void Update(){
			transform.Translate (velocity * Time.deltaTime, Space.Self);
		}
        
        void FixedUpdate() {
			//rigidbody.MovePosition(rigidbody.position + velocity * Time.fixedDeltaTime);
        }

        public void Move(Vector3 vel) {
            velocity = vel;
        }

        public void LookAt(Vector3 lookPoint) {
            // lookPoint là điểm cho trỏ chuột chỉ tới
            // Tạo điểm nhìn đúng, Player chỉ có thể nhìn ở vị trí các điểm mp Oxz (tung độ y = tung độ Player )
            //Vector3 heightCorrectedPoint = new Vector3(lookPoint.x, transform.position.y, lookPoint.z);
            // Nếu dùng transform.LookAt(lookPoint) thì Player sẽ nhìn theo các điểm thuộc mp Oxyz
			transform.LookAt(lookPoint);
            //transform.LookAt(heightCorrectedPoint);
        }
    }
}