using UnityEngine;
using System.Collections;

public class DestroyOnCollision : ProjectileProperty {

	public override void property() {
		e.health -= 10;
		Destroy (gameObject, 0.2f);
	}

	void OnCollisionEnter2D(Collision2D collInfo) {
		if (collInfo.gameObject.tag.Equals(target.ToString())) {
			e = collInfo.gameObject.GetComponent<Entity> ();
			property();
		} else {
			//Destroy (gameObject);
		}
	}

}
