using UnityEngine;
using System.Collections;

public class DestroyOnCollision : ProjectileProperty {
	public float damage = 10;
	public float lifepanAfterCollision = 0.2f;
	public override void property() {
		e.damageEntity(damage);
		Destroy (gameObject, lifepanAfterCollision);
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
