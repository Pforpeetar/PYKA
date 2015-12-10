using UnityEngine;
using System.Collections;

public class HealthPickup : MonoBehaviour {

	void OnTriggerEnter2D(Collider2D coll) {
		if (coll.CompareTag ("Player")) {
			coll.GetComponent<Player>().health += 25;
			Destroy(gameObject);
		}
	}
}
