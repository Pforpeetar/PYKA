using UnityEngine;
using System.Collections;

public class Projectile : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void OnCollisionEnter2D(Collision2D collInfo) {
		if (collInfo.gameObject.tag == "Enemy") {
			Enemy e = collInfo.gameObject.GetComponent<Enemy> ();
			e.health -= 10;
			Destroy (gameObject, 0.2f);
		} else {
			//Destroy (gameObject);
		}
	}
}
