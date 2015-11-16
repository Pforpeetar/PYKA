using UnityEngine;
using System.Collections;

public class HomingProjectile : ProjectileMovement {
	private GameObject target = null;
	public override void movement ()
	{
		Vector3 sp = Camera.main.WorldToScreenPoint(transform.position); 
		//get position relative to camera
		Vector3 dir = (Input.mousePosition - sp).normalized; 

		Vector3 mousePos = Input.mousePosition;
		
		Vector3 objectPos = Camera.main.WorldToScreenPoint (transform.position);
		mousePos.x = mousePos.x - objectPos.x;
		mousePos.y = mousePos.y - objectPos.y;
		
		float angle = Mathf.Atan2(mousePos.y, mousePos.x) * Mathf.Rad2Deg;


		//clones prefab
		GameObject clone = (GameObject) Instantiate(bullet, transform.position + dir, Quaternion.identity); 
		clone.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle-90));

		//subtract target position and current position to get vector
		clone.GetComponent<Rigidbody2D> ().velocity = new Vector3 (dir.x * speed, dir.y * speed, 0); 
		//set velocity of cloned object so it moves towards target along calculated vector
		Destroy (clone, lifeSpan);
	}


}
