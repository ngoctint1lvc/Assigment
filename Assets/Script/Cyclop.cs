using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyGame{
	public class Cyclop : Enemy {
		protected override void OnEnemyDeath(){
			base.OnEnemyDeath ();
			animator.SetBool ("Dead", true);
		}
	}
}
