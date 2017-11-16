using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyGame{
	[RequireComponent(typeof(Rigidbody))]
	public class Crawler : Enemy {
		protected override void OnEnemyDeath(){
			base.OnEnemyDeath ();
			// Do something more
		}
	}
}