using UnityEngine;
using System.Collections;

public class DamageOverTime : MonoBehaviour {
	public float damage = 5;
	void OnTriggerStay2D(Collider2D coll) {
		if (coll.CompareTag ("Enemy")) {
			coll.gameObject.GetComponent<Entity>().damageEntity(damage);
		}
	}
}
