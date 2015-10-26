using UnityEngine;
using System.Collections;

public class SpreadProjectile : ProjectileMovement {
	public float strayFactor = 2f;
	public override void movement ()
	{
		GameObject clone1 = (GameObject) Instantiate(bullet, transform.position, Quaternion.identity); 
		GameObject clone2 = (GameObject) Instantiate(bullet, transform.position, Quaternion.identity); 
		GameObject clone3 = (GameObject) Instantiate(bullet, transform.position, Quaternion.identity); 
		//clones prefab
		Vector3 sp = Camera.main.WorldToScreenPoint(transform.position); 
		//get position relative to camera
		Vector3 dir = (Input.mousePosition - sp).normalized; 
		//subtract target position and current position to get vector
		clone1.GetComponent<Rigidbody2D> ().velocity = new Vector3 (dir.x * speed, dir.y * speed, 0); 

		if ((dir.x < 0 && dir.y > 0) || (dir.x > 0 && dir.y < 0)) {
			clone2.GetComponent<Rigidbody2D> ().velocity = new Vector3 (dir.x * speed + strayFactor, dir.y * speed + strayFactor, 0); 
			clone3.GetComponent<Rigidbody2D> ().velocity = new Vector3 (dir.x * speed - strayFactor, dir.y * speed - strayFactor, 0); 
		}
		if ((dir.x > 0 && dir.y > 0) || (dir.x < 0 && dir.y < 0)) {
			clone2.GetComponent<Rigidbody2D> ().velocity = new Vector3 (dir.x * speed + strayFactor, dir.y * speed - strayFactor, 0); 
			clone3.GetComponent<Rigidbody2D> ().velocity = new Vector3 (dir.x * speed - strayFactor, dir.y * speed + strayFactor, 0); 
		}
		//clone2.GetComponent<Rigidbody2D> ().velocity = new Vector3 (dir.x * speed + Mathf.Tan(30), dir.y * speed + Mathf.Cos(30), 0); 
		//clone3.GetComponent<Rigidbody2D> ().velocity = new Vector3 (dir.x * speed - Mathf.Tan(30), dir.y * speed - Mathf.Cos(30), 0); 

		clone2.transform.Rotate(dir * strayFactor);
		clone3.transform.Rotate (dir * -strayFactor);
		Debug.Log ("Dir: " + dir);
		//set velocity of cloned object so it moves towards target along calculated vector
		Destroy (clone1, lifeSpan);
		Destroy (clone2, lifeSpan);
		Destroy (clone3, lifeSpan);
	}
}
