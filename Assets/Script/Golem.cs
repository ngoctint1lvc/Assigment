using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyGame{
	public class Golem : Enemy {
		protected override void OnEnemyDeath(){
			base.OnEnemyDeath ();
			animator.SetBool ("Dead", true);
		}
	}
}