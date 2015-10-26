using UnityEngine;
using System.Collections;

public class StraightProjectile : ProjectileMovement {

	public override void movement ()
	{
		GameObject clone = (GameObject) Instantiate(bullet, transform.position, Quaternion.identity); 
		//clones prefab
		Vector3 sp = Camera.main.WorldToScreenPoint(transform.position); 
		//get position relative to camera
		Vector3 dir = (Input.mousePosition - sp).normalized; 
		//subtract target position and current position to get vector
		clone.GetComponent<Rigidbody2D> ().velocity = new Vector3 (dir.x * speed, dir.y * speed, 0); 
		//set velocity of cloned object so it moves towards target along calculated vector
		Destroy (clone, lifeSpan);
	}
}
